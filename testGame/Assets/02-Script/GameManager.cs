using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{






    private void Start()
    {
        StartCoroutine("GetText");


       
        


    }

    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/test/PHP.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.data);
            }


        }


    }


}
