using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public const int IndexButton = 0;

    private string _invisibleLayerName = "InvisibleWall";
    private float _maxDistance = 1000f;

    private int _wallLayer;
    private Ray _ray;

    private void Awake()
    {
        _wallLayer = ~LayerMask.GetMask(_invisibleLayerName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexButton))
            SearchObjects();
    }

    private void SearchObjects()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, _maxDistance, _wallLayer))
        {
            if (hit.collider.TryGetComponent(out Explosion explosion))
            {
                explosion.BlowUp();
            }
        }
    }
}