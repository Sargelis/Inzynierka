using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button ab1, ab2, ab3;
    public TextMeshProUGUI text1, text2, text3;
    public Image image1, image2, image3;
    public Canvas ability;
    public TextMeshProUGUI timer;
    public Canvas pause;

    GameManagment gameManager;

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
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused) Reseume();
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) PauseMenu();
    }
    //healthbar
    //expbar
    //coins
    public void SetBbutton1(int rand)
    {
        switch (rand) 
        {
            case 0:
                text1.text = "SHOOTING";
                break;
            case 1:
                text1.text = "AXE";
                break;
            case 2:
                text1.text = "BOOK";
                break;
            case 3:
                text1.text = "FIREBALL";
                break;
            case 4:
                text1.text = "SCYTHE";
                break;
        }
    }
    public void SetBbutton2(int rand)
    {
        switch (rand)
        {
            case 0:
                text2.text = "SHOOTING";
                break;
            case 1:
                text2.text = "AXE";
                break;
            case 2:
                text2.text = "BOOK";
                break;
            case 3:
                text2.text = "FIREBALL";
                break;
            case 4:
                text2.text = "SCYTHE";
                break;
        }
    }
    public void SetBbutton3(int rand)
    {
        switch (rand)
        {
            case 0:
                text3.text = "SHOOTING";
                break;
            case 1:
                text3.text = "AXE";
                break;
            case 2:
                text3.text = "BOOK";
                break;
            case 3:
                text3.text = "FIREBALL";
                break;
            case 4:
                text3.text = "SCYTHE";
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
