using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ThirdPersonMovement  thirdpersonmovement;
    public InventoryObject inventory;
    public DisplayInventory InventoryUpdater;
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Testing Collision on Item: " + other.name);
        
        var item = other.GetComponent<GroundItem>();
            // if(InventoryUpdater.GetComponent<DisplayInventory>())
            // {
            //     Debug.Log("Not here");
            // }
            if (item)
            {
                inventory.AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
                InventoryUpdater.UpdateDisplay();
                //InventoryUpdater.GetComponent<DisplayInventory>().UpdateDisplay();
            }

    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
    public int maxHealth=100;
    public int currentHealth;

    public HealthBar healthBar;
    internal static float lookradius;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        thirdpersonmovement = GetComponent<ThirdPersonMovement>();
        
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            InventoryUpdater.CreateDisplay();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //StartCoroutine(LoadTimer());
            inventory.Load();
        }
    }
    IEnumerator LoadTimer()
    {
        
        inventory.Load();
        yield return new WaitForSeconds(5f);
        InventoryUpdater.CreateDisplay();

        
    }
    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth >= 1f)
        {
         thirdpersonmovement.TakeDamage();
        }
        if(currentHealth == 0f)
        {
            thirdpersonmovement.Die();
            //Debug.Log("CALLED FUNCTION");
        }
    }

}
