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
   // private StageDataController stageDataController;


    #endregion


    #region Data

    public PlayerJsonData PlayerData;

    #endregion

    public void Initialize()
    {
        gameManager = gameObject.GetComponent<GameManager>();
     //   stageDataController = gameObject.GetComponent<StageDataController>();
        StartCoroutine("LoadData");

      //  if(GetComponent<StageDataController>())
    }

    IEnumerator LoadData()
    {

        yield return new WaitForEndOfFrame();
        PlayerData = gameObject.GetComponent<StageDataController>().PlayerJson;

        //Debug.Log(PlayerData.MaxFallSpeed);
    }


    private void Awake()
    {
        GetInstance();

    }
    private void Start()
    {
       

    }
}
