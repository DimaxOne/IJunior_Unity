using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _maximumRandomCubes = 6;
    [SerializeField] private int _minimumRandomCubes = 2;
    [SerializeField] private float _radius = 0.25f;

    public List<Cube> GetCubes(Cube cube)
    {
        List<Cube> cubes = new List<Cube>();
        Cube parentCube = cube;
        cube.gameObject.SetActive(false);

        CreatedCubes(parentCube, cubes);

        return new List<Cube>(cubes);
    }

    public void CreatedCubes(Cube cube, List<Cube> cubes)
    {
        int count = Random.Range(_minimumRandomCubes, _maximumRandomCubes + 1);

        Vector3 spawnPosition = cube.transform.position + Random.insideUnitSphere * _radius;
        Vector3 localScale = cube.transform.localScale / 2;

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);

            Cube separator = newCube.GetComponent<Cube>();
            separator.InheritProbabilityValue(cube.ChanceSeparation);

            newCube.transform.localScale = localScale;

            cubes.Add(newCube);
        }
    }
}