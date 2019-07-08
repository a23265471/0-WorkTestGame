using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    #region Panel
    [SerializeField]
    private GameObject GameOverCanvas;

    #endregion

    [SerializeField]
    int Level;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Level = 1;

    }

    public void ChangeLevel(int level)
    {
        Level = level;
 //       Level= Mathf.Clamp


    }
          
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);

    }




}
