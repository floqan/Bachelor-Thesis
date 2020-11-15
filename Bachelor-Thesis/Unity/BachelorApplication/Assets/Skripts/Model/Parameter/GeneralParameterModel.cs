using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GeneralParameterModel: AbstractParameterModel {

    public int AmountOfImages { get; set; }
    public int ResolutionX { get; set; }
    public int ResolutionY { get; set; }
    public string TargetFolder { get; set; }
    public int? RandomizationSeed { get; set; }
    public bool ColorPictures { get; set; }
    public bool DepthPictures { get; set; }
    public bool SegmentationPicture { get; set; }
    public bool OnlyFullyCoveredObjects { get; set; }
    public GeneralParameterModel()
    {
        ColorPictures = true;
        DepthPictures = true;
        SegmentationPicture = true;
    }
}
