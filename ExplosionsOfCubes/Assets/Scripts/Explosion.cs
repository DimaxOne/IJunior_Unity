using UnityEngine;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force = 400f;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _offsetPointExplosion = 2;

    public void Push(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 pointExplosion = transform.position + Vector3.down * _offsetPointExplosion;

                if (pointExplosion.y <= 0.01f)
                    pointExplosion.y += 0.05f;

                rigidbody.AddExplosionForce(_force, pointExplosion, _radius);
            }    
        }
    }
}