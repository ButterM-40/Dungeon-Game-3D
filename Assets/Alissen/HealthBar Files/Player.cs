using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;

    public int attack;

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
        // // will be changed later; just need to make sure the healtbar is up and working
        // if(Input.GetKeyDown(KeyCode.Space))
        //     TakeDamage(20);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
    }

}