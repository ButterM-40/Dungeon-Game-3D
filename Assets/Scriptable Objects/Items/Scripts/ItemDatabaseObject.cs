using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    //Code that occurs after unity serializes object
    public void OnAfterDeserialize()
    {     
        GetItem = new Dictionary<int, ItemObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

    //Code that occurs before unity serializes object
    public void OnBeforeSerialize()
    {
        
    }
}
