using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePickUp : MonoBehaviour
{
    // Declared with auto-property
    public Item ItemInSlot { get;  set; }
    public int ItemCount { get;  set; }

    // scene references
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    private Image itemIcon;

    public Canvas canvas;

     void Update()
    {
        if (ItemInSlot != null)
        {
            itemCountText.text = ItemCount.ToString();
            itemIcon.sprite = ItemInSlot.Icon;
            itemIcon.gameObject.SetActive(true);

            // item follows mouse cursor
            Vector2 movePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out movePosition);
            itemIcon.transform.position = canvas.transform.TransformPoint(movePosition);
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }
    }

    public void mouseCountDecrease()
    {
        ItemCount--;
        if(ItemCount == 0)
        {
            ItemInSlot = null;
        }
    }


    public void ItemClear()
    {
        ItemInSlot = null;
    }
}
