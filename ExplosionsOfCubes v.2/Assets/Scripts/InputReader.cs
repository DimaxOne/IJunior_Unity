using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const int IndexButton = 0;

    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexButton))
            Clicked?.Invoke();
    }
}