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

    [SerializeField]
    public LevelSetting levelSetting;

    [System.Serializable]
    public struct LevelSetting
    {
        [Header("關卡機率設定")]
        public int Level;
        [Range(1,3)]
        public int MaxCircleAmount;
        [Range(1, 100)]
        public int MaxCircleAmountAppearProbability;

        [Header("障礙物數值設定")]
        public PartOfObstacle Circle_S;
        public PartOfObstacle Circle_M;
        public PartOfObstacle Circle_L;
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
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreatObstacle();
        }

    }




    public void CreatObstacle()
    {
        int randomRange = Random.Range(0, 100);

        if (randomRange <= levelSetting.MaxCircleAmountAppearProbability)
        {          
            PartOfObstacle[] appeared = new PartOfObstacle[] { levelSetting.Circle_S, levelSetting.Circle_M, levelSetting.Circle_L };
            int currentRandomLength = appeared.Length;
            for (int j = 0; j < levelSetting.MaxCircleAmount; j++)
            {

                CreateCircle(ref appeared);
              //  Debug.Log("Create One Circle");
            }
        }
        else
        {
            //CreateCircle(-1);

        }

    }
    
    private void CreateCircle(ref PartOfObstacle[] appearedCircleList/*,ref int currentRandomLength*/)
    {
        int sizeAppearRange;
        int sizeAppearRangeSum = 0;
        int currentAppearRange = 0;

        for (int i = 0; i < appearedCircleList.Length; i++)
        {
            if (appearedCircleList[i].Size != ObstacleSize.Null)
            {
                sizeAppearRangeSum += appearedCircleList[i].AppearProportion;

            }

        }

        sizeAppearRange = Random.Range(0, sizeAppearRangeSum);

        for (int j = 0; j < appearedCircleList.Length; j++)
        {

            if (appearedCircleList[j].Size != ObstacleSize.Null)
            {
                if (sizeAppearRange > currentAppearRange && sizeAppearRange <= (currentAppearRange + appearedCircleList[j].AppearProportion))
                {
                    createObstacle.CreateOneObstacle((int)appearedCircleList[j].Size, appearedCircleList[j].MaxSector, appearedCircleList[j].MinSpeed, appearedCircleList[j].MaxSpeed);
                    currentAppearRange += appearedCircleList[j].AppearProportion;
                    appearedCircleList[j].Size = ObstacleSize.Null;
                    break;
                }


            }


        }





    }



    private void InitObstacleSetting()
    {
        levelSetting.Circle_S.Size = ObstacleSize.Small;
        levelSetting.Circle_M.Size = ObstacleSize.Medium;
        levelSetting.Circle_L.Size = ObstacleSize.Large;
        
    }


}
