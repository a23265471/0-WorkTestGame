using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerJsonData playerData;

    private GameManager gameManager;

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
        gameManager = GameFacade.GetInstance().gameManager;
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
        rigidbody2.gravityScale = 0;
        rigidbody2.mass = 0;
        rigidbody2.velocity = Vector2.zero;
        rigidbody2.AddForce(new Vector2(0, playerData.JumpForce));
      
        rigidbody2.gravityScale = playerData.Gravity;
        rigidbody2.mass = playerData.Weight;

        clickCount += 1;
        Debug.Log(playerData.Score);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameManager.GameOver();


        }




    }


}
