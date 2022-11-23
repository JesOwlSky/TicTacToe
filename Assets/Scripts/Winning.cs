using System;
using System.Collections.Generic;
using UnityEngine;

public class Winning
{
    private int _moveNumber = 0;
    private readonly Field _field;

    public event Action OnEndGame;

    public Winning(Field field)
    {
        _field = field;
        _field.OnFieldChanged += Analyze;
    }

    public bool CheckConditions(Dictionary<Vector2, Sign> cells , Sign sign)
    {
        foreach (Vector2[] condition in _conditions)
        {
            if (cells[condition[0]] == sign &&
                cells[condition[1]] == sign &&
                cells[condition[2]] == sign)
            {
                return true;
            }
        }
        return false;
    }

    private void Analyze(Vector2 position, Sign sign)
    {
        if (_moveNumber < 9)
        {
            if (CheckConditions(_field.Cells, sign))
            {
                OnEndGame?.Invoke();
                _field.StopInput();
                Debug.Log(sign + " is won");
            }
        }
        else
        {
            _field.StopInput();
            Debug.Log("Draw");
        }
        _moveNumber++;
    }

    private readonly Vector2[][] _conditions = {
        new[]{new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) },
        new[]{new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1) },
        new[]{new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1) },
        new[]{new Vector2(-1, -1), new Vector2(0, 0), new Vector2(1, 1) },
        new[]{new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, -1) },
        new[]{new Vector2(0, -1), new Vector2(0, 0), new Vector2(0, 1) },
        new[]{new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0) },
        new[]{new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1) },
    };
}
