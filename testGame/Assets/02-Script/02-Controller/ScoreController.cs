using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreController : MonoBehaviour
{



    private void Awake()
    {

    }

    private void Start()
    {
        //StartCoroutine(GetJson());

    }
    IEnumerator GetJson()
    {
        WWWForm form = new WWWForm();
       

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/PHP.php", form))
        {

            yield return www.SendWebRequest();

            //  JSONObject json = JSONObject.Parse(www.downloadHandler.text.Trim("[]".ToCharArray()));

            if (www.isNetworkError || www.isHttpError)
            {

                Debug.Log(www.error);
            }
            else
            {
                // PlayerJson = JsonUtility.FromJson<PlayerJsonData>(www.downloadHandler.text.Trim("[]".ToCharArray()));

                Debug.Log(www.downloadHandler.text);


            }

        }
        //   Debug.Log(PlayerJson.MaxFallSpeed);


    }
}
