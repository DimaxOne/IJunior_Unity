using System;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public event Action Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Clicked?.Invoke();
    }
}