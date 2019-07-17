using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
//using UnityEngine.Networking;

public class StageDataController : MonoBehaviour
{
    //public PlayerStageData playerStageData;

    public static StageDataController instance;
    public PlayerJsonData PlayerJson;

    private void Awake()
    {

        GetData();
    }

    private void Start()
    {

    }

    public void GetData()
    {
        StartCoroutine("GetJson");

    }

    IEnumerator GetJson()
    {
        WWWForm form = new WWWForm();
        //   form.AddField("MaxFallSpeed", 2);
        //
        //     Debug.Log(form);

        PlayerJsonData playerDatas = new PlayerJsonData();
        playerDatas.Gravity = 5;
        playerDatas.Score = 10;
        form.AddField("Score", playerDatas.Score.ToString());
        form.AddField("Gravity", playerDatas.Gravity.ToString());

        WWW www=new WWW("http://localhost/PHP.php");
        yield return www;
        //   string test2 = JsonUtility.ToJson(playerDatas);
        PlayerJson = new PlayerJsonData();
        PlayerJson = JsonUtility.FromJson<PlayerJsonData>(www.text.Trim("[]".ToCharArray()));

        Debug.Log(PlayerJson.JumpForce);



        /*  using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/PHP.php", form))
          {

              yield return www.SendWebRequest();

              // yield return www.SendWebRequest();

              //  JSONObject json = JSONObject.Parse(www.downloadHandler.text.Trim("[]".ToCharArray()));

              if (www.isNetworkError || www.isHttpError)
              {

                  Debug.Log(www.error);
              }
              else
              {
                  //   www.SetRequestHeader("Content-Type", "application/json");
                  PlayerJson = new PlayerJsonData();
                  PlayerJson = JsonUtility.FromJson<PlayerJsonData>(www.downloadHandler.text.Trim("[]".ToCharArray()));

                  Debug.Log(www.downloadHandler.text);

              }

          }*/
        //   Debug.Log(PlayerJson.MaxFallSpeed);


    }


}
