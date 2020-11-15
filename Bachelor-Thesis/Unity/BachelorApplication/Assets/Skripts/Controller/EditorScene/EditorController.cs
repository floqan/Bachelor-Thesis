using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static EditorView;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditorController : IEditorController
{

    private IEditorView view;
    private EditorModel model;
    private bool ControllerLoaded = false;

    public event EventHandler<SceneObjectSelectedEventArgs> OnDisplaySceneObjectParameter = (sender, e) => { };
    public event EventHandler<SceneObjectSelectedEventArgs> OnHideSceneObjectParameter = (sender, e) => { };
    public event EventHandler<SceneObjectSelectedEventArgs> OnRequestSceneObjectParameter = (sender, e) => { };
    public event EventHandler OnDestroyController = (sender, e) => { };

    public EditorController(EditorModel model, IEditorView view)
    {
        this.model = model;
        SubscribeView(view);
        LoadController();
        LoadSceneObjects();
    }

    public void Init(bool loadController)
    {
        if (loadController)
        {
            LoadController();
        }
        LoadSceneObjects();
    }

    public void LoadController()
    {
        if (!ControllerLoaded)
        {
            CreateItemCategoryController(model.AllModelItemsModel, "ModelItemContent");
            CreateItemCategoryController(model.AllRegionItemsModel, "RegionItemContent");
            CreateItemCategoryController(model.AllTextureItemsModel, "TextureItemContent");
            CreateItemCategoryController(model.AllLightItemsModel, "LightningItemContent");
            CreateItemCategoryController(model.AllCameraItemsModel, "CameraItemContent");
        }
        ControllerLoaded = true;

        // <TO EXTEND> Add CreateItemCategoryController(model, contentViewName); contentViewName is the objectname in Unity, where the items should be placed within
    }

    public void DeleteSceneObjectController()
    {
        OnDestroyController(this, EventArgs.Empty);
    }

    private void DeleteReferences(object sender, EventArgs e)
    {
        ISceneObjectController controller = (ISceneObjectController)sender;
        OnDestroyController -= controller.DestoryController;
        OnRequestSceneObjectParameter -= controller.RequestParameter;
        controller.OnSendParameter -= DisplaySceneObjectParameter;
        controller.OnDeleteReferences -= DeleteReferences;
        controller.OnUpdateArrow -= UpdateArrow;
        controller.OnDeleteSceneObject -= DeleteSelectionModel;
    }

    public void SubscribeView(IEditorView view)
    {
        this.view = view;
        this.view.DeleteDefaultSun(model.DefaultSunDeleted);
        this.view.OnPlaceItem += PlaceItem;
        this.view.OnSceneObjectSelected += SelectSceneObject;
        this.view.OnSceneObjectDeselected += DeselectSceneObject;
        this.view.OnHideParameter += HideSceneObjectParameter;
        this.view.OnDefaultSunDeleted += DefaultSunDeleted;
    }


    public void UnsubscribeView()
    {
        view.OnPlaceItem -= PlaceItem;
        view.OnSceneObjectSelected -= SelectSceneObject;
        view.OnSceneObjectDeselected -= DeselectSceneObject;
        view.OnHideParameter -= HideSceneObjectParameter;
        view.OnDefaultSunDeleted -= DefaultSunDeleted;
        view = null;
        ControllerLoaded = false;
    }

    private void CreateItemCategoryController<T>(AllItemsModel<T> model, string contentViewName) where T : AbstractItemModel
    {
        var controllerFactory = new DisplayItemControllerFactory();
        var viewFactory = new DisplayItemViewFactory();
        var controller = controllerFactory.CreateDisplayItemController(model);
        controller.CreateSceneObject += CreateSceneObject;
        for(int i = 0; i < controller.ItemModels.Length(); i++)
        {
            DisplayItemView view = viewFactory.CreateDisplayItemView(controller.ItemModels.Get(i), i, contentViewName);
            controller.SubscribeView(view);
        }
    }

    private void CreateSceneObject(object sender, DisplayItemClickedArgs e)
    {
        if (view.state == State.ItemPlacement)
        {
            return;
        }
        if (e.ItemModel is TextureItemModel)
        {
            DeselectSceneObject();
            view.state = State.TexturePlacement;
            view.Selection = GameObject.CreatePrimitive(PrimitiveType.Plane);
            e.ItemModel.ApplyItemProperties(view.Selection);
            view.Selection.transform.localScale *= 10;
        }
        else
        {
            GameObject sceneObject = CreateSceneObject(e.ItemModel);

            SceneObjectSelectedEventArgs args = new SceneObjectSelectedEventArgs();
            args.Selection = sceneObject;
            SelectSceneObject(this, args);
            view.state = State.ItemPlacement;
        }
    }


    private GameObject CreateSceneObject<T>(T model) where T:AbstractItemModel
    {
        GameObject sceneObject = new GameObject();
        sceneObject = model.ApplyItemProperties(sceneObject);
        AbstractSceneObjectModel sceneObjectModel = model.CreateSceneObjectModel();

        foreach(var parameter in sceneObjectModel.Parameter)
        {
            parameter.InitFromGameObject(sceneObject);
        }
        ISceneObjectView sceneObjectView = sceneObjectModel.CreateView(sceneObject);
        ISceneObjectController sceneObjectController = new SceneObjectController(sceneObjectModel, sceneObjectView);
        this.model.AddSceneObject(sceneObjectModel);
        OnDestroyController += sceneObjectController.DestoryController;
        OnRequestSceneObjectParameter += sceneObjectController.RequestParameter;
        sceneObjectController.OnSendParameter += DisplaySceneObjectParameter;
        sceneObjectController.OnDeleteReferences += DeleteReferences;
        sceneObjectController.OnUpdateArrow += UpdateArrow;
        sceneObjectController.OnDeleteSceneObject += DeleteSelectionModel;
        return sceneObject;

    }

    private void LoadSceneObjects()
    {
        foreach (var model in model.ObjectsInScene)
        {
            GameObject sceneObject = new GameObject();
            sceneObject = model.ItemModel.ApplyItemProperties(sceneObject);
            ISceneObjectView sceneObjectView = model.CreateView(sceneObject);
            ISceneObjectController sceneObjectController = new SceneObjectController(model, sceneObjectView);
            
            if (model is ModelSceneObjectModel || model is RegionSceneObjectModel)
            {
                sceneObject.layer = LayerMask.NameToLayer("SceneObject");
            }
            else if(model is CameraSceneObjectModel)
            {
                sceneObject.layer = LayerMask.NameToLayer("Visualization");
                ((CameraSceneObjectModel)model).OnCreateImage += sceneObjectController.CreateImage;
                ((CameraSceneObjectModel)model).OnSetMainCamera += sceneObjectController.SetMainCamera;
                ((CameraSceneObjectModel)model).OnSetDefaultCamera += sceneObjectController.SetDefaultCamera;
            }
            else if (model is AbstractLightSceneObjectModel)
            {
                sceneObject.layer = LayerMask.NameToLayer("Visualization");
            }
            
            OnDestroyController += sceneObjectController.DestoryController;
            OnRequestSceneObjectParameter += sceneObjectController.RequestParameter;
            sceneObjectController.OnSendParameter += DisplaySceneObjectParameter;
            sceneObjectController.OnDeleteReferences += DeleteReferences;
            sceneObjectController.OnUpdateArrow += UpdateArrow;
            sceneObjectController.OnDeleteSceneObject += DeleteSelectionModel;

            foreach (var parameter in model.Parameter)
            {
                parameter.UpdateParameterOnSceneObject(sceneObject);
            }

        }
    }

    public void UpdateArrow(object sender, EventArgs e)
    {  
        view.MoveArrow();
    }

    private void DisplaySceneObjectParameter(object sender, SceneObjectSelectedEventArgs e)
    {
        OnDisplaySceneObjectParameter(sender, e);
    }

    private void SelectSceneObject(object sender, SceneObjectSelectedEventArgs e)
    {
        if(view.Selection == e.Selection)
        {
            return;
        }
        if (view.Selection != null)
        {
            DeselectSceneObject();
        }
        view.state = State.ItemSelection;
        view.Selection = e.Selection;
        Outline outline = view.Selection.GetComponent<Outline>();
        if(outline != null)
        {
            outline.enabled = true;
        }
        view.Arrow.SetActive(true);
        view.MoveArrow();
        if(e.Selection.tag == "Camera")
        {
            
            view.DisplayCameraPreview();
        }

        OnRequestSceneObjectParameter(this, e);
    }

    private void DeselectSceneObject()
    {
        if (view.Selection != null)
        {
            view.HideCameraPreview();
            SceneObjectSelectedEventArgs args = new SceneObjectSelectedEventArgs();
            args.Selection = view.Selection;
            OnHideSceneObjectParameter(this, args);
            Outline outline = view.Selection.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            view.Arrow.SetActive(false);
            view.Selection = null;
        }
        view.state = State.Idle;
    }

    public void HideSceneObjectParameter(object sender, SceneObjectSelectedEventArgs args)
    {
        OnHideSceneObjectParameter(sender, args);
    }

    private void PlaceItem(object sender, EventArgs e) 
    {
        switch (view.Selection.tag)
        {
            case "Model": 
                view.Selection.layer = LayerMask.NameToLayer("SceneObject");
                break;
            case "Light":
                view.Selection.layer = LayerMask.NameToLayer("Visualization");
                break;
            case "Camera":
                view.Selection.layer = LayerMask.NameToLayer("Visualization");
                break;
            case "Region":
                view.Selection.layer = LayerMask.NameToLayer("SceneObject");
                break;
        }
        view.state = State.ItemSelection;
    }

    public void DeleteSelectionModel(object sender, DeleteSceneObjectEventArgs e)
    {
        model.ObjectsInScene.Remove(e.model);
    }

    public void DefaultSunDeleted()
    {
        model.DefaultSunDeleted = true;
    }
}
