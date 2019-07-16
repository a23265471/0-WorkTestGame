using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour playerBehaviour;
    private PlayerJsonData playerData;
    private GameManager gameManager;
    public bool IsMove;

 //   public Text text;

    [SerializeField]
    private Transform StartPosition;


    #region Component
    private Rigidbody2D rigidbody2;

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
    private void Init()
    {
        playerBehaviour = this;
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameFacade.GetInstance().gameManager;
        StartCoroutine(LoadData());

        
     //   gameObject.transform.parent.gameObject.transform.position = StartPosition.position;
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

       /* Debug.Log(playerData.Score);
        text.text = playerData.Score.ToString();/*/
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

      //  Debug.Log(JsonUtility.ToJson(playerData));



    }

    public void SwitchRigidbody(bool State)
    {

        rigidbody2.simulated = State;
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
        switch (other.tag)
        {
            case "Obstacle":
                gameManager.GameOver();

                break;
            case "Scroll":
                //   Debug.Log("f");
                gameManager.NextObstacle();
                
                break;


        }


    }

    public void Scroll(float scrollDis,float speed)
    {
        float dis = 0;
        IsMove = true;
        StartCoroutine(ScrollPosition(dis, scrollDis, speed));

    }

    IEnumerator ScrollPosition(float dis,float ScrollPos,float speed)
    {
        Vector3 currPos = transform.parent.gameObject.transform.position;
        while (dis < 1) 
        {
            transform.parent.gameObject.transform.position = Vector3.Lerp(transform.parent.gameObject.transform.position, currPos + new Vector3(0, ScrollPos, 0), dis);

            dis += speed * Time.deltaTime;
            yield return null;
        }
        IsMove = false;
        Debug.Log(IsMove);
    }

}
