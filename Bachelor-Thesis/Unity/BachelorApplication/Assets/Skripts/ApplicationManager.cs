using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    public static bool useDefaultLoader = true;

    public bool IsGeneratingImages = false;
    public int generatedImages;
    public int cameraCounter;
    public GlobalSettingsModel GlobalSettingsModel { get; set; }

    public static ApplicationManager instance;
    private static System.Random Randomizer;

    GeneralParameterController GeneralParameterController;
    IEditorUIController EditorUIController;
    IEditorController EditorController;

    private void Awake()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("Manager");
        if(managers.Length > 1)
        {
            Destroy(gameObject);
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }

        GlobalSettingsModel = new GlobalSettingsModel();        

        SceneManager.sceneLoaded += InitViews;

        ImportModels();
        ImportTextures();
        ImportRegions();
        ImportLightning();
        ImportCamera();

        //<TO EXPAND> Add new Import/Load Method
    }

    void InitViews(Scene scene, LoadSceneMode mode)
    {

        switch (scene.buildIndex)
        {
            case 0:
                var factory = new GeneralParameterViewFactory();
                var view = factory.View;
                if(GeneralParameterController == null)
                {
                    var model = GlobalSettingsModel.GeneralParameterModel;
                    GeneralParameterController = new GeneralParameterController(model, view);
                }
                else
                {
                    GeneralParameterController.SubscribeView(view);
                    GeneralParameterController.ReloadView();
                }

                if (EditorController != null)
                {
                    EditorController.UnsubscribeView();
                }
                if (EditorUIController != null)
                {
                    EditorUIController.UnsubscribeView();
                }
                break;

            case 1:
                if(EditorUIController == null)
                {
                    var editorUIViewFactory = new EditorUIViewFactory();
                    EditorUIController = new EditorUIController(GlobalSettingsModel.EditorUIModel, editorUIViewFactory.View);
                }
                else
                {
                   var editorUIViewFactory = new EditorUIViewFactory();
                    EditorUIController.subscribeView(editorUIViewFactory.View);
                }

                if (EditorController == null)
                {
                    var editorViewFactory = new EditorViewFactory();
                    EditorController = new EditorController(GlobalSettingsModel.EditorModel, editorViewFactory.View);
                    EditorController.OnDisplaySceneObjectParameter += EditorUIController.DisplaySceneObjectParameter;
                    EditorController.OnHideSceneObjectParameter += EditorUIController.HideSceneObjectParameter;
                }
                else
                {
                    var editorViewFactory = new EditorViewFactory();
                    EditorController.SubscribeView(editorViewFactory.View);
                    EditorController.Init(!IsGeneratingImages);
                }

                if (IsGeneratingImages)
                {
                    StartCoroutine(GenerateImages());
                }
                break;
        }
    }

    public System.Random GetRandomizer()
    {
        if (Randomizer == null)
        {
            if(!GlobalSettingsModel.GeneralParameterModel.RandomizationSeed.HasValue)
            { 
                GlobalSettingsModel.GeneralParameterModel.RandomizationSeed = Guid.NewGuid().GetHashCode();
            }
            Randomizer = new System.Random((int)GlobalSettingsModel.GeneralParameterModel.RandomizationSeed);
        }
        return Randomizer;
    }
    public void LoadEditorScene()
    {
        GeneralParameterController.UnsubscribeView();
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        EditorController.DeleteSceneObjectController();
        SceneManager.LoadScene(0);
    }


    public void StartGenerateImages()
    {
        IsGeneratingImages = true;
        instance.LoadEditorScene();        
    }

    public IEnumerator GenerateImages()
    {
        EditorUIController.SwitchUI();

        List<AbstractSceneObjectModel> cameras = new List<AbstractSceneObjectModel>();
        List<AbstractSceneObjectModel> randomizationObjects = new List<AbstractSceneObjectModel>();
        GameObject[] AllSceneObjects = GameObject.FindGameObjectsWithTag("Model");
        Screen.SetResolution(GlobalSettingsModel.GeneralParameterModel.ResolutionX, GlobalSettingsModel.GeneralParameterModel.ResolutionY, true);

        foreach(var model in GlobalSettingsModel.EditorModel.ObjectsInScene)
        {
            if(model is CameraSceneObjectModel)
            {
                cameras.Add(model);
            }
            if(!(model is RegionSceneObjectModel) && model.IsRandom())
            {
                randomizationObjects.Add(model);
            }
        }

        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        File.Delete(instance.GlobalSettingsModel.GeneralParameterModel.TargetFolder + "\\gt.yml");
        StreamWriter writer = File.AppendText(instance.GlobalSettingsModel.GeneralParameterModel.TargetFolder + "\\gt.yml");
        Camera mainCamera = Camera.main;
        for (int i = 0; i < GlobalSettingsModel.GeneralParameterModel.AmountOfImages; i++)
        {
            mainCamera.tag = "MainCamera";
            //Randomize objects in scene
            foreach (var model in randomizationObjects)
            {
                //yield return new WaitForSeconds(1);
                model.RandomizeObject();
            }

            //Take image from all cameras
            mainCamera.tag = "Camera";


            foreach (var camera in cameras)
            {
                writer.WriteLine(instance.generatedImages + ":");
                ((CameraSceneObjectModel)camera).CreateImage();
                ((CameraSceneObjectModel)camera).SetMainCamera();

                for(int j = 0; j < AllSceneObjects.Length; j++)
                {
                    Rect? boundingBoxNullable = GetBoundingBox(AllSceneObjects[j]);
                    if (boundingBoxNullable != null)
                    {
                        Rect boundingBox = (Rect)boundingBoxNullable;
                        writer.WriteLine("- obj_bb: " + "[" + (int)boundingBox.x + ", " + (int)boundingBox.y + ", " + (int)boundingBox.width + ", " + (int)boundingBox.height + "]");
                        writer.WriteLine("  obj_id: " + j);
                    }
                }

                ((CameraSceneObjectModel)camera).SetDefaultCamera();
                generatedImages++;
            }
            EditorUIController.IncreaseImageCounter();
            yield return new WaitForEndOfFrame();
        }
        mainCamera.tag = "MainCamera";
        writer.Close();
        EditorUIController.GenerationFinished();
    }

    private Rect? GetBoundingBox(GameObject go)
    {
        Vector3[] vertices = go.GetComponent<MeshFilter>().mesh.vertices;

        float x1 = float.MaxValue, y1 = float.MaxValue, x2 = 0.0f, y2 = 0.0f;

        if (ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.OnlyFullyCoveredObjects)
        {
            foreach (Vector3 vert in vertices)
            {
                Vector2 tmp = Camera.main.WorldToScreenPoint(go.transform.TransformPoint(vert));
                if (tmp.x < 0 || tmp.y < 0 || tmp.x >= instance.GlobalSettingsModel.GeneralParameterModel.ResolutionX || tmp.y >= instance.GlobalSettingsModel.GeneralParameterModel.ResolutionY)
                {
                    return null;
                }
                if (tmp.x < x1) x1 = tmp.x;
                if (tmp.x > x2) x2 = tmp.x;
                if (tmp.y < y1) y1 = tmp.y;
                if (tmp.y > y2) y2 = tmp.y;
            }
            Rect bbox = new Rect(x1, y2, x2 - x1, y2 - y1);
            return bbox;
        }
        else
        {
            foreach (Vector3 vert in vertices)
            {
                Vector2 tmp = Camera.main.WorldToScreenPoint(go.transform.TransformPoint(vert));

                if (tmp.x < 0)
                {
                    tmp.x = 0;
                }
                if (tmp.y < 0)
                {
                    tmp.y = 0;
                }
                if (tmp.x >= instance.GlobalSettingsModel.GeneralParameterModel.ResolutionX)
                {
                    tmp.x = instance.GlobalSettingsModel.GeneralParameterModel.ResolutionX - 1;
                }
                if (tmp.y >= instance.GlobalSettingsModel.GeneralParameterModel.ResolutionY)
                {
                    tmp.y = instance.GlobalSettingsModel.GeneralParameterModel.ResolutionY - 1;
                }
                if (tmp.x < x1) x1 = tmp.x;
                if (tmp.x > x2) x2 = tmp.x;
                if (tmp.y < y1) y1 = tmp.y;
                if (tmp.y > y2) y2 = tmp.y;
            }
            if (x1 == x2 || y1 == y2)
            {   
                return null;        
            }
            Rect bbox = new Rect(x1, y2, x2 - x1, y2 - y1);
            return bbox;
        }
    }

    private void ImportModels()
    {
        string importPath = Application.dataPath + "/Import/" + "Models";
        GameObject Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Plane.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        ModelItemModel planeModel = new ModelItemModel {
            DisplayName = "Plane",
            Mesh = Plane.GetComponent<MeshFilter>().mesh,
            Materials = Plane.GetComponentInChildren<MeshRenderer>().materials
        };
        
        GlobalSettingsModel.EditorModel.AllModelItemsModel.Add(planeModel);
        foreach(string file in Directory.GetFiles(importPath))
        {
            if (file.EndsWith(".obj"))
            {
                OBJLoader objLoader = new OBJLoader();
                GameObject tmpObject = objLoader.Load(file).gameObject;
                try
                {
                    ModelItemModel model = new ModelItemModel();
                    model.DisplayName = tmpObject.name;
                    model.Mesh = tmpObject.GetComponentInChildren<MeshFilter>().mesh;
                    model.Materials = tmpObject.GetComponentInChildren<MeshRenderer>().materials;
                    GlobalSettingsModel.EditorModel.AllModelItemsModel.Add(model);
                }
                catch(Exception e)
                {
                    Debug.Log("An error has ocured during import of " + tmpObject.name +  "\n" + e.Message + "\n" + e.StackTrace);
                }
                finally
                {
                    Destroy(tmpObject);
                }
            }
        }
        Debug.Log(GlobalSettingsModel.EditorModel.AllModelItemsModel.Length() + " Models imported");
    }

    private void ImportTextures()
    {
        //Add imported textures
        string importPath = Application.dataPath + "/Import/" + "Textures";
        int importCounter = 0;
        string[] files = Directory.GetFiles(importPath);
        TextureItemModel blankModel = new TextureItemModel();
        blankModel.DisplayName = "No Texture";
        GlobalSettingsModel.EditorModel.AllTextureItemsModel.Add(blankModel);

        foreach (string file in files)
        {
            if (file.EndsWith(".png") || file.EndsWith(".jpg"))
            {
                string name = file.Substring(file.LastIndexOf('\\') + 1);
                name = name.Substring(0, name.Length - 4);
                
                Texture2D texture = new Texture2D(2, 2);
                byte[] byteArray = File.ReadAllBytes(file);
                if (texture.LoadImage(byteArray))
                {
                    int index = name.LastIndexOf("_");
                    string textureName = name.Substring(0, index);

                    texture.name = textureName;
                    TextureItemModel model;
                    if (GlobalSettingsModel.EditorModel.AllTextureItemsModel.Contains(textureName))
                    {
                        model = GlobalSettingsModel.EditorModel.AllTextureItemsModel.Get(textureName);
                    }
                    else
                    {
                        model = new TextureItemModel();
                        model.DisplayName = textureName;
                        GlobalSettingsModel.EditorModel.AllTextureItemsModel.Add(model);
                        importCounter++;
                    }

                    if (name.EndsWith("_color") || name.EndsWith("_Color") || name.EndsWith("_diff") || name.EndsWith("_Diff") || name.EndsWith("_albedo") || name.EndsWith("_Albedo") || name.EndsWith("_Diffuse") || name.EndsWith("_diffuse"))
                    {                        
                        model.SetColorMap(texture);
                    }
                                        
                    if (name.EndsWith("_normal") || name.EndsWith("_Normal") || name.EndsWith("_nor") || name.EndsWith("_Normal"))
                    {
                        model.SetNormalMap(texture); 
                    }
                    /*
                    if (name.EndsWith("_roughness") || name.EndsWith("_Roughness"))
                    {
                        Texture2D newTexture = new Texture2D(texture.width, texture.height);
                        for (int i = 0; i < texture.width; i++)
                        {
                            for (int j = 0; j < texture.height; j++)
                            {
                                Color grey = texture.GetPixel(i, j);
                                Color alpha = newTexture.GetPixel(i, j);
                                alpha.a = 1f - grey.r;
                                newTexture.SetPixel(i, j, alpha);
                            }
                        }
                        newTexture.Apply();
                        byte[] bytes = newTexture.EncodeToJPG(100);
                        File.WriteAllBytes("F:\\Users\\Flo\\Desktop\\Studium\\BachelorArbeit\\bachelor-thesis\\Unity\\BachelorApplication\\Assets\\Import\\Textures\\" + "_metallic" + ".jpg", bytes);
                        if (GlobalSettingsModel.EditorModel.AllTextureItemsModel.Contains(textureName))
                        {
                            var model = GlobalSettingsModel.EditorModel.AllTextureItemsModel.Get(textureName);
                            model.MetallicMap = newTexture;
                        }
                        else
                        {
                            TextureItemModel model = new TextureItemModel();
                            model.MetallicMap = newTexture;
                            model.DisplayName = textureName;
                            GlobalSettingsModel.EditorModel.AllTextureItemsModel.Add(model);
                            importCounter++;
                        }
                        DestroyImmediate(newTexture);
                    }*/
                }
                //DestroyImmediate(texture);
            }
        }

        Debug.Log(importCounter + " textures imported");
    }

    private void ImportRegions()
    {
        //Add hardcoded region items
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        RegionItemModel planeModel = new RegionItemModel();
        planeModel.DisplayName = "Plane";
        planeModel.Mesh = plane.GetComponent<MeshFilter>().mesh;
        planeModel.Materials = plane.GetComponent<MeshRenderer>().materials;
        GlobalSettingsModel.EditorModel.AllRegionItemsModel.Add(planeModel);

        //Add imported region items
        string importPath = Application.dataPath + "/Import/" + "Regions";
        foreach (string file in Directory.GetFiles(importPath))
        {
            if (file.EndsWith(".obj"))
            {
                OBJLoader objLoader = new OBJLoader();
                GameObject tmpObject = objLoader.Load(file).gameObject;
                try
                {
                    RegionItemModel model = new RegionItemModel();
                    model.DisplayName = tmpObject.name;
                    model.Mesh = tmpObject.GetComponentInChildren<MeshFilter>().mesh;
                    model.Materials = tmpObject.GetComponentInChildren<MeshRenderer>().materials;
                    GlobalSettingsModel.EditorModel.AllRegionItemsModel.Add(model);
                }
                catch (Exception e)
                {
                    Debug.Log("An error has occured during import of " + tmpObject.name + "\n" + e.Message + "\n" + e.StackTrace);
                }
                finally
                {
                    Destroy(tmpObject);
                }
            }
        }
    }

    private void ImportLightning()
    {
        GlobalSettingsModel.EditorModel.AllLightItemsModel.Add(new PointLightItemModel());
        GlobalSettingsModel.EditorModel.AllLightItemsModel.Add(new DirectionalLightItemModel());
        GlobalSettingsModel.EditorModel.AllLightItemsModel.Add(new SpotLightItemModel());

    }

    private void ImportCamera()
    {
        GlobalSettingsModel.EditorModel.AllCameraItemsModel.Add(new CameraItemModel());
    }

    // <TO EXPAND> Create new Import/Load Method
}
