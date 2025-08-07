using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationalSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationalSpeed * Time.deltaTime);
    }
}
