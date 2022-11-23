using UnityEngine;

public class FieldUI : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;

    [SerializeField] private float _cellOffset = 2.7f;

    public void FillUIField()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector2 position = new Vector2(i, j) * _cellOffset;
                Instantiate(_cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    public void UpdateCell(Vector2 position, Sign sign)
    {
        Vector3 temp = position * _cellOffset;
        foreach (Transform cell in transform)
        {
            if (cell.position == temp)
            {
                cell.GetComponent<CellDraw>().ChangeSprite(sign);
            }
        }
    }
}