using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractItemModel
{
    public string DisplayName { get; set; }

    //Apply the corresponding properties of the ItemModel onto the given GameObject and apply an outline, if needed
    public abstract GameObject ApplyItemProperties(GameObject item);
    //Create a preview that is shown in the item tab
    public abstract void CreateDisplayPreview(GameObject preview);
    //Return the model for the a new sceneObject
    public abstract AbstractSceneObjectModel CreateSceneObjectModel();

    public void ApplyOutline(GameObject item)
    {
        Outline outline = item.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineWidth = 5;
        outline.OutlineColor = new Color(0, 255, 255f);
    }
}
