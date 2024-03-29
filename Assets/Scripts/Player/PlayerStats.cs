using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    public float currentHealth;
    [SerializeField] public float expNeeded = 100f;
    [HideInInspector] public float exp = 0f;
    [HideInInspector] public float lvl = 1f;
    [HideInInspector]public int weaponIndex;
    //public int passiveItemIndex;

    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (exp >= expNeeded)
        {
            lvl++;
            expNeeded *= 1.5f;
            exp = 0f;
            maxHealth += 1;
            currentHealth = maxHealth;
        }
    }
}