using UnityEngine;
using UnityEngine.UI;
using YG;

public class UnlockedItemsMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsInScene;
    [SerializeField] private Text amountOfItems;

    private int unlockedItems;

    private void Start()
    {
        //YandexGame.savesData.unlockItems[3] = true;
        UpdateShowItems();
    }

    private void UpdateShowItems()
    {
        for (int i = 0; i < itemsInScene.Length; i++)
        {
            if (YandexGame.savesData.unlockItems[i] == true)
            {
                itemsInScene[i].gameObject.SetActive(true);
                unlockedItems++;
            }
            else
                itemsInScene[i].gameObject.SetActive(false);
        }

        amountOfItems.text = unlockedItems + " / " + 8;
    }
}
