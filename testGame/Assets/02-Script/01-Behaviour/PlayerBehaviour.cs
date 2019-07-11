using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


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
       // StartCoroutine(PostJson());

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
        playerData = GameFacade.GetInstance().stageDataController.PlayerJson;
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
      //  Debug.Log(JsonUtility.ToJson(playerData));



    }

    IEnumerator PostJson()
    {
        WWWForm form = new WWWForm();

        PlayerJsonData playerDatas = new PlayerJsonData();
        playerDatas.Gravity = 5;
        form.AddField("Gravity", playerDatas.Gravity.ToString());
        form.AddField("Score", "10");
      /* form.AddField("JumpForce", playerData.JumpForce.ToString());
        form.AddField("Weight", playerData.Weight.ToString());
        form.AddField("JumpAcceleration", playerData.JumpAcceleration.ToString());*/
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/PHP.php", form))
        {

            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {

                Debug.Log(www.error);
            }
            else
            {
                PlayerJsonData PlayerJson = new PlayerJsonData();
                PlayerJson = JsonUtility.FromJson<PlayerJsonData>(www.downloadHandler.text.Trim("[]".ToCharArray()));

                

              //  Debug.Log(www.downloadHandler.text);
                Debug.Log(JsonUtility.ToJson(PlayerJson));

            }

        }
        //   Debug.Log(PlayerJson.MaxFallSpeed);


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameManager.GameOver();

        }

    }


}
