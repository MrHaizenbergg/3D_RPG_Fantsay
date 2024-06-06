using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InventoryRPG_UI : MonoBehaviour
{
    public Transform itemParent;
    public GameObject inventoryUI;
    public GameControll gameControll;

    InventoryRPG inventory;

    InventorySlot[] slots;

    private void Start()
    {
        inventory = InventoryRPG.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
    }
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Inventory"))
        {
            if (inventoryUI.activeSelf)
            {
                gameControll.HideCursor();
                //Time.timeScale = 1;
                inventoryUI.SetActive(false);

            }
            else
            {
                gameControll.ShowCursor();
                //Time.timeScale = 0;
                inventoryUI.SetActive(true);
                //inventoryUI.SetActive(!inventoryUI.activeSelf);
            }
        }
    }
    private void UpdateUI()
    {
        //Debug.Log("UpdatingUI");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
