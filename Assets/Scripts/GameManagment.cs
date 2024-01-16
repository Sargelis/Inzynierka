using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    WeaponController wc;
    UIManager uiManager;

    //controllers
    PlayerShooting shooting;
    bool existShoot = false;
    [SerializeField] GameObject axeControllerObject; //1
    AxeController axe;
    bool existAxe = false;
    [SerializeField] GameObject bookControllerObject; //2
    BookController book;
    bool existBook = false;
    [SerializeField] GameObject fireballControllerObject; //3
    FireballController fireball;
    bool existFireball = false;
    [SerializeField] GameObject scytheControllerObject; //4
    ScytheController scythe;
    bool existScythe = false;

    //data
    bool stopTimer = false;
    float currentLvl;
    float playerLvl;
    float minutes, seconds;
    float time;
    int slotIndex;
    int rand1, rand2, rand3;
    GameObject[] weaponsControllers;
    List<int> rand = new List<int>(3);
    int[] aquiredWeapons = new int[4];

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        es = FindObjectOfType<EnemySpawner>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerLvl = FindObjectOfType<PlayerStats>().lvl;
        inventory = FindObjectOfType<InventoryManager>();
        wc = FindObjectOfType<WeaponController>();
        uiManager = FindObjectOfType<UIManager>();
        //shooting = FindObjectOfType<PlayerShooting>();

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
            wc.enabled = false;
            //shooting.enabled = true;
            //weapons RigidbodyConstrains2D.FreezeAll

            foreach (GameObject enemy in enemies)
            {
                em = enemy.GetComponent<EnemyMovment>();
                em.enabled = false;
            }

            CheckController();
            Debug.Log("DUPA");
            Roll();

            //wy³¹czenie obiektów broni
            weaponsControllers = GameObject.FindGameObjectsWithTag("WeaponController");
            foreach (GameObject weaponControllerObject in weaponsControllers)
            {
                weaponControllerObject.SetActive(false);
            }
            
            uiManager.SetBbutton1(rand1);
            uiManager.SetBbutton2(rand2);
            uiManager.SetBbutton3(rand3);

            ability.enabled = true;
        }

        if (!stopTimer)
        {
            time += 1 * Time.deltaTime;
            minutes = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    /*
    B£EDY/TO DO
    -zwiêkszanie poziomow broni
    -KRYTYCZNY unity zawiesza siê ca³kowicie kij wie czego
    -po zape³nieniu slotów broni pojawiaj¹ siê bronie których gracz nie posiada ju¿/wylosowa³o bron jakiej gracz nie ma po zape³nieniu slotów
    -poprawiæ generowanie bookweapon
    -przystosowaæ bronie do zwiêkszania ich poziomów - prznieœæ pola danych do controllera i weapon dziedziczy po nim
    */
    public void Click1() //unfreeze all
    {
        stopTimer = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        es.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        //w³¹cz obiekty 
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(true);
        }
        RollWeapon(rand1);

        ability.enabled = false;
    }
    public void Click2() //unfreeze all
    {
        stopTimer = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        es.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        //w³¹cz obiekty
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(true);
        }
        RollWeapon(rand2);

        ability.enabled = false;
    }
    public void Click3() //unfreeze all
    {
        stopTimer = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        es.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        //w³¹cz obiekty
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(true);
        }
        RollWeapon(rand3);

        ability.enabled = false;
    }
    public void CheckController()
    {
        //if (FindObjectOfType<PlayerShooting>() != null) existShoot = true;
        if (FindObjectOfType<AxeController>() != null) existAxe = true;
        if (FindObjectOfType<BookController>() != null) existBook = true;
        if (FindObjectOfType<FireballController>() != null) existFireball = true;
        if (FindObjectOfType<ScytheController>() != null) existScythe = true;
    }
    public void RollWeapon(int rand)
    {
        switch (rand)
        {
            case 0:
                Debug.Log("Ulepszono soohting");
                //zwiêksz poziom broni
                break;
            case 1:
                if (!existAxe)
                    {
                    GameObject axeController = (GameObject)Instantiate(axeControllerObject, player.position, player.rotation);
                    axeController.transform.SetParent(player);
                    AxeController _axeController = axeController.GetComponent<AxeController>();

                    inventory.AddWeapon(slotIndex, _axeController);
                    slotIndex++;
                    break;
                }
                else
                {
                    Debug.Log("Ulepszono axe");
                    //zwiêksz poziom broni
                    break;
                }
            case 2:
                if (!existBook)
                {
                    GameObject bookController = (GameObject)Instantiate(bookControllerObject, player.position, player.rotation);
                    bookController.transform.SetParent(player);
                    BookController _bookController = bookController.GetComponent<BookController>();

                    inventory.AddWeapon(slotIndex, _bookController);
                    slotIndex++;
                    break;
                }
                else
                {
                    Debug.Log("Ulepszono book");
                    //zwiêksz poziom broni
                    break;
                }
            case 3:
                if (!existFireball)
                {
                    GameObject fireballController = (GameObject)Instantiate(fireballControllerObject, player.position, player.rotation);
                    fireballController.transform.SetParent(player);
                    FireballController _fireballController = fireballController.GetComponent<FireballController>();

                    inventory.AddWeapon(slotIndex, _fireballController);
                    slotIndex++;
                    break;
                }
                else
                {
                    Debug.Log("Ulepszono fireball");
                    //zwiêksz poziom broni
                    break;
                }
            case 4:
                if (!existScythe)
                {
                    GameObject scytheController = (GameObject)Instantiate(scytheControllerObject, player.position, player.rotation);
                    scytheController.transform.SetParent(player);
                    ScytheController _scytheController = scytheController.GetComponent<ScytheController>();

                    inventory.AddWeapon(slotIndex, _scytheController);
                    slotIndex++;
                    break;
                }
                else
                {
                    Debug.Log("Ulepszono scythe");
                    //zwiêksz poziom broni
                    break;
                }
        }
    }
    public void Roll()
    {
        if (!inventory.weaponSlots.Contains(null))
        {
            Debug.Log("HELPP");
            CopyWeaponList(inventory.weaponSlots);
            //losowanie z elemntów listy
            while (rand.Count != rand.Capacity)
            {
                int i = Random.Range(0, aquiredWeapons.Length);
                if (rand.Contains(aquiredWeapons[i])) continue;
                else rand.Add(aquiredWeapons[i]);
            }
            rand1 = rand[0];
            rand2 = rand[1];
            rand3 = rand[2];
            rand.Clear();
        }
        else
        {
            //losowoœæ - wybierz z puli 0 - 4 czyli wszystkie bronie, bez powtórzeñ
            while (rand.Count != rand.Capacity)
            {
                int i = Random.Range(0, 5);
                if (rand.Contains(i)) continue;
                else rand.Add(i);
            }
            rand1 = rand[0];
            rand2 = rand[1];
            rand3 = rand[2];
            rand.Clear();
        }
    }
    public void CopyWeaponList(List<WeaponController> list)
    {
        if (existShoot) shooting = FindObjectOfType<PlayerShooting>().GetComponent<PlayerShooting>();
        if (existAxe) axe = FindObjectOfType<AxeController>().GetComponent<AxeController>();
        if (existBook) book = FindObjectOfType<BookController>().GetComponent<BookController>();
        if (existFireball) fireball = FindObjectOfType<FireballController>().GetComponent<FireballController>();
        if (existScythe) scythe = FindObjectOfType<ScytheController>().GetComponent<ScytheController>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(shooting)) aquiredWeapons[i] = 0;
            else if (list[i].Equals(axe)) aquiredWeapons[i] = 1;
            else if (list[i].Equals(book)) aquiredWeapons[i] = 2;
            else if (list[i].Equals(fireball)) aquiredWeapons[i] = 3;
            else if (list[i].Equals(scythe)) aquiredWeapons[i] = 4;
            else Debug.LogWarning("zesra³o siê");
        }

        for (int i = 0; i < list.Count; i++)
        {
            Debug.LogWarning(aquiredWeapons[i]);
        }
    }
}