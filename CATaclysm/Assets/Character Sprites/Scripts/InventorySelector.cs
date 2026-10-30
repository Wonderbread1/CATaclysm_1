using UnityEngine;

public class InventorySelector : MonoBehaviour
{
    public RectTransform[] boxTransform;
    private RectTransform selectorRect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectorRect = GetComponent<RectTransform>();

        if (boxTransform.Length > 0 && boxTransform[0] != null)
        {
            selectorRect.anchoredPosition = boxTransform[0].anchoredPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && boxTransform.Length > 0 && boxTransform[0] != null)
        {
            selectorRect.anchoredPosition = boxTransform[0].anchoredPosition;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && boxTransform.Length > 1 && boxTransform[1] != null)
        {
            selectorRect.anchoredPosition = boxTransform[1].anchoredPosition;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && boxTransform.Length > 2 && boxTransform[2] != null)
        {
            selectorRect.anchoredPosition = boxTransform[2].anchoredPosition;
        }
    }
}
