using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private Vector3 _maximumScale;

    private Vector3 _initialSize;
    private Vector3 _currentScale;
    private Vector3 _targetScale;
    private bool _isIncrease;

    private void Start()
    {
        _initialSize = transform.localScale;
        _isIncrease = true;
    }

    private void Update()
    {
        _currentScale = transform.localScale;
        _targetScale = _isIncrease ? _maximumScale : _initialSize;

        transform.localScale = Vector3.MoveTowards(_currentScale, _targetScale, _scaleSpeed * Time.deltaTime);

        if (_currentScale == _targetScale)
            _isIncrease = !_isIncrease;
    }
}
