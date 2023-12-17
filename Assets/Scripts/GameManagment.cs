using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagment : MonoBehaviour
{
    //UI
    public Canvas ability;
    public TextMeshProUGUI timer;

    //main things
    Transform player;
    InventoryManager inventory;
    EnemyMovment em;
    EnemySpawner es;
    Rigidbody2D playerRB;
    PlayerShooting ps;
    WeaponController wc;
    UIManager uiManager;

    //controllers
    [SerializeField] GameObject axeControllerObject;
    AxeController axeController;

    //data
    bool stopTimer = false;
    float currentLvl;
    float playerLvl;
    float minutes, seconds;
    float time;
    int slotIndex;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        es = FindObjectOfType<EnemySpawner>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        ps = FindObjectOfType<PlayerShooting>();
        playerLvl = FindObjectOfType<PlayerStats>().lvl;
        inventory = FindObjectOfType<InventoryManager>();
        wc = FindObjectOfType<WeaponController>();
        uiManager = FindObjectOfType<UIManager>();

        ability.enabled = false;
        currentLvl = playerLvl;
        minutes = 0f;
        seconds = 0f;
        time = 0f;
        slotIndex = 1;
    }
    void Update()
    {
        playerLvl = FindObjectOfType<PlayerStats>().lvl;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (currentLvl != playerLvl) //freeze all
        {
            currentLvl = playerLvl;

            stopTimer = true;
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
            es.enabled = false;
            ps.enabled = false;
            wc.enabled = false;
            //weapons RigidbodyConstrains2D.FreezeAll

            foreach (GameObject enemy in enemies)
            {
                em = enemy.GetComponent<EnemyMovment>();
                em.enabled = false;
            }

            if(FindObjectOfType<AxeController>() != null) //check controller
            {
                axeController = FindObjectOfType<AxeController>();
                axeController.enabled = false;
            }

            ability.enabled = true;
        }

        if (!stopTimer)
        {
            time += 1 * Time.deltaTime;
            minutes = Mathf.FloorToInt( time / 60);
            seconds = Mathf.FloorToInt( time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void Click() //unfreeze all
    {
        stopTimer = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        es.enabled = true;
        ps.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        if (FindObjectOfType<AxeController>() != null) //check controller
        {
            axeController.enabled = true;
        }
        else //addcontroller do slotindex w³¹cznie
        {
            GameObject axeController = (GameObject)Instantiate(axeControllerObject, player.position, player.rotation);
            axeController.transform.SetParent(player);
            AxeController axeController1 = axeController.GetComponent<AxeController>();

            inventory.AddWeapon(slotIndex, axeController1);
            slotIndex++;
        }

        ability.enabled = false;
    }
    public void AddController(float rand)
    {

    }
    public void CheckController(float rand)
    {

    }
}