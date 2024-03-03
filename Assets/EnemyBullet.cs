using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public float speed;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
    }
    
    private void Fire()
    {
        myRigidbody2D.velocity = Vector2.down * speed; 
    }
}
