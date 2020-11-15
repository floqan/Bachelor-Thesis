using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraItemModel : AbstractItemModel
{
    public CameraItemModel()
    {
        DisplayName = "Camera";
    }

    public override GameObject ApplyItemProperties(GameObject item)
    {
        GameObject.Destroy(item);
        item = Object.Instantiate(Resources.Load<GameObject>("Camera/SceneCamera"));
        item.name = DisplayName;
        ApplyOutline(item);
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        preview.name = DisplayName + "_Preview";
        Image image = preview.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Preview/Camera");
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new CameraSceneObjectModel(this);
    }
}
