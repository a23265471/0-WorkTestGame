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
        playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerBehaviour.Jump();


        }
    }



}
