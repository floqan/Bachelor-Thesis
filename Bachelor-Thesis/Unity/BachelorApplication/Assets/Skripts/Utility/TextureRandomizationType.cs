using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomizationType : RandomizationType
{

    public override object GetRandomValue()
    {
        int counter = ApplicationManager.instance.GlobalSettingsModel.EditorModel.AllTextureItemsModel.Length();
        int index = GetRandomizer().Next(1, counter);
        return ApplicationManager.instance.GlobalSettingsModel.EditorModel.AllTextureItemsModel.Get(index).Material;

    }

    public override List<object> GetValues()
    {
        //No representation of the images therefore this method is not needed
        return null;
    }

    public override void SetValues(List<object> list = null)
    {
        //Uses the importet materials therefore no values have to be set
    }
}
