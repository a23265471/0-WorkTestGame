using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerJsonData playerData;

    #region Component
    private Rigidbody2D rigidbody2;

    #endregion

    #region MoveParameter
    private float currentSpeed;
    IEnumerator MoveSmoothly;
    IEnumerator GravityCroutine;


    int clickCount;

    #endregion
    private void Awake()
    {
        Init();

       // Debug.Log(playerData.MaxFallSpeed);
    }

    private void Start()
    {
        

    }

    #region Init
    private void InitParameter()
    {
        currentSpeed = 0;
    }

    private void Init()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(LoadData());
        MoveSmoothly = null;
        GravityCroutine = null;
    }
    #endregion

    private void Update()
    {
       //Gravity();
    }

    

    IEnumerator LoadData()
    {

        yield return new WaitForEndOfFrame();
        playerData = GameFacade.GetInstance().PlayerData;
       // Debug.Log(playerData.MaxFallSpeed);
    }

    public void Jump()
    {
        /*rigidbody2.gravityScale = 0;
        rigidbody2.mass = 0;
        rigidbody2.AddForce(new Vector2(0,2000));
        rigidbody2.gravityScale = 1;
        rigidbody2.mass = 5;*/

        clickCount += 1;

        currentSpeed = playerData.JumpSpeed;
        // rigidbody2.velocity = new Vector2(0, currentSpeed);
        if (MoveSmoothly != null)
        {
            StopCoroutine(MoveSmoothly);
        }

        GravityCroutine = moveSmoothly(0, 1000, playerData.FallAcceleration, -playerData.MaxFallSpeed, 0, clickCount, null);

        MoveSmoothly = moveSmoothly(0, playerData.JumpHigh, playerData.JumpAcceleration, 3, playerData.JumpSpeed, clickCount, GravityCroutine);
        StartCoroutine(MoveSmoothly);
        Debug.Log("aa = " + clickCount);
    }

    IEnumerator moveSmoothly(float currentMoveDis ,float dis,float acceleration,float minSpeed ,float maxSpeed,int state,IEnumerator nextCroutine)
    {
      //  float moveTime = playerData.JumpHigh / playerData.JumpSpeed;

        yield return new WaitForFixedUpdate();

        float currentFramMoveDistance;
        currentSpeed -= acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        currentFramMoveDistance = currentSpeed * Time.deltaTime;

        rigidbody2.velocity = transform.rotation * new Vector2(0, currentSpeed);

        Debug.Log(currentMoveDis);

        if (currentMoveDis >= dis)
        {
            rigidbody2.velocity = transform.rotation * new Vector2(0, 0);
            //     Gravity();
            // MoveSmoothly = null;
            StartCoroutine(nextCroutine);
        }
        else
        {
            currentMoveDis += currentFramMoveDistance;
            if (state == clickCount)
            {
            
                StartCoroutine(moveSmoothly(currentMoveDis, dis, acceleration, minSpeed, maxSpeed, state, nextCroutine));

            }

        }


        /* if (currentSpeed > 0)
         {
             StartCoroutine(SlowDownSpeed());
         }*/

    }

    public void Gravity()
    {
        rigidbody2.velocity = new Vector2(0, -playerData.MaxFallSpeed);

        MoveSmoothly= moveSmoothly(0, 1000, playerData.FallAcceleration, -playerData.MaxFallSpeed, 0,clickCount,null);


    }


}
