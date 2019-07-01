using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    private Rigidbody2D rigidbody2;



    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        rigidbody2.gravityScale = 0;
        rigidbody2.AddForce(new Vector2(0,500));
        rigidbody2.gravityScale = 1;

    }


}
