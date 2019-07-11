using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{

    private static GameFacade instance;

    public static GameFacade GetInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameFacade>();
            if (instance == null)
            {
                throw new System.Exception("GameFacade不存在於場景中，請在場景中添加");
            }
            instance.Initialize();
        }
        return instance;
    }


    public GameManager gameManager;

    #region Controller
    public StageDataController stageDataController;
    public ObstacleController obstacleController;
    #endregion


    #region Data

   // public PlayerJsonData PlayerData;

    #endregion

    public void Initialize()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        stageDataController = gameObject.GetComponent<StageDataController>();
        obstacleController = gameObject.GetComponent<ObstacleController>();

      //  if(GetComponent<StageDataController>())
    }

    

    private void Awake()
    {
        GetInstance();

    }
    private void Start()
    {
       

    }
}
