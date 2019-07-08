using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


enum spriteAtlas {Player,Obstacle_S, Obstacle_M, Obstacle_L }

public class SpriteController : MonoBehaviour
{
    [SerializeField]
    private spriteAtlas CurrentSprite;

    [SerializeField]
    private SpriteAtlas Atlas;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Init();

    }

    private void Start()
    {
    

    }

    private void Init()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Atlas.GetSprite(CurrentSprite.ToString());

     //   Debug.Log(CurrentSprite.ToString());

        //   StartCoroutine(GetSprite());



    }



    /*IEnumerator GetSprite()
    {
        yield return new wai();



    }
    */



}
