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

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverCanvas.SetActive(true);

    }




}
