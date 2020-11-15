using TMPro;
using UnityEngine;
using UnityEngine.UI;
internal class DisplayItemViewFactory
{
    public DisplayItemViewFactory()
    {
    }

    public DisplayItemView CreateDisplayItemView<T>(T model, int index, string parentName) where T : AbstractItemModel
    {
        var prefab = Resources.Load<GameObject>("Views/DisplayItemView");
        var parent = GameObject.Find(parentName);
        if(parent == null)
        {
            throw new MissingReferenceException("Parent " + parentName + " not found");
        }

        GameObject viewObject = Object.Instantiate(prefab,parent.transform);        
        GameObject preview = new GameObject();

        model.CreateDisplayPreview(preview);
        preview.layer = LayerMask.NameToLayer("UI Model");
        preview.transform.SetParent(viewObject.transform.GetChild(1));
        if(model is TextureItemModel || model is AbstractLightItemModel || model is CameraItemModel)
        {
            Image image = preview.GetComponent<Image>();
            if (image != null)
            {
                image.rectTransform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if(model is ModelItemModel || model is RegionItemModel)
        {
            Vector2 panelSize = viewObject.transform.GetChild(1).GetComponent<RectTransform>().rect.size;
            Vector3 previewSize = preview.GetComponent<BoxCollider>().size;
            previewSize.Scale(preview.transform.localScale);
            float diffX = (panelSize.x - 10) / previewSize.x;
            float diffY = (panelSize.y - 10) / previewSize.y;
            preview.transform.localScale *= Mathf.Min(diffX, diffY);
        }

       // <EXTENSION> Add adjustment of the preview
        preview.transform.localPosition = Vector3.zero;

        var view = viewObject.GetComponent<DisplayItemView>();
        view.InitView(model.DisplayName, preview.transform.parent.gameObject, index);
        
        parent.GetComponentInParent<CustomScrollRect>().OnScrolled += view.SetPreviewActive;

        return view;
    }
}