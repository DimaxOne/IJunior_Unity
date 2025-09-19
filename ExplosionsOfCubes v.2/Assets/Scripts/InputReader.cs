using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const int IndexButton = 0;

    public event Action ClickedMouse;

    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexButton))
            ClickedMouse?.Invoke();
    }
}