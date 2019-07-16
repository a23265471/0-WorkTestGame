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
    }

    private void Start()
    {
        StartGame();

    }

    private void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (StartGamePanel.activeInHierarchy)
            {
                StartGamePanel.SetActive(false);

            }
            else
            {
                StartGamePanel.SetActive(true);

            }

        }*/
    }

    public void StartGame()
    {
        Time.timeScale = 0;
        CurrentGameState = GameState.Start;
        obstacleController.LoadNextObstacle();
        obstacleController.StartGame();
        playerController.SwitchControlPlayer(false);
        StartCoroutine(ReadyGameCroutine());
    }

    IEnumerator ReadyGameCroutine()
    {
        yield return new WaitUntil(() => Input.anyKey);
        StartGamePanel.SetActive(false);
        Time.timeScale = 1;

        yield return new WaitForFixedUpdate();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        PlayGame();
    }

    public void PlayGame()
    {
        CurrentGameState = GameState.Play;
        //    obstacleController.CreatObstacle();
        playerController.SwitchControlPlayer(true);
    }
  
    public void GameOver()
    {
        CurrentGameState = GameState.GameOver;
        GameOverPanel.SetActive(true);
        obstacleController.ClearAllObstacle();
        obstacleController.LoadNextObstacle();
        obstacleController.StartGame();
        
        //      obstacleController.StartGame();
        Time.timeScale = 0;
        playerController.SwitchControlPlayer(false);
        StartCoroutine(RestartGame());

    }

    IEnumerator RestartGame()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        playerController.ResetPlayer();

        yield return new WaitForFixedUpdate();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        PlayGame();

    }

    public void NextObstacle()
    {
        playerController.ScrollPlayer(-70, 0.5f);
        obstacleController.ScrollObject(-70, 0.5f);
        obstacleController.UnLoadCurrentObstacle();
        obstacleController.UpdateCurrentObstacle();
        StartCoroutine(LoadNextObstacle());
    }

    IEnumerator LoadNextObstacle()
    {

        yield return new WaitUntil(() => (!obstacleController.isMove && !PlayerBehaviour.playerBehaviour.IsMove));
        obstacleController.LoadNextObstacle();
        Debug.Log("LoadNext");
    }

}
