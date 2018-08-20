using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    // ****************** IF  NOT BOUGHT, PLAYERPREF INT = 0,
    // ****************** IF IS  BOUGHT, PLAYERPREF INT = 1,
    // ****************** IF EQUIPPED, PLAYERPREF INT = 2,

    public GameObject[] bdBuyButton;
    public GameObject[] bdEquipButton;
    public GameObject[] bdEquippedButton;

    public GameObject[] chBuyButton;
    public GameObject[] chEquipButton;
    public GameObject[] chEquippedButton;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        // updating board buttons
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            if(PlayerPrefs.GetInt("board"+i,0) == 0)
            {
                bdBuyButton[i].SetActive(true);
                bdEquipButton[i].SetActive(false);
                bdEquippedButton[i].SetActive(false);
            }

            if (PlayerPrefs.GetInt("board" + i, 0) == 1)
            {
                bdBuyButton[i].SetActive(false);
                bdEquipButton[i].SetActive(true);
                bdEquippedButton[i].SetActive(false);
            }

            if (PlayerPrefs.GetInt("board" + i, 0) == 2)
            {
                bdBuyButton[i].SetActive(false);
                bdEquipButton[i].SetActive(false);
                bdEquippedButton[i].SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("board0") == 0)
        {
            PlayerPrefs.SetInt("board0", 2);
            bdBuyButton[0].SetActive(false);
            bdEquipButton[0].SetActive(false);
            bdEquippedButton[0].SetActive(true);
        }

        // updating character buttons
        for (int i = 0; i < chEquipButton.Length; i++)
        {
            if (PlayerPrefs.GetInt("character" + i, 0) == 0)
            {
                chBuyButton[i].SetActive(true);
                chEquipButton[i].SetActive(false);
                chEquippedButton[i].SetActive(false);
            }

            if (PlayerPrefs.GetInt("character" + i, 0) == 1)
            {
                chBuyButton[i].SetActive(false);
                chEquipButton[i].SetActive(true);
                chEquippedButton[i].SetActive(false);
            }

            if (PlayerPrefs.GetInt("character" + i, 0) == 2)
            {
                chBuyButton[i].SetActive(false);
                chEquipButton[i].SetActive(false);
                chEquippedButton[i].SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("character0") == 0)
        {
            PlayerPrefs.SetInt("character0", 2);
            chBuyButton[0].SetActive(false);
            chEquipButton[0].SetActive(false);
            chEquippedButton[0].SetActive(true);
        }
    }

    public void EquipBoard(int j)
    {
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            if(i != j && PlayerPrefs.GetInt("board"+ i) == 2)
            {
                PlayerPrefs.SetInt("board" +i, 1);
            }

            if (i == j)
            {
                PlayerPrefs.SetInt("board" + i, 2);
            }

        }
    }

    public void BuyBoard(int k)
    {
        PlayerPrefs.SetInt("board" + k, 1);
    }

    public void EquipCharacter(int j)
    {
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            if (i != j && PlayerPrefs.GetInt("character" + i) == 2)
            {
                PlayerPrefs.SetInt("character" + i, 1);
            }

            if (i == j)
            {
                PlayerPrefs.SetInt("character" + i, 2);
            }

        }
    }

    public void BuyCharacter(int k)
    {
        PlayerPrefs.SetInt("character" + k, 1);
    }


}
