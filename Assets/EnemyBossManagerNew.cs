using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManagerNew : MonoBehaviour
{
    public string bossName;
    UIBossHealthBar bossHealthBar;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();

    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(100); 
    }
}
