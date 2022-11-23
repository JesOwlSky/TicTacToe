using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiInput : MonoBehaviour
{
    private Field _field;
    private Winning _winning;
    private Sign _mySign = Sign.Circle;

    public event Action<Vector2> OnAiInput;

    public void Connect(Field field, Winning winning)
    {
        _winning = winning;
        _field = field;
        _field.OnFieldChanged += MakeMove;
    }

    private void MakeMove(Vector2 p, Sign s)
    {
        StartCoroutine(WaitAndMove());
    }

    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(1);
        OnAiInput?.Invoke(ChoseMove(_field.Cells));
    }

    private Vector2 ChoseMove(Dictionary<Vector2, Sign> field)
    {
        List<Vector2> emptyCells = field.Where(x => x.Value == Sign.Empty)
                                        .Select(x => x.Key)
                                        .ToList();

        Dictionary<Vector2, int> weigths = new();
        int topWeigth = int.MinValue;

        foreach ( var cell in emptyCells)
        {
            int weight = Minimax(new Dictionary<Vector2, Sign> (field), cell, _mySign, 1);
            weigths.Add(cell, weight);

            if(weight > topWeigth)
            {
                topWeigth = weight;
            }
        }

        return weigths.First(x => x.Value == topWeigth).Key;
    }

    int Minimax(Dictionary<Vector2, Sign> field, Vector2 move, Sign sign, int depth)
    {
        field[move] = sign;


        if (_winning.CheckConditions(field, Sign.Circle))
        {
            return 100 - depth;
        }
        if (_winning.CheckConditions(field, Sign.Cross))
        {
            return -100 + depth;
        }
        if (!field.Any(x => x.Value == Sign.Empty))
        {
            return 0;
        }

        depth++;

        List<Vector2> emptyCells = field.Where(x => x.Value == Sign.Empty)
                                        .Select(x => x.Key)
                                        .ToList();

        List <int> weigths = new();
        Sign nextSign = _mySign == sign ? Sign.Cross : Sign.Circle;

        foreach (var cell in emptyCells)
        {
            int weight = Minimax(new Dictionary<Vector2, Sign>(field), cell, nextSign, depth);
            weigths.Add(weight);
        }

        return _mySign == nextSign
            ? weigths.Max()
            : weigths.Min();
    }
}