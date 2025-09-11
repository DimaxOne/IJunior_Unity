using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Separator), typeof(ColorChanger))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _force = 3f;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private int _maximumRandomCubes = 6;
    [SerializeField] private int _minimumRandomCubes = 2;

    private List<GameObject> _objectsForExplosions = new List<GameObject>();

    private Vector3 _currentScale;
    private Separator _separator;
    private ColorChanger _colorChanger;

    private void Start()
    {
        _currentScale = transform.localScale;
        _separator = GetComponent<Separator>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void BlowUp()
    {
        TryCreateCubes();
        Push();
        gameObject.SetActive(false);
    }

    private void TryCreateCubes()
    {
        bool isSeparation = _separator.TrySeparation();

        if (isSeparation == false)
        {
            gameObject.SetActive(false);
            return;
        }

        int count = Random.Range(_minimumRandomCubes, _maximumRandomCubes + 1);
        float radiusSpawn = 0.25f;
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * radiusSpawn;
        Vector3 localScale = _currentScale / 2;

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);

            Separator separator = newCube.GetComponent<Separator>();
            separator.InheritProbabilityValue(_separator.ChanceSeparation);

            Renderer renderer = newCube.GetComponent<Renderer>();
            renderer.material.color = _colorChanger.GetRandomColor();
            newCube.transform.localScale = localScale;

            _objectsForExplosions.Add(newCube);
        }
    }

    private void Push()
    {
        float offsetPointExplosion = 0.5f;

        foreach (GameObject gameObject in _objectsForExplosions)
        {
            if (gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 pointExplosion = transform.position + Vector3.down * offsetPointExplosion;

                if (pointExplosion.y <= 0.01f)
                    pointExplosion.y += 0.05f;

                rigidbody.AddExplosionForce(_force, pointExplosion, _radius);
            }    
        }
    }
}