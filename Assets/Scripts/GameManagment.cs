using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagment : MonoBehaviour
{
    //main things
    Transform player;
    InventoryManager inventory;
    EnemyMovment em;
    EnemySpawner es;
    Rigidbody2D playerRB;
    WeaponController wc;
    UIManager uiManager;
    PlayerStats playerStats;

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
    int slotIndex;
    int index;
    int rand1, rand2, rand3;
    GameObject[] weaponsControllers;
    GameObject[] weapons;
    GameObject[] books;
    List<int> rand = new List<int>(3);
    int[] aquiredWeapons = new int[4];
    [Header("Time in seconds")]
    [SerializeField] float endTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        es = FindObjectOfType<EnemySpawner>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<PlayerStats>();
        playerLvl = FindObjectOfType<PlayerStats>().lvl;
        inventory = FindObjectOfType<InventoryManager>();
        wc = FindObjectOfType<WeaponController>();
        uiManager = FindObjectOfType<UIManager>();
        shooting = FindObjectOfType<PlayerShooting>();

        currentLvl = playerLvl;
        slotIndex = 1;
    }
    void Update()
    {
        if (uiManager.time >= endTimer) stopTimer = true;

        playerLvl = FindObjectOfType<PlayerStats>().lvl;

        if (playerStats.currentHealth <= 0) SceneManager.LoadScene("GameOver");

        if (currentLvl != playerLvl)
        {
            currentLvl = playerLvl;

            Pause();
            Roll();

            uiManager.SetBbutton1(rand1);
            uiManager.SetBbutton2(rand2);
            uiManager.SetBbutton3(rand3);

            uiManager.wasAbility = true;
            uiManager.ability.enabled = true;
        }

        if (!stopTimer) uiManager.SetTimer();


        if (uiManager.time >= endTimer && es.enemiesAlive <=0) SceneManager.LoadScene("YouWin");
    }

    public void Click1() 
    {
        UnPause();
        RollWeapon(rand1);

        uiManager.ability.enabled = false;
        uiManager.wasAbility = false;
    }
    public void Click2() 
    {
        UnPause();
        RollWeapon(rand2);

        uiManager.ability.enabled = false;
        uiManager.wasAbility = false;
    }
    public void Click3() 
    {
        UnPause();
        RollWeapon(rand3);
        uiManager.ability.enabled = false;
        uiManager.wasAbility = false;
    }
    public void CheckController()
    {
        if (FindObjectOfType<PlayerShooting>() != null) existShoot = true;
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
                if(inventory.shootingLvl == inventory.maxlvl) break;
                inventory.LevelUpWeapon(0);
                inventory.shootingLvl += 1;
                shooting.damage *= 1.5f;
                break;
            case 1:
                if (!existAxe)
                    {
                    GameObject axeController = (GameObject)Instantiate(axeControllerObject, player.position, player.rotation);
                    axeController.transform.SetParent(player);
                    AxeController _axeController = axeController.GetComponent<AxeController>();

                    inventory.AddWeapon(slotIndex, _axeController);
                    inventory.weaponLevels[slotIndex] = 1;
                    inventory.axeLvl = 1;
                    slotIndex++;
                    break;
                }
                else
                {
                    if (inventory.axeLvl == inventory.maxlvl) break;
                    SearchIndex(inventory.weaponSlots, axe);
                    inventory.LevelUpWeapon(index);
                    inventory.axeLvl += 1;
                    axe.damage *= 1.8f;
                    break;
                }
            case 2:
                if (!existBook)
                {
                    GameObject bookController = (GameObject)Instantiate(bookControllerObject, player.position, player.rotation);
                    bookController.transform.SetParent(player);
                    BookController _bookController = bookController.GetComponent<BookController>();

                    inventory.AddWeapon(slotIndex, _bookController);
                    inventory.weaponLevels[slotIndex] = 1;
                    inventory.bookLvl = 1;
                    slotIndex++;
                    //existBook = true;
                    break;
                }
                else
                {
                    if (inventory.bookLvl == inventory.maxlvl) break;
                    SearchIndex(inventory.weaponSlots, book);
                    inventory.LevelUpWeapon(index);
                    book.damage *= 1.6f;
                    inventory.bookLvl += 1;
                    //if (inventory.bookLvl <= 4) book.maxSpawnCount += 1;
                    //switch (inventory.bookLvl)
                    //{
                    //    case 2:
                    //        book.fireRateBook = 0.6f;
                    //        book.cooldownBook = 0.6f;
                    //        break;
                    //    case 3:
                    //        book.fireRateBook = 0.5f;
                    //        book.cooldownBook = 0.5f;
                    //        break;
                    //    case 4:
                    //        book.fireRateBook = 0.4f;
                    //        book.cooldownBook = 0.4f;
                    //        break;
                    //    default: break;
                    //}
                    break;
                }
            case 3:
                if (!existFireball)
                {
                    GameObject fireballController = (GameObject)Instantiate(fireballControllerObject, player.position, player.rotation);
                    fireballController.transform.SetParent(player);
                    FireballController _fireballController = fireballController.GetComponent<FireballController>();

                    inventory.AddWeapon(slotIndex, _fireballController);
                    inventory.weaponLevels[slotIndex] = 1;
                    inventory.fireballLvl = 1;
                    slotIndex++;
                    break;
                }
                else
                {
                    if (inventory.fireballLvl == inventory.maxlvl) break;
                    SearchIndex(inventory.weaponSlots, fireball);
                    inventory.LevelUpWeapon(index);
                    inventory.fireballLvl += 1;
                    fireball.damage *= 2;
                    fireball.fireRateFireball -= 0.2f;
                    break;
                }
            case 4:
                if (!existScythe)
                {
                    GameObject scytheController = (GameObject)Instantiate(scytheControllerObject, player.position, player.rotation);
                    scytheController.transform.SetParent(player);
                    ScytheController _scytheController = scytheController.GetComponent<ScytheController>();

                    inventory.AddWeapon(slotIndex, _scytheController);
                    inventory.weaponLevels[slotIndex] = 1;
                    inventory.scytheLvl = 1;
                    slotIndex++;
                    break;
                }
                else
                {
                    if (inventory.scytheLvl == inventory.maxlvl) break;
                    SearchIndex(inventory.weaponSlots, scythe);
                    inventory.LevelUpWeapon(index);
                    inventory.scytheLvl += 1;
                    scythe.damage *= 1.5f;
                    break;
                }
        }
    }
    public void Roll()
    {
        if (!inventory.weaponSlots.Contains(null))
        {
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
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(shooting)) aquiredWeapons[i] = 0;
            else if (list[i].Equals(axe)) aquiredWeapons[i] = 1;
            else if (list[i].Equals(book)) aquiredWeapons[i] = 2;
            else if (list[i].Equals(fireball)) aquiredWeapons[i] = 3;
            else if (list[i].Equals(scythe)) aquiredWeapons[i] = 4;
        }
    }
    public void SetControllers()
    {
        if (existShoot) shooting = FindObjectOfType<PlayerShooting>().GetComponent<PlayerShooting>();
        if (existAxe) axe = FindObjectOfType<AxeController>().GetComponent<AxeController>();
        if (existBook) book = FindObjectOfType<BookController>().GetComponent<BookController>();
        if (existFireball) fireball = FindObjectOfType<FireballController>().GetComponent<FireballController>();
        if (existScythe) scythe = FindObjectOfType<ScytheController>().GetComponent<ScytheController>();
    }
    public void SearchIndex(List<WeaponController> list, WeaponController weapon)
    {
        index = list.IndexOf(weapon);
    }
    public void Pause()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("BOSS");

        stopTimer = true;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        es.enabled = false;
        wc.enabled = false;

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = false;
        }

        if (bosses != null)
        {
            foreach (GameObject boss in bosses)
            {
                em = boss.GetComponent<EnemyMovment>();
                em.enabled = false;
            }
        }

        //books = GameObject.FindGameObjectsWithTag("BookWeapon");
        //foreach (GameObject book in books)
        //{
        //    Destroy(book);
        //}

        //wy³¹cz weapony
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        books = GameObject.FindGameObjectsWithTag("BookWeapon");
        foreach (GameObject book in books)
        {
            book.SetActive(false);
        }

        CheckController();
        SetControllers();
        //if (existBook) book.spawnCount = 0;

        //wy³¹czenie obiektów broni
        weaponsControllers = GameObject.FindGameObjectsWithTag("WeaponController");
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(false);
        }
    }
    public void UnPause()
    {
        stopTimer = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        es.enabled = true;
        wc.enabled = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("BOSS");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }
        if (bosses != null)
        {
            foreach (GameObject boss in bosses)
            {
                em = boss.GetComponent<EnemyMovment>();
                em.enabled = true;
            }
        }

        //w³¹cz obiekty
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(true);
        }
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(true);
        }
        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }
    }
}