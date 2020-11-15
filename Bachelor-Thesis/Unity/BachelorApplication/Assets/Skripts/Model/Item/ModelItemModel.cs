using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelItemModel : AbstractItemModel
{
    
    public Mesh Mesh {get; set;}
    public Material[] Materials { get; set; }

    public override GameObject ApplyItemProperties(GameObject item)
    {
        InitItem(item);
        ApplyOutline(item);
        item.tag = "Model";
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        InitItem(preview);
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new ModelSceneObjectModel(this);
    }

    private void InitItem(GameObject item)
    {
        item.name = DisplayName;
        item.AddComponent<MeshFilter>();
        item.AddComponent<MeshRenderer>();
        item.GetComponent<MeshFilter>().mesh = Mesh;
        item.GetComponent<MeshRenderer>().materials = Materials;
        item.AddComponent<BoxCollider>();
    }
}
