using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class SceneObjectController : ISceneObjectController
{
    private AbstractSceneObjectModel model;
    private ISceneObjectView view;
    public event EventHandler<SceneObjectSelectedEventArgs> OnSendParameter = (sender, e) => { };
    public event EventHandler<DeleteSceneObjectEventArgs> OnDeleteSceneObject = (sender, e) => { };
    public event EventHandler OnDeleteReferences = (sender, e) => { };
    public event EventHandler OnUpdateArrow = (sender, e) => { };

    public SceneObjectController(AbstractSceneObjectModel model, ISceneObjectView view)
    {
        this.model = model;
        this.view = view;
        view.OnParameterValueChanged += ParameterValueChanged;
        view.OnSceneObjectValueChanged += SceneObjectValueChanged;
        view.OnDestroyController += DestoryController;
        view.TestRandomize += TestRandomize;
        view.OnDeleteSceneObject += DeleteSceneObject;
        foreach(var parameter in model.Parameter)
        {
            parameter.OnUpdateSceneObjectView += UpdateSceneObjectView;
        }
    }

    public void DeleteSceneObject()
    {
        DeleteSceneObjectEventArgs args = new DeleteSceneObjectEventArgs();
        args.model = model;
        OnDeleteSceneObject(this, args);
    }

    public ISceneObjectView getView()
    {
        return view;
    }

    public void TestRandomize()
    {
        model.RandomizeObject();
    }

    public void RequestParameter(object sender, SceneObjectSelectedEventArgs e)
    {
        SceneObjectView tmpView = (SceneObjectView)view;
        if(IsViewNull())
        {
            DestoryController(this, EventArgs.Empty);
            return;
        }
        if (!view.IsEquals(e.Selection))
        {
            return;
        }
        e.Parameter = model.Parameter;
        OnSendParameter(this, e);
    }

    public void ParameterValueChanged(object sender, StoreParameterEventArgs e)
    {
        foreach (var parameterModel in model.Parameter)
        {
            if(parameterModel.ParameterType == e.ParameterType)
            {
                parameterModel.StoreAndApplyValueOnSceneObject(e);
                
                if(e.ParameterType == Constants.SceneObjectParameter.POSITION)
                {
                    OnUpdateArrow(this, EventArgs.Empty);
                }
            }
        }
    }
    private void SceneObjectValueChanged(object sender, StoreParameterEventArgs e)
    {
        foreach (var parameterModel in model.Parameter)
        {
            if (parameterModel.ParameterType == e.ParameterType)
            {
                parameterModel.StoreValue(e);
            }
        }
    }

    public void DestoryController(object sender, EventArgs e)
    {
        OnDeleteReferences(this, EventArgs.Empty);
        view.OnParameterValueChanged -= ParameterValueChanged;
        view.OnSceneObjectValueChanged -= SceneObjectValueChanged;
        view.OnDestroyController -= DestoryController;
        view.OnDeleteSceneObject -= DeleteSceneObject;
        view.TestRandomize -= TestRandomize;
        if (model is CameraSceneObjectModel model1)
        {
            model1.OnCreateImage -= CreateImage;
            model1.OnSetMainCamera -= SetMainCamera;
            model1.OnSetDefaultCamera -= SetDefaultCamera;
        }

        foreach (var parameter in model.Parameter)
        {
            parameter.OnUpdateSceneObjectView -= UpdateSceneObjectView;
        }
        if (!IsViewNull())
        {
            UnityEngine.Object.Destroy(view.GetGameObject());
        }
        view = null;
        model = null;
    }

    public void UpdateSceneObjectView(object sender, EventArgs e)
    {
        SceneObjectParameterModel model = (SceneObjectParameterModel)sender;
        model.UpdateParameterOnSceneObject(view.GetGameObject());
    }

    private bool IsViewNull()
    {
        SceneObjectView tmpView = (SceneObjectView)view;
        return tmpView == null;
    }

    public void CreateImage()
    {
        Camera camera = view.GetGameObject().GetComponent<Camera>();
        int width = ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.ResolutionX;
        int height = ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.ResolutionY;
        
        RenderTexture previousActive = RenderTexture.active;
        RenderTexture previousCamera = camera.targetTexture;
        
        RenderTextureDescriptor descriptor = new RenderTextureDescriptor(width,height);

        camera.targetTexture = new RenderTexture(descriptor);
        string targetPath = ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.TargetFolder;
        
        RenderTexture.active = camera.targetTexture;

        Texture2D imageTexture = new Texture2D(width, height);

        //Create color image
        if (ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.ColorPictures)
        {
            Directory.CreateDirectory(targetPath + "\\Color");

            camera.Render();
            imageTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            imageTexture.Apply();

            byte[] bytes = imageTexture.EncodeToPNG();
            File.WriteAllBytes(targetPath + "\\Color\\" + ApplicationManager.instance.generatedImages + ".png", bytes);
        }

        //Create depth image with greyScale
        if (ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.DepthPictures)
        {
            Directory.CreateDirectory(targetPath + "\\Depth");

            camera.GetComponent<RenderDepth>().enabled = true;
            camera.Render();

            imageTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            imageTexture.Apply();

            byte[] depthBytes = imageTexture.EncodeToPNG();

            File.WriteAllBytes(targetPath + "\\Depth\\" + ApplicationManager.instance.generatedImages + ".png", depthBytes);
            camera.GetComponent<RenderDepth>().enabled = false;
        }

        if(ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.SegmentationPicture)
        {
            Directory.CreateDirectory(targetPath + "\\Segmentation");
            ImageSynthesis imageSynthesis = camera.GetComponent<ImageSynthesis>();
            imageSynthesis.enabled = false;
            imageSynthesis.enabled = true;
            imageSynthesis.saveDepth = false;
            imageSynthesis.saveImage = false;
            imageSynthesis.saveOpticalFlow = false;
            imageSynthesis.saveNormals = false;
            imageSynthesis.saveLayerSegmentation = false;
            imageSynthesis.saveIdSegmentation = true;

            imageSynthesis.Save(ApplicationManager.instance.generatedImages + ".png", width, height, targetPath + "\\Segmentation");
        }
        RenderTexture.active = previousActive;

        Texture2D.Destroy(imageTexture);
    }

    public void SetMainCamera()
    {
        if(view != null)
        {
            view.GetGameObject().GetComponent<Camera>().tag = "MainCamera";
        }
    }

    public void SetDefaultCamera()
    {
        if (view != null)
        {
            view.GetGameObject().GetComponent<Camera>().tag = "Camera";
        }
    }
}
