using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject purchasePanelDark;
    public GameObject NoMoneyDark;

    public Button yesButton;
    public Button noButton;

    private bool bdbool;
    private bool chbool;

    private int boardNumber;
    private int characterNumber;

    public Text bd0;
    public Text bd1;
    public Text bd2;
    public Text bd3;

    public Text ch0;
    public Text ch1;
    public Text ch2;
    public Text ch3;

    // price of boards and characters
    public int bd0price;
    public int bd1price;
    public int bd2price;
    public int bd3price;

    public int ch0price;
    public int ch1price;
    public int ch2price;
    public int ch3price; 

    private int[] bdprice;
    private int[] chprice;

    // Use this for initialization
    void Start () {
        Button yesBtn = yesButton.GetComponent<Button>();
        yesBtn.onClick.AddListener(Purchase);

        Button noBtn = noButton.GetComponent<Button>();
        noBtn.onClick.AddListener(DontPurchase);

        bd0.text = bd0price.ToString();
        bd1.text = bd1price.ToString();
        bd2.text = bd2price.ToString();
        bd3.text = bd3price.ToString();

        ch0.text = ch0price.ToString();
        ch1.text = ch1price.ToString();
        ch2.text = ch2price.ToString();
        ch3.text = ch3price.ToString();

        bdprice = new int[] {bd0price, bd1price, bd2price, bd3price};
        chprice = new int[] {ch0price, ch1price, ch2price, ch3price};
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
        purchasePanelDark.SetActive(true);
       boardNumber = k;
       bdbool = true;
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
        purchasePanelDark.SetActive(true);
        characterNumber = k;
        chbool = true;
    }

    public void Purchase()
    {

        if (bdbool)
        {
            if (PlayerPrefs.GetInt("money", 0) - bdprice[boardNumber]  >= 0)
            {
                bdbool = false;
                PlayerPrefs.SetInt("board" + boardNumber, 1);
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money", 0) - bdprice[boardNumber]));
            }
            else
            {
                NoMoneyDark.SetActive(true);
                bdbool = false;
            }
        }
        if (chbool)
        {
            if (PlayerPrefs.GetInt("money", 0) - chprice[characterNumber] >= 0)
            {
                chbool = false;
                PlayerPrefs.SetInt("character" + characterNumber, 1);
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money", 0) - chprice[characterNumber]));
            }
            else
            {
                NoMoneyDark.SetActive(true);
                chbool = false;
            }
        }
    }

    public void DontPurchase()
    {
        bdbool = false;
        chbool = false;
    }
}
