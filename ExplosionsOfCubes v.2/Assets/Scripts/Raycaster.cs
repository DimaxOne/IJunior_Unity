using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _userReader;
    [SerializeField] private LayerMask _invisibleLayerName;

    public event Action<Cube> FoundCube;

    private float _maxDistance = 1000f;

    private void OnEnable()
    {
        _userReader.Clicked += SearchCubes;
    }

    private void OnDisable()
    {
        _userReader.Clicked -= SearchCubes;
    }

    private void SearchCubes()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, _maxDistance, ~_invisibleLayerName))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                FoundCube?.Invoke(cube);
            }
        }
    }
}