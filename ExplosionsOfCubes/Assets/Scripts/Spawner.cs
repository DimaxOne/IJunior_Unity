using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _maximumRandomCubes = 6;
    [SerializeField] private int _minimumRandomCubes = 2;
    [SerializeField] private float _radius = 0.25f;

    private float _divisor = 2f;

    public List<Cube> GetCubes(Cube cube)
    {
        List<Cube> cubes = new List<Cube>();
        Cube parentCube = cube;
        cube.gameObject.SetActive(false);

        CreatedCubes(parentCube, cubes);

        return new List<Cube>(cubes);
    }

    public void CreatedCubes(Cube initial—ube, List<Cube> cubes)
    {
        int count = Random.Range(_minimumRandomCubes, _maximumRandomCubes + 1);

        Vector3 spawnPosition = initial—ube.transform.position + Random.insideUnitSphere * _radius;
        Vector3 localScale = initial—ube.transform.localScale / _divisor;

        for (int i = 0; i < count; i++)
        {
            Cube Òube = Instantiate(_prefab, spawnPosition, Quaternion.identity);

            Cube separator = Òube.GetComponent<Cube>();
            separator.InheritProbabilityValue(initial—ube.ChanceSeparation);

            Òube.transform.localScale = localScale;

            cubes.Add(Òube);
        }
    }
}