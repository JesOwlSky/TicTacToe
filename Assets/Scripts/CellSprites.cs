using System.Collections.Generic;
using UnityEngine;

public class CellSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new();

    public Sprite GetSpriteBySign(Sign sign) => sprites[(int)sign];
}