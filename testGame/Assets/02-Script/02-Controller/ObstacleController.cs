using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

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
        public int Size;
        [Header("速度範圍")]
        public float MinSpeed;
        public float MaxSpeed;
        [Header("最大扇形數")]
        public int MaxSector;
        [Header("出現比例")]
        [Range(0,10)]
        public int AppearProportion;


    }

    private void Awake()
    {
        Init();


    }

    private void Start()
    {
        CreatObstacle(1);
    }

    private void Init()
    {
        
        createObstacle = gameObject.GetComponent<CreateObstacle>();
        InitObstacleSetting();
        

    }

    public void CreatObstacle(int Level)
    {
        int randomRange = Random.Range(0, 100);
        if (randomRange <= levelSetting.MaxCircleAmountAppearProbability)
        {
            for (int j = 0; j < levelSetting.MaxCircleAmount; j++)
            {
                CreateCircle(-1);
            }

        }
        else
        {
            CreateCircle(-1);

        }

    }
    
    private void CreateCircle(int appearedCircleSize)
    {
        int circleAppearProportionSum = levelSetting.Circle_L.AppearProportion + levelSetting.Circle_M.AppearProportion + levelSetting.Circle_S.AppearProportion;
        int sizeAppearRange = Random.Range(0, circleAppearProportionSum);
        

        if (sizeAppearRange <= levelSetting.Circle_S.AppearProportion)
        {
            createObstacle.CreateOneObstacle(levelSetting.Circle_S.Size, levelSetting.Circle_S.MaxSector, levelSetting.Circle_S.MinSpeed, levelSetting.Circle_S.MaxSpeed);
        }
        else if (sizeAppearRange > levelSetting.Circle_S.AppearProportion && sizeAppearRange <= (levelSetting.Circle_S.AppearProportion + levelSetting.Circle_M.AppearProportion))
        {
            createObstacle.CreateOneObstacle(levelSetting.Circle_M.Size, levelSetting.Circle_M.MaxSector, levelSetting.Circle_M.MinSpeed, levelSetting.Circle_M.MaxSpeed);
        }
        else
        {
            createObstacle.CreateOneObstacle(levelSetting.Circle_L.Size, levelSetting.Circle_M.MaxSector, levelSetting.Circle_M.MinSpeed, levelSetting.Circle_M.MaxSpeed);
        }


    }



    private void InitObstacleSetting()
    {
        levelSetting.Circle_S.Size = 0;
        levelSetting.Circle_M.Size = 1;
        levelSetting.Circle_L.Size = 2;
        
    }


}
