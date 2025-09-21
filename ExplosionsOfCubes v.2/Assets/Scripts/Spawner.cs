using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ColorChanger))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _maximumRandomCubes = 6;
    [SerializeField] private int _minimumRandomCubes = 2;

    private ColorChanger _colorChanger;
    private float _divisor = 2f;

    private void Start()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void CreateCubes(Cube parentCube)
    {
        Cube initialCube = parentCube;
        int count = Random.Range(_minimumRandomCubes, _maximumRandomCubes + 1);

        parentCube.gameObject.SetActive(false);
        List<Vector3> spawnPositions = GetPositions(initialCube, count);
        Vector3 localScale = initialCube.transform.localScale / _divisor;

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Cube cube = Instantiate(_prefab, spawnPositions[i], Quaternion.identity);

            cube.InheritProbabilityValue(initialCube.ChanceSeparation);
            cube.transform.localScale = localScale;
            _colorChanger.ChangeColor(cube);
        }
    }

    private List<Vector3> GetPositions(Cube initialCube, int count)
    {
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> availablePositions = new List<Vector3>();

        Vector3 parentScale = initialCube.transform.localScale;
        int[] possibleShifts = { -1, 1 };
        Vector3 offset = parentScale * 0.25f;

        foreach (int positionX in possibleShifts)
        {
            foreach (int positionY in possibleShifts)
            {
                foreach (int positionZ in possibleShifts)
                {
                    Vector3 localOffset = new Vector3(positionX * offset.x, positionY * offset.y, positionZ * offset.z);
                    Vector3 worldPosition = initialCube.transform.TransformPoint(localOffset);

                    availablePositions.Add(worldPosition);
                }
            }
        }

        for (int i = 0; i < count; i++)
        {
            int minimumRandomValue = 0;

            if(availablePositions.Count > 0)
            {
                int position = Random.Range(minimumRandomValue, availablePositions.Count);

                positions.Add(availablePositions[position]);
                availablePositions.RemoveAt(position);
            }
        }

        return positions;
    }
}