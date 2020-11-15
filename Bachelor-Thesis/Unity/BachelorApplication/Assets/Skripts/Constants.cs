
public static class Constants 
{ 
    public static class GeneralParameter
    {
        public const string TARGET_FOLDER = "Target folder";
        public const string RESOLUTION_X = "Resolution x";
        public const string RESOLUTION_Y = "Resolution y";
        public const string AMOUNT_OF_IMAGES = "Amount of images";
        public const string RANDOMIZATION_SEED = "Randomization seed";
        public const string COLOR_IMAGE = "Color image";
        public const string DEPTH_IMAGE = "Depth image";
        public const string SEGMENTATION_IMAGE = "Segmentation image";
        public const string ONLY_FULLY_COVERED_OBJECTS = "Only fully covered objects";
    }

    public static class ImportPaths
    {
        public const string MODElS_PATH = "";
        public const string TEXTURES_PATH = "";
        public const string REGION_PATH = "";
    }

    public static class SceneObjectParameter
    {
        //Model
        public const string POSITION = "Position";
        public const string ROTATION = "Rotation";
        public const string SCALE = "Scale";
        public const string TEXTURE = "Texture";
        public const string OBJECT_COLOR = "Object Color";

        //Lightning
        public const string LIGHTNING_INTENSITY = "Intensity";
        public const string LIGHTNING_RANGE = "Range";
        public const string LIGHTNING_COLOR = "Color";
        public const string LIGHTNING_SPOT_ANGLE = "Spot Angle";


        // <TO EXTEND> Add new Parameter Constant
    }

}
