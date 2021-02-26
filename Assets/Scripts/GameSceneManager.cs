using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    public GameObject nextLevelRect;
    public GameObject retryLevelRect;
    public GameObject nextLevelButton;
    public GameObject retryButton;
    public GameObject tapToPlayButton;
    public Text levelText;
    public Text levelBarText;
    public GameObject tutorialImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TapToPlayButtonClick()
    {
        GameManager.instance.StartGame();
        if (GameManager.instance.level == 0)
            tutorialImage.SetActive(true);

    }

    public void NextLevelButtonClick()
    {
        GameManager.instance.level++;
        levelText.text = ("Level " + (GameManager.instance.level + 1).ToString());
        PlayerPrefs.SetInt("Level", GameManager.instance.level);
        PlayerManager.instance.SetDefault();
        LevelManager.instance.SetLevel();
    }

    public void RetryButtonClick()
    {
        PlayerManager.instance.SetDefault();
        LevelManager.instance.SetLevel();
    }


}
