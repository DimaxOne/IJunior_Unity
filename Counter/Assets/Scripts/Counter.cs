using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Clicker _clicker;
    [SerializeField] private float _timeToWait;
    [SerializeField] private int _step;

    private int _counterValue;
    private bool _is—ountdown;

    private void OnEnable()
    {
        _clicker.Clicked += ChangeValue;
    }

    private void Start()
    {
        _counterValue = 0;
        _is—ountdown = false;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= ChangeValue;
    }

    private void ChangeValue()
    {
        if (_is—ountdown)
            StopCoroutine(ChangeCounterValue());

        _is—ountdown = !_is—ountdown;
        
        if(_is—ountdown)
            StartCoroutine(ChangeCounterValue());
    }

    private IEnumerator ChangeCounterValue()
    {
        var timeToWait = new WaitForSeconds(_timeToWait);

        while(_is—ountdown)
        {
            yield return timeToWait;
            _counterValue++;
            _text.text = _counterValue.ToString("");
        }
    }
}