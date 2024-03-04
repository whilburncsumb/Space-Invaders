using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer rend;
    public float speed;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(FlipSprite());
    }
    
    private void Fire()
    {
        myRigidbody2D.velocity = Vector2.down * speed; 
    }
    IEnumerator FlipSprite()
    {
        while (true)
        {
            rend.flipX = !rend.flipX;
            yield return new WaitForSeconds(.05f);
        }
    }
}
