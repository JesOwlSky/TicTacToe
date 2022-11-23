using System;
using UnityEngine;

public class CellInput : MonoBehaviour
{
    public event Action<Vector2> OnPlayerInput;

    private void OnMouseDown()
    {
        float xPos = Mathf.Clamp(transform.position.x, -1, 1);
        float yPos = Mathf.Clamp(transform.position.y, -1, 1);
        OnPlayerInput?.Invoke(new Vector2(xPos, yPos));
    }
}