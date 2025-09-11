using UnityEngine;

public class Separator : MonoBehaviour
{
    [SerializeField] private float _chanceSeparation = 100;

    public float ChanceSeparation => _chanceSeparation;

    private float _divisor = 2f;

    public void InheritProbabilityValue(float parentChance)
    {
        _chanceSeparation = parentChance / _divisor;
    }

    public bool TrySeparation()
    {
        int maximumRandomValue = 100;
        int minimumRandomValue = 0;

        bool isSeparation = _chanceSeparation >= Random.Range(minimumRandomValue, maximumRandomValue);

        if (isSeparation)
        {
            Debug.Log(_chanceSeparation);
            return true;
        }
        else
        {
            return false;
        }
    }
}