using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public enum ObstacleSize
    {
        Small=0,
        Medium=1,
        Large=2,
        Null=-1

    }

    private CreateObstacle createObstacle;
    private GameObject[] currentObstacle;
    private GameObject[] NextObstacle;

    [SerializeField]
    private Vector3 nextObstaclePosition;

    [SerializeField]
    public LevelSetting levelSetting;

    [System.Serializable]
    public struct LevelSetting
    {
        [Header("關卡機率設定")]
        public int Level;
         
        [Header("圓環數比例")]
        public CircleAmountProportion[] circleAmountProportion;
        

        [Header("障礙物數值設定")]
        public PartOfObstacle Circle_S;
        public PartOfObstacle Circle_M;
        public PartOfObstacle Circle_L;
    }

    [System.Serializable]
    public struct CircleAmountProportion
    {
        public string Name;
        [Range(0, 10)]
        public int CircleProportion;


    }
        [System.Serializable]
    public struct PartOfObstacle
    {
        //[Header("圓形的尺寸 S:0 M:1 L:2")]
        public ObstacleSize Size;
        [Header("速度範圍")]
        public float MinSpeed;
        public float MaxSpeed;
        [Header("最大扇形數")]
        public int MaxSector;
        [Header("出現比例")]
        [Range(0,10)]
        public int AppearProportion;


    }

    PartOfObstacle nullPartOfObstacle;

    private void Awake()
    {
        Init();

    }

    private void Start()
    {
      
    }

    private void Init()
    {        
        createObstacle = gameObject.GetComponent<CreateObstacle>();
        InitObstacleSetting();
        currentObstacle = new GameObject[3];
        NextObstacle = new GameObject[3];
    }

    private void InitObstacleSetting()
    {
        levelSetting.Circle_S.Size = ObstacleSize.Small;
        levelSetting.Circle_M.Size = ObstacleSize.Medium;
        levelSetting.Circle_L.Size = ObstacleSize.Large;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreatObstacle(ref currentObstacle);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            createObstacle.RecoverObstacle(currentObstacle);

          //  Debug.Log(currentObstacle.Length);
        }

    }

    public void StartGame()
    {

        ResetObstacle(ref currentObstacle,Vector3.zero);

    }

    /*public void GetCurrentObstacle()
    {
        if (currentObstacle == null)
        {
            currentObstacle = NextObstacle;
            NextObstacle = null;
        }
    }*/

    public void LoadNextObstacle()
    {
        ResetObstacle(ref NextObstacle, nextObstaclePosition);
    }

    public void UnLoadPreObstacle()
    {
        createObstacle.RecoverObstacle(currentObstacle);
     //   currentObstacle = null;
    }

    public void ClearAllObstacle()
    {
        createObstacle.RecoverObstacle(currentObstacle);
        createObstacle.RecoverObstacle(NextObstacle);


     /*   for (int i = 0; i < 3; i++)
        {
            

            currentObstacle[i].transform.position = Vector3.zero;
            NextObstacle[i].transform.position = Vector3.zero;

        
        }*/


        currentObstacle = new GameObject[3];
        NextObstacle = new GameObject[3];
    }

    private void ResetObstacle(ref GameObject[] Obstacle,Vector3 resetPosition)
    {
        CreatObstacle(ref Obstacle);

        GameObject game = createObstacle.GetObject(createObstacle.ObstaclePrefab[4].ID);

        for (int i = 0; i < Obstacle.Length; i++)
        {
            if (Obstacle[i] != null)
            {
                Obstacle[i].transform.parent = game.transform;
                game.transform.position = resetPosition;

                Obstacle[i].transform.localPosition = new Vector3(0,0,0);
                for (int j = 0; j < Obstacle[i].transform.childCount; j++)
                {
                    Obstacle[i].transform.GetChild(j).transform.localPosition = new Vector3(0, 0, 0);
                }

            }
            //  

        }                     


    }

    private void CreatObstacle(ref GameObject[] Obstacle)
    {
        int circleSizeProportionSun = 0;
        int randomRange;
        int currentProportionRange = 0;

        for (int i = 0; i < levelSetting.circleAmountProportion.Length; i++)
        {
            circleSizeProportionSun += levelSetting.circleAmountProportion[i].CircleProportion;
        }

        randomRange = Random.Range(1, circleSizeProportionSun);

        for (int currentCircle = 0; currentCircle < levelSetting.circleAmountProportion.Length; currentCircle++)
        {
            Debug.Log("圓環個數機率 min : " + currentProportionRange + " Max : " + (levelSetting.circleAmountProportion[currentCircle].CircleProportion + currentProportionRange) + " 現在機率 : " + randomRange);

            if (randomRange > currentProportionRange && randomRange <= (levelSetting.circleAmountProportion[currentCircle].CircleProportion + currentProportionRange)) 
            {
                PartOfObstacle[] appeared = new PartOfObstacle[] { levelSetting.Circle_S, levelSetting.Circle_M, levelSetting.Circle_L };
                for (int q = 0; q < (currentCircle + 1); q++) 
                {
                    Obstacle[q] = CreateCircle(ref appeared);//存入當前的障礙物
                }

                break;
            }

            currentProportionRange += levelSetting.circleAmountProportion[currentCircle].CircleProportion;
        }     

    }
    
    private GameObject CreateCircle(ref PartOfObstacle[] appearedCircleList/*,ref int currentRandomLength*/)
    {
        int sizeAppearRange;
        int sizeAppearRangeSum = 0;
        int currentAppearRange = 0;
        GameObject currentCircle = null;

        for (int i = 0; i < appearedCircleList.Length; i++)
        {
            if (appearedCircleList[i].Size != ObstacleSize.Null)
            {
                sizeAppearRangeSum += appearedCircleList[i].AppearProportion;

            }

        }

        sizeAppearRange = Random.Range(1, sizeAppearRangeSum);

        for (int j = 0; j < appearedCircleList.Length; j++)//判斷該製造哪種尺寸的圓
        {

            if (appearedCircleList[j].Size != ObstacleSize.Null)
            {
           //     Debug.Log("機率 Min : " + currentAppearRange + "  MAX : "+ (currentAppearRange + appearedCircleList[j].AppearProportion) + "  現在機率" + sizeAppearRange);

                if (sizeAppearRange > currentAppearRange && sizeAppearRange <= (currentAppearRange + appearedCircleList[j].AppearProportion))
                {
                    currentCircle = createObstacle.CreateOneObstacle((int)appearedCircleList[j].Size, appearedCircleList[j].MaxSector, appearedCircleList[j].MinSpeed, appearedCircleList[j].MaxSpeed);
                    appearedCircleList[j].Size = ObstacleSize.Null;

                    //break;
                }
                currentAppearRange += appearedCircleList[j].AppearProportion;

            }

        }

        return currentCircle;
        
    }

}
