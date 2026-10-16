
using UnityEngine;
using System.Collections;

public class BarrelBreakHandler : MonoBehaviour
{
    [Header("Barrel Animation")]
    public Animator barrelAnimator;
    public float destructionDelay = 0.5f;

    [Header("Item Drops")]
    public GameObject bandagePrefab;
    public GameObject tntPrefab;

    [Header("Guaranteed Drops")]
    public bool guaranteedTNTDrop = false;

    [Range(0f, 100f)]
    public float bandageChance = 30f;

    [Range(0f, 100f)]
    public float tntChance = 10f;

    private bool isBreaking = false;

    public void BreakBarrel()
    {
        if (isBreaking) return;
        isBreaking = true;

        if (barrelAnimator != null)
            barrelAnimator.SetTrigger("Break");

        YSort barrelYSort = GetComponent<YSort>();

        if (barrelYSort != null)
            barrelYSort.enabled = false;

        GameObject droppedItem = DropItem();

        SpriteRenderer barrelSprite = GetComponent<SpriteRenderer>();

        if (barrelSprite != null)
        {
            if (droppedItem != null)
            {
                PickupYSort pickupSort =
                    droppedItem.GetComponent<PickupYSort>();

                if (pickupSort != null && pickupSort.groundPoint != null)
                {
                    int itemGroundOrder =
                        -Mathf.RoundToInt(
                            pickupSort.groundPoint.position.y *
                            pickupSort.sortingScale
                        ) + pickupSort.sortingOffset;

                    barrelSprite.sortingOrder = itemGroundOrder - 2;
                }
                else
                {
                    barrelSprite.sortingOrder -= 20;
                }
            }
            else
            {
                barrelSprite.sortingOrder -= 20;
            }
        }

        StartCoroutine(FinishBreaking());
    }

    private GameObject DropItem()
    {
        GameObject itemToDrop = null;

        if (guaranteedTNTDrop)
        {
            itemToDrop = tntPrefab;
        }
        else
        {
            float roll = Random.Range(0f, 100f);

            if (roll < tntChance)
                itemToDrop = tntPrefab;
            else if (roll < tntChance + bandageChance)
                itemToDrop = bandagePrefab;
        }

        if (itemToDrop == null)
            return null;

        GameObject item = Instantiate(
            itemToDrop,
            transform.position,
            Quaternion.identity
        );

        ItemPopOut popOut = item.GetComponent<ItemPopOut>();

        if (popOut != null)
            popOut.PopOut();

        return item;
    }

    private IEnumerator FinishBreaking()
    {
        yield return new WaitForSeconds(destructionDelay);

        if (barrelAnimator != null)
            barrelAnimator.enabled = false;

        BoxCollider2D barrelCollider = GetComponent<BoxCollider2D>();

        if (barrelCollider != null)
            barrelCollider.enabled = false;
    }
}
