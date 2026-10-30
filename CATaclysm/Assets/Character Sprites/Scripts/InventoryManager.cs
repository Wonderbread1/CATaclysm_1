using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //Icons
    public Image box_1;
    public Image box_2;
    public Image box_3;

    //Sprites
    public Sprite sword;
    public Sprite bandage;
    public Sprite dynamite;

    //Inventory State
    private string itemBox1 = "Sword";
    private bool hasSword = true;
    private string itemBox2 = "Empty";
    private int bandageCount2 = 0;

    private string itemBox3 = "Empty";
    private int bandageCount3 = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasAvailableSlot()
    {
        if(itemBox2 == "Empty" || itemBox3 == "Empty")
        {
            return true;
        }
        return false;
    }

    public string GetSelectedItemType(int index)
    {
        if (index == 0) return itemBox1;
        if (index == 1) return itemBox2;
        if (index == 2) return itemBox3;

        return "Empty";
    }

    public void PickUpItem(string itemType)
    {
        if (itemType == "Bandage")
        {
            if (itemBox2 == "Bandage" && bandageCount2 < 3)
            {
                bandageCount2++;
            }
            else if (itemBox3 == "Bandage" && bandageCount3 < 3)
            {
                bandageCount3++;
            }
            else if (itemBox2 == "Empty")
            {
                itemBox2 = "Bandage";
                bandageCount2 = 1;
            }
            else if (itemBox3 == "Empty")
            {
                itemBox3 = "Bandage";
                bandageCount3 = 1;
            }
        }
        else if (itemType == "Dynamite")
        {
            if (itemBox2 == "Empty")
            {
                itemBox2 = "Dynamite";
            }
            else if(itemBox3 == "Empty")
            {
                itemBox3 = "Dynamite";
            }
        }

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        //Box 1: Sword
        if (hasSword && box_1 != null)
        {
            box_1.gameObject.SetActive(true);
            box_1.sprite = sword;
        }

        //Box 2
        if (box_2 != null)
        {
            if (itemBox2 == "Bandage")
            {
                box_2.gameObject.SetActive(true);
                box_2.sprite = bandage;
            }
            else if (itemBox2 == "Dynamite")
            {
                box_2.gameObject.SetActive(true);
                box_2.sprite = dynamite;
            }
            else
            {
                box_2.gameObject.SetActive(false);
            }
        }

        //Box 3
        if(box_3 != null)
        {
            if (itemBox3 == "Bandage")
            {
                box_3.gameObject.SetActive(true);
                box_3.sprite = bandage;
            }
            else if (itemBox3 == "Dynamite")
            {
                box_3.gameObject.SetActive(true);
                box_3.sprite = dynamite;
            }
            else
            {
                box_3.gameObject.SetActive(false);
            }
        }

    }

    public void RemoveItem(string itemType)
    {
        //Box 2
        if(itemBox2 == itemType)
        {
            itemBox2 = "Empty";
            bandageCount2 = 0;
        }
        else if(itemBox3 == itemType)
        {
            itemBox3 = "Empty";
            bandageCount3 = 0;
        }

        UpdateInventoryUI();
    }

    public void DecreaseSelectedItem(int index)
    {
        if (index == 1)
        {
            if (itemBox2 == "Bandage")
            {
                bandageCount2--;
                if (bandageCount2 <= 0) { itemBox2 = "Empty"; bandageCount2 = 0; }
            }
            else if (itemBox2 == "Dynamite")
            {
                itemBox2 = "Empty";
            }

        }
        else if (index == 2)
        {
            if (itemBox3 == "Bandage")
            {
                bandageCount3--;
                if (bandageCount3 <= 0) { itemBox3 = "Empty"; bandageCount3 = 0; }
            }
            else if (itemBox3 == "Dynamite")
            {
                itemBox3 = "Empty";
            }
        }

        UpdateInventoryUI();
    }

    public void ClearSlot (int index)
    {
        if (index == 1)
        {
            itemBox2 = "Empty";
            bandageCount2 = 0;
        }
        else if (index == 2)
        {
            itemBox3 = "Empty";
            bandageCount3 = 0;
        }

        UpdateInventoryUI();
    }
    public bool UseTNT()
    {
        if (itemBox3 == "Dynamite")
        {
            itemBox3 = "Empty";
            UpdateInventoryUI();
            return true;
        }
        else if (itemBox2 == "Dynamite")
        {
            itemBox2 = "Empty";
            UpdateInventoryUI();
            return true;
        }

        return false;
    }

}
