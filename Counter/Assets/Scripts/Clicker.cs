using System;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    const int IndexButton = 0;

    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexButton))
            Clicked?.Invoke();
    }
}