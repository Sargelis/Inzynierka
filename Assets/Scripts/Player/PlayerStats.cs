using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] float expNeeded = 100f;
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
        if (currentHealth <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //je�eli �ycie <=0 to zmie� scene
        if (exp >= expNeeded)
        {
            lvl++;
            expNeeded *= 1.5f;
            exp = 0f;
        }
    }
}