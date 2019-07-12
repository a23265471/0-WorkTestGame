using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerBehaviour playerBehaviour;
    public Vector3 PlayerStartPosition;


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
        else if (Input.GetKey(KeyCode.W))
        {
            playerBehaviour.Scroll();
        }
    }

    public void ResetPlayer()
    {
        playerBehaviour.transform.position = PlayerStartPosition;
    }

    public void SwitchControlPlayer(bool state)
    {
        playerBehaviour.SwitchRigidbody(state);

    }

}
