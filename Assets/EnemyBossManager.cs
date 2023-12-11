using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;
    public GameObject boss;
    UIBossHealthBar bossHealthBar;
    public float moveSpeed = 50000f;
    public float spinSpeed = 50000f;
    private float timer = 5f;
    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();

    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(100); 
        //bossHealthBar.DecreaseBossCurrentHealth(1000);
    }
    private void Update(){
        //Debug.Log(bossHealthBar.getHealthBar());
        if(bossHealthBar.getHealthBar() <= 0 || boss.GetComponent<BossMove>().giveUpBoss()){
            //Debug.Log("dead");
            boss.GetComponent<BossMove>().enabled = false;
            boss.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            boss.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            timer -= Time.deltaTime;

            GameObject[] crystals = GameObject.FindGameObjectsWithTag("Crystal");

            // Loop through each found GameObject and destroy it
            foreach (GameObject crystal in crystals)
            {
                Debug.Log("Destroyed");
                Destroy(crystal);
            }
        }
        if(timer < 0){
            Destroy(boss);
        }

        
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Weapon"){
            Debug.Log("Detected Player");
            bossHealthBar.DecreaseBossCurrentHealth(10);
        }
    }
}
