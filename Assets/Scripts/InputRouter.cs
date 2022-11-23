using System;
using UnityEngine;

public class InputRouter
{
    private readonly MoveOrder _moveOrder;

    public event Action<Vector2, Sign> OnInputRouted;
        
    public InputRouter(MoveOrder moveOrder, FieldUI uiInput, AiInput aiInput)
    {
        _moveOrder = moveOrder;

        foreach (Transform cell in uiInput.transform)
        {
            cell.GetComponent<CellInput>().OnPlayerInput += PlayerInput;
        }

        aiInput.OnAiInput += AiInput;
    }

    private void PlayerInput(Vector2 position)
    {
        if (_moveOrder.IsActivePlayer)
        {
            OnInputRouted?.Invoke(position, _moveOrder.ActiveUser.Sign);
        }
    }

    private void AiInput(Vector2 position)
    {
        if (!_moveOrder.IsActivePlayer)
        {
            OnInputRouted?.Invoke(position, _moveOrder.ActiveUser.Sign);
        }
    }
}