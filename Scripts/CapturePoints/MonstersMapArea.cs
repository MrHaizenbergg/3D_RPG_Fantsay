using System;
using UnityEngine;

public class MonstersMapArea : MonoBehaviour
{
    private MapAreaCollider mapAreaColl;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<MapAreaCollider>(out MapAreaCollider mapAreaCollider))
        {
            mapAreaColl = mapAreaCollider;
        }
    }

    private void OnDisable()
    {
        if(mapAreaColl != null)
        {
            mapAreaColl.GetMonstersMapAreasList().Remove(this);
            Debug.Log("MonsterDeleteFromZone");
        }
    }
}
