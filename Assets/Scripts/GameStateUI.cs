using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//script written by Oliver Jenkinson for this project (with reference to previously written code from Game 2)

public class GameStateUI : MonoBehaviour
{
    public GameObject startUI, gameUI, endUI, pauseUI, infoUI, infoStartButton, infoResumeButton, infoRestartButton;
    public TextMeshProUGUI gameTimerText, ghostsMatchedText, endGameText;
    float gameTimer;

    public int ghostsMatched, ghostsLeft;

    public List<GameObject> ghostList;

    bool gameStarted, gameEnded, endGameLogged, gamePaused;

    public List<GenerateGrave> gravesList;

    public List<string> endGameComments;

    
    void Awake()
    {
        //foreach (GenerateGrave grave in gravesList)
        //{
        //    for (int i = 0; i < gravesList.Count; i++)
        //    {
        //        if (grave.job == gravesList[i].job && grave != gravesList[i])
        //        {
        //            grave.job = grave.ghostStats.GenerateJob();
        //        }
        //    }
        //}

        startUI.SetActive(true);
        endUI.SetActive(false);
        gameUI.SetActive(false);
        infoUI.SetActive(false);
        pauseUI.SetActive(false);

        //gameTimer = 0f;

        ghostList = new List<GameObject>();

        //FillGhostList();
        Cursor.lockState = CursorLockMode.Confined;

        //CountMatches();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gamePaused && !gameEnded)
        {
            if (ghostList.Count <= 0)
                FillGhostList();

            if (ghostsLeft > 0)
                gameTimer += Time.deltaTime;

            gameTimerText.text = gameTimer.ToString("0000");

            ghostsMatchedText.text = ghostsMatched.ToString() + " Ghosts Matched" + "\n" + ghostsLeft.ToString() + " Ghosts Left";

            CountMatches();

            if (ghostsLeft <= 0)
            {
                gameEnded = true;
                
            }

            if (Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }
        }

        if (gameEnded && !endGameLogged)
        {
            EndGame();
        }

    }

    public void FillGhostList()
    {

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Ghost").Length; i++)
        {
            ghostList.Add(GameObject.FindGameObjectsWithTag("Ghost")[i]);
        }

        CountMatches();
    }

    public void CountMatches()
    {
        ghostsMatched = 0;
        ghostsLeft = 0;

        foreach (GameObject ghost in ghostList)
        {
            if (!ghost.GetComponent<Ghost>().isIdentified)
            {
                ghostsLeft++;
            }
            else
            {
                ghostsMatched++;
            }
        }
    }

    public void StartGame()
    {
        //foreach (GenerateGrave grave in gravesList)
        //{
        //    grave.enabled = true;
        //}

        startUI.SetActive(false);
        endUI.SetActive(false);
        gameUI.SetActive(true);
        infoUI.SetActive(false);
        pauseUI.SetActive(false);

        gameTimer = 0f;

        //ghostList = new List<GameObject>();

        FillGhostList();

        Cursor.lockState = CursorLockMode.Locked;
        gameStarted = true;
    }

    public void EndGame()
    {
        startUI.SetActive(false);
        endUI.SetActive(true);
        gameUI.SetActive(false);
        infoUI.SetActive(false);
        pauseUI.SetActive(false);

        endGameText.text = endGameComments[Random.Range(0, endGameComments.Count)] + "\n" + "It took you " + gameTimer.ToString("0000") + " seconds";
        Cursor.lockState = CursorLockMode.Confined;

        endGameLogged = true;
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            gameUI.SetActive(false);
            pauseUI.SetActive(true);
            infoUI.SetActive(false);
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            gameUI.SetActive(true);
            pauseUI.SetActive(false);
            infoUI.SetActive(false);
            gamePaused = false;
        }
    }

    public void GameInfo()
    {
        infoUI.SetActive(true);
        startUI.SetActive(false);
        pauseUI.SetActive(false);

        if (!gamePaused)
        {
            infoResumeButton.SetActive(false);
            infoRestartButton.SetActive(false);
            infoStartButton.SetActive(true);
        }
        else
        {
            infoResumeButton.SetActive(true);
            infoRestartButton.SetActive(true);
            infoStartButton.SetActive(false);
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
