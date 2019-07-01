using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [System.Serializable]
    public struct JumpParameter
    {
        public AnimationCurve JumpVelocity;

        [Header("FallData")]

        public float MaxGravity;
        public float FallAcceleration;

        [Header("JumpData")]
        public float MaxJumpVelocity;
        public float MinJumpVelocity;
        public float JumpAcceleration;
    }

    public JumpParameter jumpParameter;


}
