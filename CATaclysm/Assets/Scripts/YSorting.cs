using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    public int sortingOffset = 0;
    public int sortingScale = 100;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        float bottomY = spriteRenderer.bounds.min.y;

        spriteRenderer.sortingOrder =
            -Mathf.RoundToInt(bottomY * sortingScale)
            + sortingOffset;
    }
}