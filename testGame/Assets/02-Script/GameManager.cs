using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Start,
        Play,
        GameOver


    }

    public GameState CurrentGameState;
    private PlayerController playerController;
    private ObstacleController obstacleController;

    #region Panel
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private GameObject StartGamePanel;

    #endregion

    [SerializeField]
    int Level;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //  GameFacade.GetInstance().stageDataController.
        playerController = gameObject.GetComponent<PlayerController>();
        obstacleController = GameFacade.GetInstance().obstacleController;
        playerController.enabled = false;
    }

    private void Start()
    {
        StartGame();

    }

    public void StartGame()
    {
        Time.timeScale = 0;
        CurrentGameState = GameState.Start;
        obstacleController.LoadNextObstacle();
        obstacleController.StartGame();
        StartCoroutine(ReadyGameCroutine());
    }

    IEnumerator ReadyGameCroutine()
    {
        yield return new WaitUntil(() => Input.anyKey);
        StartGamePanel.SetActive(false);
        PlayGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        CurrentGameState = GameState.Play;
    //    obstacleController.CreatObstacle();
        StartControlPlayer();
    }

    public void StartControlPlayer()
    {
        playerController.enabled = true;
    }

    public void ChangeLevel(int level)
    {
        Level = level;
 
    }

    public void GameOver()
    {
        CurrentGameState = GameState.GameOver;
        GameOverPanel.SetActive(true);
        obstacleController.ClearAllObstacle();
       /* obstacleController.LoadNextObstacle();
        obstacleController.StartGame();
        */
        //      obstacleController.StartGame();
        Time.timeScale = 0;

        StartCoroutine(RestartGame());

    }

    IEnumerator RestartGame()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        PlayGame();
    }


}
