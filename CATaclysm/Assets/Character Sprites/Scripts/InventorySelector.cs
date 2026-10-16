using UnityEngine;

public class InventorySelector : MonoBehaviour
{
    [SerializeField] private Transform[] inventoryBoxes;
    [SerializeField] private SpriteRenderer[] itemIcons;

    [SerializeField] Sprite sword;

    [SerializeField] private KeyCode switchKey = KeyCode.F;

    private int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateSelectorPosition();

       for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i == 0)
            {
                if(sword != null)
                {
                    itemIcons[i].sprite = sword;
                    itemIcons[i].enabled = true;
                }
            }
            else
            {
                itemIcons[i].sprite = null;
                itemIcons[i].enabled = false;
            }
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Cycle options with F
        if (Input.GetKeyDown(switchKey))
        {
            CycleSelection();
        }
    }


    private void CycleSelection()
    {
        if (inventoryBoxes == null || inventoryBoxes.Length == 0) return;

        currentIndex = (currentIndex + 1) % inventoryBoxes.Length;

        UpdateSelectorPosition();
    }

    private void UpdateSelectorPosition()
    {
        if (inventoryBoxes == null || inventoryBoxes.Length == 0) return;

        transform.position = inventoryBoxes[currentIndex].position;
    }

    public void AddItemToCurrentSlot(Sprite newItemSprite)
    {
        if(currentIndex >= 0 && currentIndex < itemIcons.Length)
        {
            itemIcons[currentIndex].sprite = newItemSprite;
            itemIcons[currentIndex].enabled = true;
        }
    }

    public void ClearCurrentSlot()
    {
        if(currentIndex >= 0 && currentIndex < itemIcons.Length)
        {
            itemIcons[currentIndex].sprite = null;
            itemIcons[currentIndex].enabled = false;
        }
    }

    private void ClearAllIcons()
    {
        foreach (var icon in itemIcons)
        {
            if (icon != null)
            {
                icon.sprite = null;
                icon.enabled = false;
            }
        }
    }

    public int GetSelectedIndex()
    {
        return currentIndex;
    }

}
