using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour
{
    // Event callbacks
    public UnityEvent<Item> onItemUse;

    // flag to tell ItemSlot it needs to update itself after being changed
    private bool b_needsUpdate = true;

    // Declared with auto-property
    public Item ItemInSlot { get; private set; }
    public int ItemCount { get; private set; }

    public Item tempItem;

    // scene references
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    private Image itemIcon;

    public GameObject player;

    public ItemType itemType;

    private void Update()
    {
        if(b_needsUpdate)
        {
            UpdateSlot();
        }
    }

    /// <summary>
    /// Returns true if there is an item in the slot
    /// </summary>
    /// <returns></returns>
    public bool HasItem()
    {
        return ItemInSlot != null;
    }

    /// <summary>
    /// Removes everything in the item slot
    /// </summary>
    /// <returns></returns>
    public void ClearSlot()
    {
        ItemInSlot = null;
        b_needsUpdate = true;
    }

    /// <summary>
    /// Attempts to remove a number of items. Returns number removed
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public int TryRemoveItems(int count)
    {
        if(count > ItemCount)
        {
            int numRemoved = ItemCount;
            ItemCount -= numRemoved;
            b_needsUpdate = true;
            return numRemoved;
        } else
        {
            ItemCount -= count;
            b_needsUpdate = true;
            return count;
        }
    }

    /// <summary>
    /// Sets what is contained in this slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void SetContents(Item item, int count)
    {
        ItemInSlot = item;
        ItemCount = count;
        b_needsUpdate = true;
    }

    /// <summary>
    /// Activate the item currently held in the slot
    /// </summary>
    public void UseItem()
    {
        if(itemType == ItemType.INVENTORY)
        {
            if (ItemInSlot != null)
            {
                if (ItemCount >= 1)
                {
                    if (player.GetComponent<MousePickUp>().ItemInSlot != null)
                    {
                        tempItem = ItemInSlot;
                        ItemInSlot = player.GetComponent<MousePickUp>().ItemInSlot;
                        player.GetComponent<MousePickUp>().ItemInSlot = tempItem;

                        if(ItemInSlot == player.GetComponent<MousePickUp>().ItemInSlot)
                        {
                            ItemCount += player.GetComponent<MousePickUp>().ItemCount;
                            player.GetComponent<MousePickUp>().ItemInSlot = null;
                        }

                    }
                    else
                    {
                        player.GetComponent<MousePickUp>().ItemInSlot = ItemInSlot;
                        player.GetComponent<MousePickUp>().ItemCount = ItemCount;

                        ClearSlot();
                    }
                    b_needsUpdate = true;
                }
            }
            else
            {
                SetContents(player.GetComponent<MousePickUp>().ItemInSlot, player.GetComponent<MousePickUp>().ItemCount);

                player.GetComponent<MousePickUp>().ItemClear();
            }
        }
        else if (itemType == ItemType.CRAFTING)
        {
            if (ItemInSlot != null)
            {
                if (ItemCount >= 1)
                {
                    if (player.GetComponent<MousePickUp>().ItemInSlot != null)
                    {
                        if(ItemInSlot != player.GetComponent<MousePickUp>().ItemInSlot)
                        {
                            tempItem = ItemInSlot;
                            ItemInSlot = player.GetComponent<MousePickUp>().ItemInSlot;
                            player.GetComponent<MousePickUp>().ItemInSlot = tempItem;

                            int tempCount;
                            tempCount = ItemCount;
                            ItemCount = player.GetComponent<MousePickUp>().ItemCount;
                            player.GetComponent<MousePickUp>().ItemCount = tempCount;

                        }
                        else
                        {
                            player.GetComponent<MousePickUp>().mouseCountDecrease();
                            ItemCount++;
                        }
                    }
                    else
                    {
                        player.GetComponent<MousePickUp>().ItemInSlot = ItemInSlot;
                        player.GetComponent<MousePickUp>().ItemCount = ItemCount;

                        ClearSlot();
                    }
                    b_needsUpdate = true;
                }
            }
            else
            {
                if (player.GetComponent<MousePickUp>().ItemInSlot != null)
                {
                    SetContents(player.GetComponent<MousePickUp>().ItemInSlot, 1);
                    player.GetComponent<MousePickUp>().mouseCountDecrease();
                }

               // SetContents(player.GetComponent<MousePickUp>().ItemInSlot, player.GetComponent<MousePickUp>().ItemCount);
               // player.GetComponent<MousePickUp>().ItemClear();
            }
        }
        else if(itemType == ItemType.RESULT)
        {

        }
    }

    /// <summary>
    /// Update visuals of slot to match items contained
    /// </summary>
    private void UpdateSlot()
    {
        if(ItemCount == 0)
        {
            ItemInSlot = null;
        }

      if(ItemInSlot != null)
        {
            itemCountText.text = ItemCount.ToString();
            itemIcon.sprite = ItemInSlot.Icon;
            itemIcon.gameObject.SetActive(true);
        } else
        {
            itemIcon.gameObject.SetActive(false);

        }

        b_needsUpdate = false;
    }
}
