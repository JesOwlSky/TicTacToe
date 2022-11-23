using System;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    private InputRouter _router;
    private Dictionary<Vector2, Sign> _cells = new();

    public void Init(InputRouter router)
    {
        FillField();
        _router = router;
        _router.OnInputRouted += UpdadeCell;
    }

    public Dictionary<Vector2, Sign> Cells => _cells;

    public event Action<Vector2, Sign> OnFieldChanged;

    public void StopInput()
    {
        _router.OnInputRouted -= UpdadeCell;
    }

    private void FillField()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector2 position = new(i, j);
                _cells.Add(position, Sign.Empty);
            }
        }
    }

    private void UpdadeCell(Vector2 position, Sign sign)
    {
        if (_cells[position] == Sign.Empty)
        {
            _cells[position] = sign;
            OnFieldChanged?.Invoke(position, sign);
        }
    }
}
