using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploder))]
public class ClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycast;

    private Spawner _spawner;
    private Exploder _explosion;

    private void OnEnable()
    {
        _raycast.FoundCube += CreateExplosion;
    }

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
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
            _explosion.Explode(cube);
            cube.gameObject.SetActive(false);
            return;
        }

        _spawner.CreateCubes(cube);
    }

    private bool TrySeparation(Cube cube)
    {
        int maximumRandomValue = 100;
        int minimumRandomValue = 0;

        bool isSeparation = cube.ChanceSeparation >= Random.Range(minimumRandomValue, maximumRandomValue);

        return isSeparation;
    }
}