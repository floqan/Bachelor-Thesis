using UnityEngine;
using System.Collections.Generic;
using System;

public class EditorModel
{
    public List<AbstractSceneObjectModel> ObjectsInScene { get; set; }
    public AllItemsModel<ModelItemModel> AllModelItemsModel { get; set; }
    public AllItemsModel<RegionItemModel> AllRegionItemsModel { get; set; }
    public AllItemsModel<TextureItemModel> AllTextureItemsModel { get; set; }
    public AllItemsModel<AbstractLightItemModel> AllLightItemsModel { get; set; }
    public AllItemsModel<CameraItemModel>AllCameraItemsModel { get; set;}
    public bool DefaultSunDeleted { get; set; }

    // <TO EXTEND> Create new ItemModel Type and add new AllXXXModel and initialize in constructor

    public EditorModel()
    {
        ObjectsInScene = new List<AbstractSceneObjectModel>();
        AllModelItemsModel = new AllItemsModel<ModelItemModel>();
        AllRegionItemsModel = new AllItemsModel<RegionItemModel>();
        AllTextureItemsModel = new AllItemsModel<TextureItemModel>();
        AllLightItemsModel = new AllItemsModel<AbstractLightItemModel>();
        AllCameraItemsModel = new AllItemsModel<CameraItemModel>();
    }

    internal void AddSceneObject(AbstractSceneObjectModel sceneObject)
    {
        ObjectsInScene.Add(sceneObject);
    }
}
