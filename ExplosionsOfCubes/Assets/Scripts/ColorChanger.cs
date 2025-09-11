using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color GetRandomColor()
    {
        return Random.ColorHSV();
    }
}
