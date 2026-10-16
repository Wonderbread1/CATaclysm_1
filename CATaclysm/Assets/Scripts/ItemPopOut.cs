
using UnityEngine;
using System.Collections;

public class ItemPopOut : MonoBehaviour
{
    [Header("Pop Animation")]
    public float jumpHeight = 0.65f;
    public float jumpDistance = 0.15f;
    public float jumpDuration = 0.55f;

    private ItemFloat itemFloat;
    private Transform itemSprite;
    private bool hasPopped = false;

    private void Awake()
    {
        itemFloat = GetComponentInChildren<ItemFloat>();

        if (itemFloat != null)
        {
            itemSprite = itemFloat.transform;
            itemFloat.enabled = false;
        }
    }

    public void PopOut()
    {
        if (hasPopped) return;

        hasPopped = true;
        StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition +
            new Vector3(Random.Range(-jumpDistance, jumpDistance), 0f, 0f);

        Vector3 spriteStart = itemSprite != null
            ? itemSprite.localPosition
            : Vector3.zero;

        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / jumpDuration);

            // Move only a tiny distance sideways.
            transform.position = Vector3.Lerp(
                startPosition, endPosition, t
            );

            // Make the item jump straight upward in an arc.
            float height = 4f * jumpHeight * t * (1f - t);

            if (itemSprite != null)
            {
                itemSprite.localPosition =
                    spriteStart + Vector3.up * height;
            }

            yield return null;
        }

        transform.position = endPosition;

        if (itemSprite != null)
        {
            itemSprite.localPosition = spriteStart;
        }

        // Start the regular floating effect after landing.
        if (itemFloat != null)
        {
            itemFloat.enabled = true;
        }
    }
}
