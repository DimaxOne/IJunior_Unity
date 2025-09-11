using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Spawner), typeof(ColorChanger), typeof(Explosion))]
public class ClickHendler : MonoBehaviour
{
    [SerializeField] private Raycast _raycast;

    private Spawner _spawner;
    private ColorChanger _colorChanger;
    private Explosion _explosion;

    private void OnEnable()
    {
        _raycast.OnCubeFound += BlowUp;
    }

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _colorChanger = GetComponent<ColorChanger>();
        _explosion = GetComponent<Explosion>();
    }

    private void OnDisable()
    {
        _raycast.OnCubeFound -= BlowUp;
    }

    private void BlowUp(Cube cube)
    {
        if(TrySeparation(cube) == false)
        {
            cube.gameObject.SetActive(false);
            return;
        }

        List<Cube> cubes = _spawner.GetCubes(cube);

        foreach (Cube newCube in cubes)
        {
            _colorChanger.ChangeColor(newCube);
        }

        _explosion.Push(cubes);
    }

    private bool TrySeparation(Cube cube)
    {
        int maximumRandomValue = 100;
        int minimumRandomValue = 0;

        bool isSeparation = cube.ChanceSeparation >= Random.Range(minimumRandomValue, maximumRandomValue);

        return isSeparation;
    }
}
