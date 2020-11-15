using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLightItemModel : AbstractLightItemModel
{
    public PointLightItemModel()
    {
        DisplayName = "Point Light";
    }

    public override GameObject ApplyItemProperties(GameObject item)
    {
        GameObject.Destroy(item);
        item = Object.Instantiate(Resources.Load<GameObject>("Light/Point Light"));
        item.name = "Point Light";
        ApplyOutline(item);
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        preview.name = DisplayName + "_Preview";
        Image image = preview.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Preview/lightbulb");
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new PointLightSceneObjectModel(this);
    }
}
