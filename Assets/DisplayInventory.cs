using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    public float Spacing_Parameters = 20;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {        
        CreateDisplay();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     UpdateDisplay();
    // }

    public void UpdateDisplay()
    {
        
        Debug.Log("Updated");

        for(int i = 0; i < inventory.Container.Items.Count; i++)
        {
            //Debug.Log("Run Number: " + inventory.Container.Items.Count);
            InventorySlot slot = inventory.Container.Items[i];
            int CurrentItems = inventory.Container.Items.Count;

            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(-95 + (CurrentItems * Spacing_Parameters),55,0);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }
            
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            int CurrentItems = inventory.Container.Items.Count;
            InventorySlot slot = inventory.Container.Items[i];
            Debug.Log(i);
            //Vector3 location = new Vector3(-110 + (i * Spacing_Parameters),109,0);
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(-95 + (CurrentItems * Spacing_Parameters),55,0);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

            itemsDisplayed.Add(slot, obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + ((-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
