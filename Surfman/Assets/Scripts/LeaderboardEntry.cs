using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry 
{

    public GameObject LeaderEntryPrefab;

    private Sprite ProfilePic;
    private string Name;
    private string Score;

    private GameObject Entry;
    private GameObject ImageObj;
    private GameObject NameObj;
    private GameObject ScoreObj;

    //First Second and Third
    private GameObject TopLeaderText;
    private GameObject TopLeaderPic;

    public LeaderboardEntry()
    {
        this.Name = "Dummy";
        this.Score = "10000";
    }

    public LeaderboardEntry(GameObject entry, string name, string score)
    {
        //this.ProfilePic = pic;
        this.Entry = entry;
        this.Name = name;
        this.Score = score;
    }

    public void MakeGameObject()
    {
        //Create a new image for each leaderboard entry and set it to be a child of Holder
        GameObject imageObj = Entry.transform.GetChild(0).GetChild(0).gameObject;
        this.ImageObj = imageObj;

        //Create a text gameobject to hold player name
        GameObject nameObj = Entry.transform.GetChild(1).gameObject;
        nameObj.GetComponent<Text>().text = Name;
        this.NameObj = nameObj;

        //Create a text gameobject to hold player score
        GameObject scoreObj = Entry.transform.GetChild(2).gameObject;
        scoreObj.GetComponent<Text>().text = Score;
        this.ScoreObj = scoreObj;
    }

    public void AddTopLeaderPic(GameObject o)
    {
        TopLeaderPic = o;
    }

    public void AddSprite(Sprite pic)
    {
        this.ProfilePic = pic;
        ImageObj.GetComponent<Image>().sprite = pic;
        if (TopLeaderPic != null)
        {
            TopLeaderPic.GetComponent<Image>().sprite = pic;
        }
    }

    public GameObject GetEntryObj()
    {
        return Entry;
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

    public Sprite getProfileSprite()
    {
        return this.ProfilePic;
    }

    public string getName()
    {
        return this.Name;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public string getScore()
    {
        return this.Score;
    }
}
