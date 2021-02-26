using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    public bool isSucces;
    public bool isGameRunning;

    public float finishDelay;

    public int level;

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

    void Start()
    {
        level = PlayerPrefs.GetInt("Level", level);
        LevelManager.instance.SetLevel();
        GameSceneManager.instance.levelText.text = ("Level " + (level + 1).ToString());
    }

    public void StartGame()
    {
        isGameRunning = true;
        level = PlayerPrefs.GetInt("Level", level);
        GameSceneManager.instance.levelBarText.text = (level + 1).ToString();
    }

    public IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(finishDelay);
        isGameRunning = false;

        if (isSucces && PlayerManager.instance.isFinished)
        {
            GameSceneManager.instance.nextLevelRect.SetActive(true);
        }
        else if(!isSucces && PlayerManager.instance.isFinished)
        {
            GameSceneManager.instance.retryLevelRect.SetActive(true);
        }
    }


}
