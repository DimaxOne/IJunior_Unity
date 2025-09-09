using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private Clicker _clicker;
    [SerializeField] private float _timeToWait;
    [SerializeField] private int _step;

    public event Action<int> ValueChanged;

    private bool _isCountdown;
    private int _counterValue;

    private void OnEnable()
    {
        _clicker.Clicked += ChangeValue;
    }

    private void Start()
    {
        _counterValue = 0;
        _isCountdown = false;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= ChangeValue;
    }

    private void ChangeValue()
    {
        if (_isCountdown)
            StopCoroutine(ChangeCounterValue());

        _isCountdown = !_isCountdown;
        
        if(_isCountdown)
            StartCoroutine(ChangeCounterValue());
    }

    private IEnumerator ChangeCounterValue()
    {
        var timeToWait = new WaitForSeconds(_timeToWait);

        while(_isCountdown)
        {
            yield return timeToWait;
            _counterValue++;
            ValueChanged?.Invoke(_counterValue);
        }
    }
}