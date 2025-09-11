using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _userReader;
    [SerializeField] private LayerMask _invisibleLayerName;

    public event Action<Cube> FoundCube;

    private float _maxDistance = 1000f;

    private Ray _ray;

    private void OnEnable()
    {
        _userReader.ClickedMouse += SearchCubes;
    }

    private void OnDisable()
    {
        _userReader.ClickedMouse -= SearchCubes;
    }

    private void SearchCubes()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, _maxDistance, ~_invisibleLayerName))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                FoundCube?.Invoke(cube);
            }
        }
    }
}