using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public interface IGeneralParameterView
{
    event Action OnGenerateImages;

    event EventHandler<StoreParameterEventArgs> OnStoreParameter;

    void ChangeInvalidateMessage(string parameter, bool active);
    void Init();

    InputField TargetFolderField { get; set; }
    InputField ResolutionXField { get; set; }
    InputField ResolutionYField { get; set; }
    InputField AmountOfImagesField { get; set; }
    InputField RandomizationField { get; set; }
    Toggle ColorImageToggle { get; set; }
    Toggle DepthImageToggle { get; set; }
    Toggle SegmentationImageToggle { get; set; }
    Toggle OnlyFullyCoveredObjectsToggle { get; set; }
    void HideLoadingScreen();
    void ShowLoadingScreen();
}
