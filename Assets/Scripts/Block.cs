using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health = 4;
    public Sprite[] animationFrames;
    //prefab particle systems
    public GameObject blockDustParticle;

    public void lowerHealth()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        health--;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
           rend.sprite = animationFrames[health-1]; 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("block hit!");
        if (other.CompareTag("EnemyBullet") || other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(blockDustParticle, transform.position, Quaternion.identity);
            lowerHealth();
        }
    }
}
