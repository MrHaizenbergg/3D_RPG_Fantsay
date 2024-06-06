using UnityEngine;

public class HumansMapArea : MonoBehaviour
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
        if (mapAreaColl != null)
        {
            mapAreaColl.GetHumansMapAreasList().Remove(this);
            //Debug.Log("HumanDeleteFromZone");
        }
    }
}
