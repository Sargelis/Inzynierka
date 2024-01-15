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
    [SerializeField] GameObject axeControllerObject; //1
    AxeController axeController;
    bool existAxe = false;
    [SerializeField] GameObject bookControllerObject; //2
    BookController bookController;
    bool existBook = false;
    [SerializeField] GameObject fireballControllerObject; //3
    FireballController fireballController;
    bool existFireball = false;
    [SerializeField] GameObject scytheControllerObject; //4
    ScytheController scytheController;
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

            CheckController();

            //wy��czenie obiekt�w broni
            weaponsControllers = GameObject.FindGameObjectsWithTag("WeaponController");
            foreach (GameObject weaponControllerObject in weaponsControllers)
            {
                weaponControllerObject.SetActive(false);
            }

            //losowo�� - wybierz z puli 1 do 4 w��cznie, po wybraniu nie bierz ponownie tej liczby od uwag�, o ile nie zosta�a dodana do invenory - wtedy ulepsze bro�
            rand1 = Random.Range(1, 5);
            rand2 = Random.Range(1, 5);
            rand3 = Random.Range(1, 5);
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
    B�EDY
    -doda� sprawdzenie czy lista jest pe�na jak tak to ju� nie losuj nowcyh broni.
    -poprawi� generowanie bookweapon
    -przystosowa� bronie do zwi�kszania ich poziom�w - prznie�� pola danych do controllera i weapon dziedziczy po nim
    */
    public void Click1() //unfreeze all
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

        //zamiast if do ka�dego, przej�� przez list� inventoryManager i off. dosta� si� do gameObject poprzez Weaponslots gameObjectu Inventory Manager.
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
        ps.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        //zamiast if do ka�dego, przej�� przez list� inventoryManager i off. dosta� si� do gameObject poprzez Weaponslots gameObjectu Inventory Manager.
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
        ps.enabled = true;
        wc.enabled = true;
        //weapons RigidbodyConstraints2D.FreezeRotation

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            em = enemy.GetComponent<EnemyMovment>();
            em.enabled = true;
        }

        //zamiast if do ka�dego, przej�� przez list� inventoryManager i off. dosta� si� do gameObject poprzez Weaponslots gameObjectu Inventory Manager.
        foreach (GameObject weaponControllerObject in weaponsControllers)
        {
            weaponControllerObject.SetActive(true);
        }
        RollWeapon(rand3);

        ability.enabled = false;
    }
    public void CheckController()
    {
        if (FindObjectOfType<AxeController>() != null) existAxe = true;
        if (FindObjectOfType<BookController>() != null) existBook = true;
        if (FindObjectOfType<FireballController>() != null) existFireball = true;
        if (FindObjectOfType<ScytheController>() != null) existScythe = true;
    }
    public void RollWeapon(int rand)
    {
        switch (rand)
        {
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
                    //zwi�ksz poziom broni
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
                    //zwi�ksz poziom broni
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
                    //zwi�ksz poziom broni
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
                    //zwi�ksz poziom broni
                    break;
                }
        }
    }
}