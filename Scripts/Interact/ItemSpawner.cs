using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] findItems;
    [SerializeField] private Transform[] positionItems;

    public int randomItem;
    private int randomPosition;

    private void Awake()
    {
        randomItem = Random.Range(0, findItems.Length);
        randomPosition = Random.Range(0, positionItems.Length);     
    }

    private void Start()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        //findItems[randomItem].gameObject.SetActive(true);
        //findItems[randomItem].transform.position = positionItems[randomItem].position;
        //Debug.Log("ItemSpawn: "+ findItems[randomItem].name);
    }
}
