using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Boomlagoon.JSON;
using System.IO;


public class GameManager : MonoBehaviour
{
 /*   private void Start()
    {
        StartCoroutine("GetText");

    }

    IEnumerator GetText()
    {
        WWWForm form = new WWWForm();
        form.AddField("MaxFallSpeed", 2);

        PlayerDatas playerDatas = new PlayerDatas();
     

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/PHP.php", form))
        {

            yield return www.SendWebRequest();

        //  JSONObject json = JSONObject.Parse(www.downloadHandler.text.Trim("[]".ToCharArray()));
            PlayerDatas eeee = JsonUtility.FromJson<PlayerDatas>(www.downloadHandler.text.Trim("[]".ToCharArray()));

            if (www.isNetworkError || www.isHttpError)
            {

                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(eeee.MaxFallSpeed);

            }

        }

    }

    [System.Serializable]
    private class PlayerDatas
    {
        public int MaxFallSpeed;
        public int FallAcceleration;

        public AnimationCurve fall;



    }*/


}
