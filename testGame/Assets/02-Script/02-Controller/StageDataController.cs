using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StageDataController : MonoBehaviour
{
    //public PlayerStageData playerStageData;

    public static StageDataController instance;
    public PlayerJsonData PlayerJson;


    private void Awake()
    {
        
        StartCoroutine("GetJson");

    }
    private void Start()
    {

    }

    IEnumerator GetJson()
    {
        WWWForm form = new WWWForm();
        //   form.AddField("MaxFallSpeed", 2);
        form.AddField("Score", 10);

        PlayerJsonData playerDatas = new PlayerJsonData();
       // playerDatas.Score = 10;

        string test2 = JsonUtility.ToJson(playerDatas);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/PHP.php", form))
        {

        //    www.SetRequestHeader("Score", "10");
           /* www.uploadHandler*/

            yield return www.SendWebRequest();

            //  JSONObject json = JSONObject.Parse(www.downloadHandler.text.Trim("[]".ToCharArray()));

            if (www.isNetworkError || www.isHttpError)
            {

                Debug.Log(www.error);
            }
            else
            {
             //   www.SetRequestHeader("Content-Type", "application/json");

                PlayerJson = JsonUtility.FromJson<PlayerJsonData>(www.downloadHandler.text.Trim("[]".ToCharArray()));

                Debug.Log(www.downloadHandler.text);

            }

        }
     //   Debug.Log(PlayerJson.MaxFallSpeed);


    }

    
}
