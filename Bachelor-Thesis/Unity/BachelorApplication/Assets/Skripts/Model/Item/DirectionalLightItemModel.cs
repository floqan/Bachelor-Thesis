using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalLightItemModel : AbstractLightItemModel
{
    public DirectionalLightItemModel()
    {
        DisplayName = "Directional Light";
    }

    public override GameObject ApplyItemProperties(GameObject item)
    {

        GameObject.Destroy(item);
        item = Object.Instantiate(Resources.Load<GameObject>("Light/Directional Light"));
        item.name = DisplayName;
        ApplyOutline(item);
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        preview.name = DisplayName + "_Preview";
        Image image = preview.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Preview/sun");
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new DirectionalLightSceneObjectModel(this);
    }
}
