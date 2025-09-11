using System;
using UnityEngine;

public class UserReader : MonoBehaviour
{
    public const int IndexButton = 0;

    public event Action OnMouseClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(IndexButton))
            OnMouseClicked?.Invoke();
    }
}