using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject tooltip;
    Item item;

    public void Set(Item item, GameObject tooltip) {
        this.tooltip = tooltip;
        this.item = item;
        this.GetComponent<Image>().sprite = item.sprite;
    }

    void SetTooltip() {
        tooltip.SetActive(true);
        tooltip.GetComponentInChildren<TMPro.TMP_Text>().text = item.description;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltip();
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        if(tooltip.GetComponentInChildren<TMPro.TMP_Text>().text == item.description) {
            tooltip.SetActive(false);
        }
    }
}
