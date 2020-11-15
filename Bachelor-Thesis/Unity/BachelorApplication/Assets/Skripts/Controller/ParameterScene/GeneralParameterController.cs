using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralParameterController : IGeneralParameterController
{

    public  IGeneralParameterView view;
    private readonly GeneralParameterModel model;

    public GeneralParameterController(GeneralParameterModel model, IGeneralParameterView view)
    {
        this.model = model;
        SubscribeView(view);
    }

    public void UnsubscribeView()
    {
        view.OnGenerateImages -= GenerateImages;
        view.OnStoreParameter -= StoreParameter;
        view = null;
    }

    public void SubscribeView(IGeneralParameterView view)
    {
        this.view = view;
        view.OnGenerateImages += GenerateImages;
        view.OnStoreParameter += StoreParameter;
        ReloadView();
    }
    
    public void StoreParameter(object sender, StoreParameterEventArgs e)
    {
        switch (e.ParameterType)
        {
            case Constants.GeneralParameter.TARGET_FOLDER :
                {
                    model.TargetFolder = (string) e.Value;
                    break;
                }
            case Constants.GeneralParameter.RANDOMIZATION_SEED :
                {
                    ApplicationManager.instance.GlobalSettingsModel.GeneralParameterModel.RandomizationSeed = (int.Parse(e.Value.ToString()));
                    break;
                }
            case Constants.GeneralParameter.AMOUNT_OF_IMAGES:
                {
                    model.AmountOfImages = int.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.RESOLUTION_X:
                {
                    model.ResolutionX = int.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.RESOLUTION_Y:
                {
                    model.ResolutionY = int.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.COLOR_IMAGE:
                {
                    model.ColorPictures = bool.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.DEPTH_IMAGE:
                {
                    model.DepthPictures = bool.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.SEGMENTATION_IMAGE:
                {
                    model.SegmentationPicture = bool.Parse(e.Value.ToString());
                    break;
                }
            case Constants.GeneralParameter.ONLY_FULLY_COVERED_OBJECTS:
                {
                    model.OnlyFullyCoveredObjects = bool.Parse(e.Value.ToString());
                    break;
                }
            default :
                Debug.LogError("GeneralParameterController -> StoreParamter() : Parameter not defined: " + e.ParameterType);
                break;
        
        };
    }

    internal void ShowLoadingScreen()
    {
        view.ShowLoadingScreen();
    }

    internal void HideLoadingScreen()
    {
        view.HideLoadingScreen();
    }

    internal void ReloadView()
    {

        view.Init();
        view.RandomizationField.text = model.RandomizationSeed.ToString(); 
        view.TargetFolderField.text = model.TargetFolder;
        if (model.ResolutionX != 0)
        {
            view.ResolutionXField.text = model.ResolutionX.ToString();
        }
        if(model.ResolutionY != 0)
        {
            view.ResolutionYField.text = model.ResolutionY.ToString();
        }
        if(model.AmountOfImages != 0)
        {
            view.AmountOfImagesField.text = model.AmountOfImages.ToString();
        }
        view.ColorImageToggle.isOn = model.ColorPictures;
        view.DepthImageToggle.isOn = model.DepthPictures;
        view.SegmentationImageToggle.isOn = model.SegmentationPicture;
        view.OnlyFullyCoveredObjectsToggle.isOn = model.OnlyFullyCoveredObjects;
    }

    public void GenerateImages()
    {
        if (ValidateGeneralParameter())
        {
            ApplicationManager.instance.StartGenerateImages();
        };
    }

    private bool ValidateGeneralParameter()
    {
        bool result = true;
        if (model.TargetFolder != null && model.TargetFolder.Length == 0)
        {
            result = false;
            view.ChangeInvalidateMessage(Constants.GeneralParameter.TARGET_FOLDER, true);
        }

        if(model.AmountOfImages <= 0)
        {

        }
        //TODO
        return result;
    }

    public void StoreRandomizationSeed()
    {
        
    }

    public void StoreResolutionX(int resolutionX)
    {
        model.ResolutionX = resolutionX;
    }

    public void StoreResolutionY(int resolutionY)
    {
        model.ResolutionY = resolutionY;
    }

    public void StoreAmountOfImages(int amountOfImages)
    {
        model.AmountOfImages = amountOfImages;
    }

    public void StoreTargetPath(string targetPath)
    {
        model.TargetFolder = targetPath;
    }
}
