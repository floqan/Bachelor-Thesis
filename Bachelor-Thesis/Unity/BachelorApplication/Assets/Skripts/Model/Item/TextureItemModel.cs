using UnityEngine;
using UnityEngine.UI;

public class TextureItemModel : AbstractItemModel
{
    public Material Material;
    public Sprite Preview;
    //public Texture2D ColorTexture { get; set; }
    //public Texture2D NormalMap { get; set; }
    //public Texture2D MetallicMap { get; set; } 

    public TextureItemModel()
    {
        Material = new Material(Shader.Find("Standard (Specular setup)"));
    }

    public override GameObject ApplyItemProperties(GameObject item)
    {
        Color itemColor = item.GetComponent<Renderer>().material.color;
        item.GetComponent<Renderer>().material = Material;
        item.GetComponent<Renderer>().material.color = itemColor;
        /*
        material.EnableKeyword("_METALLICGLOSSMAP");
        material.DisableKeyword("_EMISSION");

        material.SetTexture("_MetallicGlossMap", MetallicMap);
        material.SetFloat("_Metallic", 0.2f);
        */
        return item;
    }

    public override void CreateDisplayPreview(GameObject preview)
    {
        preview.name = DisplayName;
        Image image = preview.AddComponent<Image>();
        image.rectTransform.anchorMin = new Vector2(0, 0);
        image.rectTransform.anchorMax = new Vector2(1, 1);
        image.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
        if (Preview != null)
        {
            image.sprite = Preview;
        }
        else
        {

        }
    }

    public void SetColorMap(Texture2D colorMap)
    {
        Material.mainTexture = colorMap;
        Preview = Sprite.Create(colorMap, new Rect(0, 0, colorMap.width, colorMap.height), new Vector2(0.5f, 0.5f));
    }

    public void SetNormalMap(Texture2D normalMap)
    {
        Material.EnableKeyword("_NORMALMAP");
        Material.SetTexture("_BumpMap", normalMap);
    }

    public override AbstractSceneObjectModel CreateSceneObjectModel()
    {
        return new TextureSceneObjectModel(this);
    }
}
