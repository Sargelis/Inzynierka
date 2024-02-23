using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button ab1, ab2, ab3;
    public TextMeshProUGUI text1, text2, text3;
    public Image image1, image2, image3;
    public Sprite axe, book, bullet, fireball, scythe;
    public Canvas ability;
    public TextMeshProUGUI timer;
    public Canvas pause;
    public Slider healthBar;
    public Slider expBar;

    GameManagment gameManager;
    PlayerStats playerStats;

    float minutes, seconds;
    [HideInInspector] public float time;
    bool isPaused = false;
    [HideInInspector] public bool wasAbility = false;

    void Start()
    {
        ability.enabled = false;
        pause.enabled = false;
        minutes = 0f;
        seconds = 0f;
        time = 0f;
        gameManager = FindObjectOfType<GameManagment>();
        playerStats = FindObjectOfType<PlayerStats>();
        healthBar.maxValue = playerStats.maxHealth;
        healthBar.value = playerStats.maxHealth;
        expBar.maxValue = playerStats.expNeeded;
        expBar.value = playerStats.exp;
    }
    void Update()
    {
        SetHealthBar();
        SetExpBar();
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused) Reseume();
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) PauseMenu();
    }
    public void SetHealthBar()
    {
        healthBar.value = playerStats.currentHealth;
        healthBar.maxValue = playerStats.maxHealth;
    }
    public void SetExpBar()
    {
        expBar.value = playerStats.exp;
        expBar.maxValue = playerStats.expNeeded;
    }
    //coins
    public void SetBbutton1(int rand)
    {
        switch (rand) 
        {
            case 0:
                text1.text = "SHOOTING";
                image1.sprite = bullet;
                break;
            case 1:
                text1.text = "AXE";
                image1.sprite = axe;
                break;
            case 2:
                text1.text = "BOOK";
                image1.sprite = book;
                break;
            case 3:
                text1.text = "FIREBALL";
                image1.sprite = fireball;
                break;
            case 4:
                text1.text = "SCYTHE";
                image1.sprite = scythe;
                break;
        }
    }
    public void SetBbutton2(int rand)
    {
        switch (rand)
        {
            case 0:
                text2.text = "SHOOTING";
                image2.sprite = bullet;
                break;
            case 1:
                text2.text = "AXE";
                image2.sprite = axe;
                break;
            case 2:
                text2.text = "BOOK";
                image2.sprite = book;
                break;
            case 3:
                text2.text = "FIREBALL";
                image2.sprite = fireball;
                break;
            case 4:
                text2.text = "SCYTHE";
                image2.sprite = scythe;
                break;
        }
    }
    public void SetBbutton3(int rand)
    {
        switch (rand)
        {
            case 0:
                text3.text = "SHOOTING";
                image3.sprite = bullet;
                break;
            case 1:
                text3.text = "AXE";
                image3.sprite = axe;
                break;
            case 2:
                text3.text = "BOOK";
                image3.sprite = book;
                break;
            case 3:
                text3.text = "FIREBALL";
                image3.sprite = fireball;
                break;
            case 4:
                text3.text = "SCYTHE";
                image3.sprite = scythe;
                break;
        }
    }
    public void SetTimer()
    {
        time += 1 * Time.deltaTime;
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void PauseMenu()
    {
        if (isPaused) Reseume();
        else
        {
            if (wasAbility)
            {
                ability.enabled = false;
                pause.enabled = false;
                isPaused = false;
            }
            pause.enabled = true;
            isPaused = true;
            gameManager.Pause();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Reseume()
    {
        if (wasAbility)
        {
            ability.enabled = true;
            pause.enabled = false;
            isPaused = false;
            gameManager.Pause();
        }
        else
        {
            gameManager.UnPause();
            pause.enabled = false;
            isPaused = false;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}