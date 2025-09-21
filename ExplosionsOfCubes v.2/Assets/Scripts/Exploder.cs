using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _force = 200f;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _offsetPointExplosion = 2;
    [SerializeField] private float _forceMultiplier = 0.5f;

    private float minimumPositionY = 0.01f;
    private float defaultPositionY = 0.05f;

    public void Explode(Cube initialCube)
    {
        float size = initialCube.gameObject.transform.localScale.magnitude;

        float force = _force * _forceMultiplier / size;

        UseForce(initialCube, force, _radius / size);
    }

    private List<Cube> GetCubesForExplode(Cube initialCube)
    {
        List<Cube> cubes = new List<Cube>();
        
        Collider[] hitColliders = Physics.OverlapSphere(initialCube.transform.position, _radius / initialCube.transform.localScale.magnitude);

        foreach (var collider in hitColliders)
        {
            if (collider == initialCube.GetComponent<Collider>())
                continue;

            if (collider.TryGetComponent(out Cube cube))
                cubes.Add(cube);
        }

        return cubes;
    }

    private List<Rigidbody> GetRigitdobyCubes(List<Cube> cubes)
    {
        List<Rigidbody> cubesRigidbody = new List<Rigidbody>();

        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody))
            {
                cubesRigidbody.Add(rigidbody);
            }
        }

        return cubesRigidbody;
    }

    private void UseForce(Cube initialCube, float force, float radius)
    {
        List<Cube> cubes = new List<Cube>();
        List<Rigidbody> cubesRigidbody = new List<Rigidbody>();

        cubes = GetCubesForExplode(initialCube);
        cubesRigidbody = GetRigitdobyCubes(cubes);

        foreach (Rigidbody cubeRigidbody in cubesRigidbody)
        {
            Vector3 pointExplosion = transform.position + Vector3.down * _offsetPointExplosion;

            if (pointExplosion.y <= minimumPositionY)
                pointExplosion.y += defaultPositionY;

            cubeRigidbody.AddExplosionForce(force, initialCube.transform.position, radius);
        }
    }
}