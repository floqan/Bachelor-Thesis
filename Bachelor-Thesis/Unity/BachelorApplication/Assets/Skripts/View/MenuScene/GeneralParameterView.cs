using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SFB;
using System.IO;
using Boo.Lang;

public class GeneralParameterView : MonoBehaviour, IGeneralParameterView
{
    public InputField TargetFolderField { get; set; }
    public InputField ResolutionXField { get; set; }
    public InputField ResolutionYField { get; set; }
    public InputField AmountOfImagesField { get; set; }
    public InputField RandomizationField { get; set; }
    public Toggle ColorImageToggle { get; set; }
    public Toggle DepthImageToggle { get; set; }
    public Toggle SegmentationImageToggle { get; set; }
    public Toggle OnlyFullyCoveredObjectsToggle { get; set; }

    public Transform warningTargetFolder;


    public GameObject loadingScreen;
    public GameObject quitDialog;

    public Transform infoResolution;

    public event Action OnGenerateImages;
    public event EventHandler<StoreParameterEventArgs> OnStoreParameter = (sender, e) => { };

    public void Init()
    {
        TargetFolderField = GameObject.Find("InputTargetFolder").GetComponent<InputField>();
        ResolutionXField = GameObject.Find("InputResolutionX").GetComponent<InputField>();
        ResolutionYField = GameObject.Find("InputResolutionY").GetComponent<InputField>();
        AmountOfImagesField = GameObject.Find("InputAmountOfImages").GetComponent<InputField>();
        RandomizationField = GameObject.Find("InputRandomizationSeed").GetComponent<InputField>();
        ColorImageToggle = GameObject.Find("NormalPictureToggle").GetComponent<Toggle>();
        DepthImageToggle = GameObject.Find("DepthPictureToggle").GetComponent<Toggle>();
        SegmentationImageToggle = GameObject.Find("SegmentationPictureToggle").GetComponent<Toggle>();
        OnlyFullyCoveredObjectsToggle = GameObject.Find("OnlyFullyCoveredObjetsToggle").GetComponent<Toggle>();
    }

    char ValidatInputIsPositive(char addedChar, int index)
    {
        if ((index == 0 && addedChar.Equals('0')) || addedChar.Equals('-'))
        {
            return '\0';
        }
        else
        {
            return addedChar;
        }
    }

    public void ImportSettings()
    {
        /*
         * TODO
         * 
         * string[] path = StandaloneFileBrowser.OpenFilePanel("Select a file to import", "", "json", false);
        if (path.Length > 0 && path[0] != null)
        {
            string json = File.ReadAllText(path[0]);
            GlobalSettingsModel model = JsonUtility.FromJson<GlobalSettingsModel>(json);
            JsonUtility.FromJsonOverwrite(json, ApplicationManager.instance.GlobalSettingsModel);
            Debug.Log("X: " + ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.ResolutionX);
        }*/
    }

    public void ExportSettings()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Select a folder for export", "", "settings.json", "json");
        if (path != null)
        {
            string model = JsonUtility.ToJson(ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.ResolutionY);
            System.IO.File.WriteAllText(path, model);
        }
    }

    public void GenerateImages()
    {
        OnGenerateImages();
    }

    public void SwitchScene()
    {
        ApplicationManager.instance.LoadEditorScene();
    }

    public void ChangeInvalidateMessage(string parameter, bool active)
    {
        switch (parameter)
        {
            case Constants.GeneralParameter.TARGET_FOLDER:
                warningTargetFolder.gameObject.SetActive(active);
                break;
            case Constants.GeneralParameter.AMOUNT_OF_IMAGES:

                break;
        }
    }

    public void SelectTargetFolder()
    {
        string[] path = StandaloneFileBrowser.OpenFolderPanel("Select a target folder", "", false);
        if (path.Length == 1)
        {
            TargetFolderField.text = path[0];
            StoreTargetFolder();
        }
    }

    public void ShowQuitDialog()
    {
        quitDialog.SetActive(true);
    }

    public void HideQuitDialoge()
    {
        quitDialog.SetActive(false);
    }

    public void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void TestDontDestry()
    {
        GameObject[] manager = GameObject.FindGameObjectsWithTag("Manager");
        Debug.Log("count: " + manager.Length);
    }

    public void StoreRandomizationSeed()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.RANDOMIZATION_SEED);
        args.Value = int.Parse(RandomizationField.text);
        OnStoreParameter(this, args);
    }

    public void StoreResolutionX()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.RESOLUTION_X);
        args.Value = ResolutionXField.text;
        OnStoreParameter(this, args);
    }

    public void StoreResolutionY()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.RESOLUTION_Y);
        args.Value = ResolutionYField.text;
        OnStoreParameter(this, args);
    }

    public void StoreAmountOfImages()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.AMOUNT_OF_IMAGES);
        args.Value = AmountOfImagesField.text;
        OnStoreParameter(this, args);
    }

    public void StoreTargetFolder()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.TARGET_FOLDER);
        args.Value = TargetFolderField.text;
        OnStoreParameter(this, args);
    }

    public void StoreColorImage()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.COLOR_IMAGE);
        args.Value = ColorImageToggle.isOn;
        OnStoreParameter(this, args);
    }
    public void StoreDepthImage()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.DEPTH_IMAGE);
        args.Value = DepthImageToggle.isOn;
        OnStoreParameter(this, args);
    }
    public void StoreSegmentationImage()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.SEGMENTATION_IMAGE);
        args.Value = SegmentationImageToggle.isOn;
        OnStoreParameter(this, args);
    }

    public void StoreOnlyFullyCoveredObjects()
    {
        var args = new StoreParameterEventArgs(Constants.GeneralParameter.ONLY_FULLY_COVERED_OBJECTS);
        args.Value = OnlyFullyCoveredObjectsToggle.isOn;
        OnStoreParameter(this, args);
    }
}
