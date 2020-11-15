using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform toolTipDialog;

    private void Start()
    {
        toolTipDialog.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipDialog.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipDialog.gameObject.SetActive(false);
    }
}
