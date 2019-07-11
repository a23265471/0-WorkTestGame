using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerBehaviour playerBehaviour;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) 
        {
        //    Debug.Log(playerBehaviour = null);
            playerBehaviour.Jump();


        }
    }



}
