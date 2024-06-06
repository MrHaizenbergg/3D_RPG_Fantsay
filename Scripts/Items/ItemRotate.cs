using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    [SerializeField] private float speedRotate = 0.2f;

    private void Update()
    {
        transform.Rotate(0, 0, speedRotate);
    }
}
