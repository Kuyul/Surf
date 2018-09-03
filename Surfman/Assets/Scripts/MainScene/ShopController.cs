using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    //Singleton
    public static ShopController Instance;

    // ****************** IF  NOT BOUGHT, PLAYERPREF INT = 0,
    // ****************** IF IS  BOUGHT, PLAYERPREF INT = 1,
    // ****************** IF EQUIPPED, PLAYERPREF INT = 2,

    public GameObject[] activeCharacter;

    public GameObject[] previewCharacter;
    public GameObject[] adPreviewCharacter;
    public GameObject[] noMoneyPreviewCharacter;

    public GameObject[] previewBoard;
    public GameObject[] noMoneyPreviewBoard;

    public GameObject[] bdBuyButton;
    public GameObject[] bdEquipButton;
    public GameObject[] bdEquippedButton;

    public GameObject[] chBuyButton;
    public GameObject[] chEquipButton;
    public GameObject[] chEquippedButton;

    public GameObject purchasePanelDark;
    public GameObject purchasePanel;
    public GameObject watchAdPanel;
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
    public Text adch1;
    public Text ch2;
    public Text ch3;

    // price of boards and characters
    public int bd0price;
    public int bd1price;
    public int bd2price;
    public int bd3price;

    public int ch0price;
    public int adch1price;
    public int ch2price;
    public int ch3price;

    private int[] bdprice;
    private int[] chprice;

    //Initialise static, so that this controller is callable from anywhere
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        //The actual purchase is done in the code
        Button yesBtn = yesButton.GetComponent<Button>();
        yesBtn.onClick.AddListener(Purchase);

        Button noBtn = noButton.GetComponent<Button>();
        noBtn.onClick.AddListener(DontPurchase);

        bd0.text = bd0price.ToString();
        bd1.text = bd1price.ToString();
        bd2.text = bd2price.ToString();
        bd3.text = bd3price.ToString();

        ch0.text = ch0price.ToString();
        ch2.text = ch2price.ToString();
        ch3.text = ch3price.ToString();

        bdprice = new int[] { bd0price, bd1price, bd2price, bd3price };
        chprice = new int[] { ch0price, adch1price, ch2price, ch3price };
    }

    // Update is called once per frame
    void Update()
    {
        adch1.text = PlayerPrefs.GetInt("adch1", 0).ToString() + "/3";
        
        // For each Board, update the buttons according to its respective status value stored in playerprefs.
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            //If Unpurchased, then set buy button to active
            if (PlayerPrefs.GetInt("board" + i, 0) == 0)
            {
                bdBuyButton[i].SetActive(true);
                bdEquipButton[i].SetActive(false);
                bdEquippedButton[i].SetActive(false);
            }

            //If Bought, but not equipped, set equip button to active
            if (PlayerPrefs.GetInt("board" + i, 0) == 1)
            {
                bdBuyButton[i].SetActive(false);
                bdEquipButton[i].SetActive(true);
                bdEquippedButton[i].SetActive(false);
            }

            //If Equipped, set the equipped button to active
            //There can only be one eqiupped character/board at one time.
            if (PlayerPrefs.GetInt("board" + i, 0) == 2)
            {
                bdBuyButton[i].SetActive(false);
                bdEquipButton[i].SetActive(false);
                bdEquippedButton[i].SetActive(true);
            }
        }

        //Default Board's playerprefs will start as 0, we want it to be equipped when this is the case.
        //This statement will only run once when the user first starts the game.
        if (PlayerPrefs.GetInt("board0") == 0)
        {
            PlayerPrefs.SetInt("board0", 2);
            bdBuyButton[0].SetActive(false);
            bdEquipButton[0].SetActive(false);
            bdEquippedButton[0].SetActive(true);
        }

        // For each character, update the buttons according to its respective status value stored in playerprefs.
        for (int i = 0; i < chEquipButton.Length; i++)
        {
            //If Unpurchased, then set buy button to active
            if (PlayerPrefs.GetInt("character" + i, 0) == 0)
            {
                chBuyButton[i].SetActive(true);
                chEquipButton[i].SetActive(false);
                chEquippedButton[i].SetActive(false);

                activeCharacter[i].SetActive(false);
            }

            //If Bought, but not equipped, set equip button to active
            if (PlayerPrefs.GetInt("character" + i, 0) == 1)
            {
                chBuyButton[i].SetActive(false);
                chEquipButton[i].SetActive(true);
                chEquippedButton[i].SetActive(false);

                activeCharacter[i].SetActive(false);
            }

            //If Equipped, set the equipped button to active
            //There can only be one eqiupped character/board at one time.
            if (PlayerPrefs.GetInt("character" + i, 0) == 2)
            {
                chBuyButton[i].SetActive(false);
                chEquipButton[i].SetActive(false);
                chEquippedButton[i].SetActive(true);

                activeCharacter[i].SetActive(true);
            }
        }

        //Default character's playerprefs will start as 0, we want it to be equipped when this is the case.
        //This statement will only run once when the user first starts the game.
        if (PlayerPrefs.GetInt("character0") == 0)
        {
            PlayerPrefs.SetInt("character0", 2);
            chBuyButton[0].SetActive(false);
            chEquipButton[0].SetActive(false);
            chEquippedButton[0].SetActive(true);
        }
    }

    //The button feeds in an integer representing the board
    //With the given number, perform the following logic
    public void EquipBoard(int j)
    {
        //Go through each board in the array
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            //If a different board is currently equipped, set its status to 1 "Bought"
            if (i != j && PlayerPrefs.GetInt("board" + i) == 2)
            {
                PlayerPrefs.SetInt("board" + i, 1);
            }

            if (i == j)
            {
                PlayerPrefs.SetInt("board" + i, 2);
            }
        }
    }

    //Brings up the purchase panel
    public void BuyBoard(int k)
    {
        purchasePanelDark.SetActive(true);
        purchasePanel.SetActive(true);
        watchAdPanel.SetActive(false);

        previewBoard[k].SetActive(true);
        boardNumber = k;
        bdbool = true;
    }

    //The button feeds in an integer representing the character
    //With the given number, perform the following logic
    public void EquipCharacter(int j)
    {
        //Go through each board in the array
        for (int i = 0; i < bdEquipButton.Length; i++)
        {
            //If a different character is currently active, set its status to 1 "Bought"
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

    //Brings up the purchase panel
    //Sets the current character number to 'k' to let the code know which character we're buying
    //also sets chbool to true to denote that we're buying a character not a board.
    public void BuyCharacter(int k)
    {
        purchasePanelDark.SetActive(true);
        purchasePanel.SetActive(true);
        watchAdPanel.SetActive(false);

        previewCharacter[k].SetActive(true);

        characterNumber = k;
        chbool = true;
    }

    //Listener method
    public void Purchase()
    {
        //If the user wants to buy a board
        if (bdbool)
        {
            //Check if there is enough money
            if (PlayerPrefs.GetInt("money", 0) - bdprice[boardNumber] >= 0)
            {
                bdbool = false;
                PlayerPrefs.SetInt("board" + boardNumber, 1);
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money", 0) - bdprice[boardNumber]));
                previewBoard[boardNumber].SetActive(false);
            }
            else
            {
                noMoneyPreviewBoard[boardNumber].SetActive(true);
                NoMoneyDark.SetActive(true);
            }
        }
        //If the user wants to buy a character
        if (chbool)
        {
            if (PlayerPrefs.GetInt("money", 0) - chprice[characterNumber] >= 0)
            {
                chbool = false;
                PlayerPrefs.SetInt("character" + characterNumber, 1);
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money", 0) - chprice[characterNumber]));
                previewCharacter[characterNumber].SetActive(false);
            }
            else
            {
                noMoneyPreviewCharacter[characterNumber].SetActive(true);
                NoMoneyDark.SetActive(true);
            }
        }
    }

    //Listener method: called when the user clicks "No" in the purchase panel
    //Logic self explanatory
    public void DontPurchase()
    {
        if (bdbool)
        {
            previewBoard[boardNumber].SetActive(false);
            bdbool = false;
        }

        if (chbool)
        {
            previewCharacter[characterNumber].SetActive(false);
            chbool = false;
        }
    }

    //when user has no money :( *sob*
    public void NoMoneyOKButton()
    {
        if (bdbool)
        {
            previewBoard[boardNumber].SetActive(false);
            noMoneyPreviewBoard[boardNumber].SetActive(false);
            bdbool = false;

            NoMoneyDark.SetActive(false);
        }

        if (chbool)
        {
            previewCharacter[characterNumber].SetActive(false);
            noMoneyPreviewCharacter[characterNumber].SetActive(false);
            chbool = false;

            NoMoneyDark.SetActive(false);
        }
    }

    //Barrell
    public void AdCharacterBuy(int k)
    {
        purchasePanelDark.SetActive(true);
        purchasePanel.SetActive(false);
        watchAdPanel.SetActive(true);

        adPreviewCharacter[k].SetActive(true);

        characterNumber = k;
    }

    //Call Admob to show rewarded based video
    public void WatchAd()
    {
        Admob.Instance.ShowRewardBasedVideo("Shop");
    }

    //Callback function - Once the Ad finishes this method will be called
    public void RewardAd()
    {
        PlayerPrefs.SetInt("adch" + characterNumber, (PlayerPrefs.GetInt("adch" + characterNumber, 0) + 1));

        purchasePanelDark.SetActive(false);
        adPreviewCharacter[characterNumber].SetActive(false);

        if (PlayerPrefs.GetInt("adch" + characterNumber, 0) == 3)
        {
            PlayerPrefs.SetInt("character" + characterNumber, 1);
        }
    }

    //The user doesn't want free Barrell. wtf
    public void NoWatchAd()
    {
        purchasePanelDark.SetActive(false);
        adPreviewCharacter[characterNumber].SetActive(false);
    }
}
