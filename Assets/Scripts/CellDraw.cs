using UnityEngine;

public class CellDraw : MonoBehaviour
{
    public void ChangeSprite(Sign sign)
    {
        GetComponent<SpriteRenderer>().sprite = GetComponentInParent<CellSprites>().GetSpriteBySign(sign);
    }
}