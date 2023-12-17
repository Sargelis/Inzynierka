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
    InventoryManager inventory;
    [SerializeField] GameObject playerShooting;
    Transform player;

    [HideInInspector]public int weaponIndex;
    //public int passiveItemIndex;

    void Start()
    {
        currentHealth = maxHealth;
        inventory = FindObjectOfType<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject ps = (GameObject)Instantiate(playerShooting, player.transform.position, player.rotation);
        ps.transform.SetParent(player);
        PlayerShooting playerShooting1 = ps.GetComponent<PlayerShooting>();

        inventory.AddWeapon(0, playerShooting1);
    }
    void Update()
    {
        if (currentHealth <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //je¿eli ¿ycie <=0 to zmieñ scene
        if (exp >= expNeeded)
        {
            lvl++;
            expNeeded *= 1.5f;
            exp = 0f;
        }
    }
}