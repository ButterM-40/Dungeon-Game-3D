using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : MonoBehaviour
{
    public Text bossName;
    Slider slider;
    public float Timer = 10f;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        bossName = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        SetUIHealthBarToActive();
    }

    public void SetBossName(string name)
    {
        bossName.text = name;
    }

    public void SetUIHealthBarToActive()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetUIHealthBarToInactive()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetBossMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetBossCurrentHealth(int CurrentHealth)
    {
        slider.value = CurrentHealth;
    }

    public void DecreaseBossCurrentHealth(int CurrentHealth)
    {
        slider.value -= CurrentHealth;
    }

    // public void Update()
    // {
    //     Timer -= Time.deltaTime;
    //     if(Timer < 0)
    //     {
    //         slider.value -= 10;
    //         Timer = 10f;
    //     }
    // }
}
