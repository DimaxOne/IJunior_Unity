using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Spawner), typeof(ColorChanger), typeof(Exploder))]
public class ClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycast;

    private Spawner _spawner;
    private ColorChanger _colorChanger;
    private Exploder _explosion;

    private void OnEnable()
    {
        _raycast.FoundCube += CreateExplosion;
    }

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _colorChanger = GetComponent<ColorChanger>();
        _explosion = GetComponent<Exploder>();
    }

    private void OnDisable()
    {
        _raycast.FoundCube -= CreateExplosion;
    }

    private void CreateExplosion(Cube cube)
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