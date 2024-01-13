using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button ab1, ab2, ab3;
    public TextMeshProUGUI text1, text2, text3;
    public Image image1, image2, image3;
    //canvas ability
    //timer

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    //set timer
    //set ability
    //random ability
    //healthbar
    //expbar
    //coins
    //pause menu
    public void SetBbutton1(int rand)
    {
        switch (rand) 
        {
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
}
