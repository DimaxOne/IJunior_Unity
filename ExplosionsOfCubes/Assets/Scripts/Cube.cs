using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanceSeparation = 100;

    public float ChanceSeparation => _chanceSeparation;

    private float _divisor = 2f;

    public void InheritProbabilityValue(float parentChance)
    {
        _chanceSeparation = parentChance / _divisor;
    }
}