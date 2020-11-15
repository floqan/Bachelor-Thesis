using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotLightItemModel : AbstractLightItemModel
{
    public SpotLightItemModel()
    {
        DisplayName = "Spot Light";
    }

    public override GameObject ApplyItemProperties(GameObject item)
    {
        Object.Destroy(item);
        item = Object.Instantiate(Resources.Load<GameObject>("Light/Spot Light"));
        item.name = DisplayName;
        ApplyOutline(item);
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        preview.name = DisplayName + "_Preview";
        Image image = preview.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Preview/Spot Light");
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new SpotLightSceneObjectModel(this);
    }
}
