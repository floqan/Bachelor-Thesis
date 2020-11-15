using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DisplayItemView : MonoBehaviour, IDisplayItemView
{
    public GameObject Preview { get; set; }
    public TextMeshProUGUI DisplayNameField { get ; set; }
    public int SizeToHideElements;

    public int ItemIndex { get; set; }

    public event EventHandler<DisplayItemClickedArgs> OnClicked = (sender, e) => { };

    private void Awake()
    {
        DisplayNameField = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void ClearView()
    {
        gameObject.SetActive(false);
        DisplayNameField.text = null;
        Preview = null;
    }

    public void OnItemClicked()
    {
        DisplayItemClickedArgs args = new DisplayItemClickedArgs();
        args.Index = ItemIndex;
        OnClicked(this, args);
    }

    public void InitView(string displayName, GameObject preview, int itemIndex)
    {
        DisplayNameField.text = displayName;
        Preview = preview;
        ItemIndex = itemIndex;
    }

    public void SetPreviewActive(object sender, EventArgs args)
    {
        Vector3 previewPos = GameObject.FindGameObjectWithTag("UI Camera").GetComponent<Camera>().WorldToScreenPoint(Preview.transform.position);
        float maxHeight = (transform.parent.parent.GetComponent<RectTransform>().rect.height);
        if(previewPos.y > maxHeight)
        {
            Preview.SetActive(false);
        }
        else
        {
            Preview.SetActive(true);
        }
    }
}
