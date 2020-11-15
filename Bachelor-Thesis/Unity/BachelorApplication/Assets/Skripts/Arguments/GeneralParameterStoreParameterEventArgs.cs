using System;

public class GeneralParameterStoreParameterEventArgs : EventArgs
{
    public string TargetFolder { get; set; }
    public int RandomizationSeed { get; set; }
    public int ResolutionX { get; set; }
    public int ResolutionY { get; set; }
    public int AmountOfImages { get; set; }

}
