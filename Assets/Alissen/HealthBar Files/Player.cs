using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Testing Collision on Item: " + other.name);

        var item = other.GetComponent<GroundItem>();

            if (item)
            {
                inventory.AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
            }

    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
    public int maxHealth=100;
    public int currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        // will be changed later; just need to make sure the healtbar is up and working
        //if(Input.GetKeyDown(KeyCode.Space))
        //    TakeDamage(20);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
    }
}
