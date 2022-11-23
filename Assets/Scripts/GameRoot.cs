using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private FieldUI _fieldUI;
    private Field _field;
    private Winning _winning;
    private MoveOrder _moveOrder;
    private AiInput _aiInput;
    private InputRouter _inputRouter;

    private void Start()
    {
        Compose();
    }

    private void Compose()
    {
        _field = new Field();

        _fieldUI = FindObjectOfType<FieldUI>();
        _fieldUI.FillUIField();
        _field.OnFieldChanged += _fieldUI.UpdateCell;

        _winning = new Winning(_field);
        _moveOrder = new MoveOrder(_field);
        _aiInput = gameObject.AddComponent<AiInput>();
        _aiInput.Connect(_field, _winning);
        _inputRouter = new InputRouter(_moveOrder, _fieldUI, _aiInput);

        _field.Init(_inputRouter);
    }
}