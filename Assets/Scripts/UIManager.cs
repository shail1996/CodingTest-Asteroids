using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject lifeImage1;
    [SerializeField] GameObject lifeImage2;
    [SerializeField] GameObject lifeImage3;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI userName;

    [Header("StartApp")]
    [SerializeField] GameObject script;
    [SerializeField] TextMeshProUGUI userInputName;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject ship;

    [Header("GameOver")]
    [SerializeField] TextMeshProUGUI endGameScore;
    [SerializeField] GameObject gameOverUI;

    [Header("PauseApp")]
    [SerializeField] GameObject pauseMenu;


    [Header("LeaderBoard-End")]
    [SerializeField] TextMeshProUGUI name1;
    [SerializeField] TextMeshProUGUI name2;
    [SerializeField] TextMeshProUGUI name3;
    [SerializeField] TextMeshProUGUI score1;
    [SerializeField] TextMeshProUGUI score2;
    [SerializeField] TextMeshProUGUI score3;

    [Header("LeaderBoard-Start")]
    [SerializeField] TextMeshProUGUI sname1;
    [SerializeField] TextMeshProUGUI sname2;
    [SerializeField] TextMeshProUGUI sname3;
    [SerializeField] TextMeshProUGUI sscore1;
    [SerializeField] TextMeshProUGUI sscore2;
    [SerializeField] TextMeshProUGUI sscore3;

    private string fileName;
    public List<Score> gameScore;

    private void Start()
    {
        gameScore = new List<Score>();
        fileName = Application.dataPath + "/LeaderBoardData.csv";
        LoadLeaderboard();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseApp();
        }
    }
    public void UpdateLifeCount()
    {
        if (GlobalVariables.life == 1)
        {
            lifeImage2.SetActive(false);
            lifeImage3.SetActive(false);
        }
        else if (GlobalVariables.life == 2)
        {
            lifeImage3.SetActive(false);
        }
        else if(GlobalVariables.life == 0)
        {
            GameOverMenu();
        }
    }

    public void UpdateScore()
    {
        score.text = GlobalVariables.score.ToString();
    }

    public void QuitApp()
    {
        //ClearCSV();
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void StartApp()
    {
        userName.text = userInputName.text;
        GlobalVariables.userName = userInputName.text.ToString().Replace(",", "");
        script.SetActive(true);
        inGameUI.SetActive(true);
        menuUI.SetActive(false);
        ship.SetActive(true);
    }

    public void GameOverMenu()
    {
        endGameScore.text = "Your Score: " + GlobalVariables.score + "(" + GlobalVariables.userName + ")";
        script.SetActive(false);
        inGameUI.SetActive(false);
        ship.SetActive(false);
        gameOverUI.SetActive(true);

        WriteCSV();
    }

    public void RestartApp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseApp()
    {
        script.SetActive(false);
        inGameUI.SetActive(false);
        ship.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeApp()
    {
        pauseMenu.SetActive(false);
        script.SetActive(true);
        inGameUI.SetActive(true);
        ship.SetActive(true);
    }

    private void WriteCSV()
    {
        TextWriter tw = new StreamWriter(fileName, append: true);

        tw.WriteLine(GlobalVariables.userName + ", " + GlobalVariables.score);
        tw.Close();

        // Restart wirh 3 life
        GlobalVariables.life = 3;
        GlobalVariables.score = 0;

        // Ready and update leaderboard
        ReadCSV(false);
    }

    private void ClearCSV()
    {
        TextWriter tw = new StreamWriter(fileName, false);
        tw.WriteLine("Name, Score");
        tw.Close();
    }

    private void ReadCSV(bool onStart)
    {
        string line;
        int lineNumber = 1;
        StreamReader reader = new StreamReader(fileName);
        while ((line = reader.ReadLine()) != null)
        {
            if(lineNumber > 1)
            {
                string[] splitArray = line.Split(',');
                gameScore.Add(new Score(splitArray[0], Int32.Parse(splitArray[1])));
            }
            lineNumber++;
        }
        gameScore = gameScore.OrderByDescending(x => x.score).ToList();
        if (onStart)
        {
            LeaderBoardDataOnStart();
        }
        else
        {
            LeaderBoardData();
        }
    }

    private void LeaderBoardData()
    {
        Debug.Log(gameScore.Count);
        if (gameScore.Count == 1)
        {
            name1.text = gameScore[0].userName;
            score1.text = gameScore[0].score.ToString();
        }
        if (gameScore.Count == 2)
        {
            name1.text = gameScore[0].userName;
            score1.text = gameScore[0].score.ToString();
            name2.text = gameScore[1].userName;
            score2.text = gameScore[1].score.ToString();
        }
        if (gameScore.Count > 2)
        {
            name1.text = gameScore[0].userName;
            score1.text = gameScore[0].score.ToString();
            name2.text = gameScore[1].userName;
            score2.text = gameScore[1].score.ToString();
            name3.text = gameScore[2].userName;
            score3.text = gameScore[2].score.ToString();
        }
    }

    private void LeaderBoardDataOnStart()
    {
        Debug.Log(gameScore.Count);
        if (gameScore.Count == 1)
        {
            sname1.text = gameScore[0].userName;
            sscore1.text = gameScore[0].score.ToString();
        }
        if (gameScore.Count == 2)
        {
            sname1.text = gameScore[0].userName;
            sscore1.text = gameScore[0].score.ToString();
            sname2.text = gameScore[1].userName;
            sscore2.text = gameScore[1].score.ToString();
        }
        if (gameScore.Count > 2)
        {
            sname1.text = gameScore[0].userName;
            sscore1.text = gameScore[0].score.ToString();
            sname2.text = gameScore[1].userName;
            sscore2.text = gameScore[1].score.ToString();
            sname3.text = gameScore[2].userName;
            sscore3.text = gameScore[2].score.ToString();
        }
    }

    private void LoadLeaderboard()
    {
        ReadCSV(true);
    }
}
