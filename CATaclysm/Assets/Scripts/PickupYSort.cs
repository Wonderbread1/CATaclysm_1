
using UnityEngine;

public class PickupYSort : MonoBehaviour
{
    public SpriteRenderer itemRenderer;
    public SpriteRenderer shadowRenderer;
    public Transform groundPoint;

    public int sortingScale = 100;
    public int sortingOffset = 10;

    private void LateUpdate()
    {
        if (itemRenderer == null || groundPoint == null)
            return;

        int groundOrder =
            -Mathf.RoundToInt(groundPoint.position.y * sortingScale);

        itemRenderer.sortingOrder = groundOrder + sortingOffset;

        if (shadowRenderer != null)
            shadowRenderer.sortingOrder = groundOrder + sortingOffset - 1;
    }
}
