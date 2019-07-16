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
/*#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                playerBehaviour.Jump();

            }
        }


#else*/
      /*  if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                playerBehaviour.Jump();

            }
        }*/
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerBehaviour.Jump();
            
        }

//#endif
    }

    public void ResetPlayer()
    {
        playerBehaviour.transform.position = PlayerStartPosition;
    }

    public void SwitchControlPlayer(bool state)
    {
        playerBehaviour.SwitchRigidbody(state);

    }

    public void ScrollPlayer(float scrollDis, float speed)
    {
        playerBehaviour.Scroll(scrollDis, speed);
      

    }
}
