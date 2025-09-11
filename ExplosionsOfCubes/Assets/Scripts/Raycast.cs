using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private UserReader _userReader;

    public event Action<Cube> OnCubeFound;

    private string _invisibleLayerName = "InvisibleWall";
    private float _maxDistance = 1000f;

    private int _wallLayer;
    private Ray _ray;

    private void Awake()
    {
        _wallLayer = ~LayerMask.GetMask(_invisibleLayerName);
    }

    private void OnEnable()
    {
        _userReader.OnMouseClicked += SearchCubes;
    }

    private void OnDisable()
    {
        _userReader.OnMouseClicked -= SearchCubes;
    }

    private void SearchCubes()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, _maxDistance, _wallLayer))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                OnCubeFound?.Invoke(cube);
            }
        }
    }
}