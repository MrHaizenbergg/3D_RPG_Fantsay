using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField] private GameObject triangle;
    [SerializeField] private float speedRotate;

    private void Update()
    {
        transform.RotateAround(triangle.transform.position, Vector3.up, speedRotate * Time.deltaTime);
    }
}
