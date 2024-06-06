using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRPG : MonoBehaviour
{
    public int space = 20;

    public List<ItemRPG> items = new List<ItemRPG>();

    public delegate void OnItemEventHandler(ItemRPG item);
    public static event OnItemEventHandler OnItemCheck;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    #region Singleton
    public static InventoryRPG instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than iventroyRPG found");
            return;
        }
        instance = this;
    }
    #endregion

    public static void ItemCheck(ItemRPG item)
    {
        if (OnItemCheck != null)
            OnItemCheck(item);
    }

    public bool Add(ItemRPG item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enought room.");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

            ItemCheck(item);
        }

        return true;
    }

    public void Remove(ItemRPG item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
