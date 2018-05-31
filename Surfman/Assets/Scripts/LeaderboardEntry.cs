using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry {

    private Sprite ProfilePic;
    private string Name;
    private string Score;

    private GameObject Entry;
    private GameObject ImageObj;
    private GameObject NameObj;
    private GameObject ScoreObj;


    public LeaderboardEntry()
    {
        this.Name = "Dummy";
        this.Score = "10000";
    }

    public LeaderboardEntry(string name, string score)
    {
        //this.ProfilePic = pic;
        this.Name = name;
        this.Score = score;
    }

    public GameObject MakeGameObject()
    {
        //Holder object is a child of LeaderboardContainer Object
        GameObject entry = new GameObject("Entry");
        Entry = entry;

        //Create a new image for each leaderboard entry and set it to be a child of Holder
        GameObject imageHolder = new GameObject("Image");
        RectTransform imagerf = imageHolder.AddComponent<RectTransform>();
        imagerf.SetParent(entry.transform);
        //Set transform properties
        imagerf.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        imagerf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        imagerf.sizeDelta = new Vector2(150.0f, 150.0f);
        Image profilePic = imageHolder.AddComponent<Image>();
        this.ImageObj = imageHolder;
        //profilePic.sprite = this.ProfilePic;

        //Create a text gameobject to hold player name
        GameObject name = new GameObject("Name");
        RectTransform namerf = name.AddComponent<RectTransform>();
        name.GetComponent<RectTransform>().SetParent(entry.transform);
        //set transform properties
        namerf.localPosition = new Vector3(325.0f, 0.0f, 0.0f);
        namerf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        namerf.sizeDelta = new Vector2(350.0f, 100.0f);
        //set Text properties
        Text playerName = name.AddComponent<Text>();
        playerName.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerName.fontSize = 70;
        playerName.color = Color.black;
        playerName.GetComponent<Text>().text = this.Name;
        this.NameObj = name;

        //Create a text gameobject to hold player score
        GameObject score = new GameObject("Score");
        RectTransform scorerf = score.AddComponent<RectTransform>();
        score.GetComponent<RectTransform>().SetParent(entry.transform);
        //set transform properties
        scorerf.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
        scorerf.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        scorerf.sizeDelta = new Vector3(300.0f, 100.0f);
        //set Text properties
        Text playerScore = score.AddComponent<Text>();
        playerScore.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerScore.fontSize = 70;
        playerScore.color = Color.black;
        playerScore.GetComponent<Text>().text = this.Score;
        this.ScoreObj = score;

        return entry;
    }

    public void AddSprite(Sprite pic)
    {
        this.ProfilePic = pic;
        ImageObj.GetComponent<Image>().sprite = pic;
    }

    public GameObject GetImageObj()
    {
        return ImageObj;
    }

    public GameObject GetNameObj()
    {
        return NameObj;
    }

    public GameObject GetScoreObj()
    {
        return ScoreObj;
    }
}
