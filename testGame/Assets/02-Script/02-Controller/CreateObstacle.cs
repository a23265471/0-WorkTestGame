using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject quarterObstaclePrefab_S;
    [SerializeField]
    private GameObject quarterObstaclePrefab_M;
    [SerializeField]
    private GameObject quarterObstaclePrefab_L;

    [SerializeField]
    private int obstacleAmount;

    private GameObject[] quarterObstacle;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

        CreatObjectPool();
        CreateOneObstacle();
        CreateOneObstacle();
        CreateOneObstacle();




    }

    private GameObject CreateOneObstacle()
    {
        GameObject ObstacleParent = new GameObject("Obstacle");

        int[] rotationAngle;
        rotationAngle = new int[] { 0, 90, 180, 270 };
        for (int i = 0; i <= Random.Range(0, 3); i++)
        {
            GameObject obstacle = GetObject();
            int angle;
            obstacle.transform.parent = ObstacleParent.transform;
            obstacle.transform.position = Vector3.zero;
            angle = Random.Range(0, 4);

            while (rotationAngle[angle] == -1)
            {
                angle = Random.Range(0, 4);
                Debug.Log(rotationAngle[angle]);

            }
            obstacle.transform.Rotate(new Vector3(0, 0, rotationAngle[angle]));
            rotationAngle[angle] = -1;
        }

        ObstacleParent.transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));
        ObstacleParent.AddComponent<ObstacleBehaviour>();
        ObstacleParent.GetComponent<ObstacleBehaviour>().RotateSpeed = Random.Range(100f, 200);

        return ObstacleParent;
    }

 /*   IEnumerator SortRotation()
    {




    }
    */

    private void SortObstacle(int currentAngle)
    {
       

    }
       
    private void CreatObjectPool()
    {
        quarterObstacle = new GameObject[obstacleAmount];
        for (int j = 0; j < obstacleAmount; j++)
        {
            GameObject gameObject = Instantiate(quarterObstaclePrefab_S);
            quarterObstacle[j] = gameObject;
            gameObject.SetActive(false);

        }

    }

    public GameObject GetObject()
    {
        for (int i = 0; i < obstacleAmount; i++)
        {
            if (!quarterObstacle[i].activeInHierarchy)
            {
                quarterObstacle[i].SetActive(true);
                return quarterObstacle[i];
            }

        }


        return null;
    }


}
