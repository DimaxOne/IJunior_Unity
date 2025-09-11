using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _force = 400f;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _offsetPointExplosion = 2;

    private float minimumPositionY = 0.01f;
    private float defaultPositionY = 0.05f;

    public void Push(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 pointExplosion = transform.position + Vector3.down * _offsetPointExplosion;

                if (pointExplosion.y <= minimumPositionY)
                    pointExplosion.y += defaultPositionY;

                rigidbody.AddExplosionForce(_force, pointExplosion, _radius);
            }    
        }
    }
}