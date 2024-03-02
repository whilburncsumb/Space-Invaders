using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int points = 20;
    public delegate void EnemyDeath(int pointValue);
    public static event EnemyDeath OnEnemyDeath;
    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Ouch!");
      Destroy(collision.gameObject);
      OnEnemyDeath.Invoke(points);
      // Destroy(gameObject);
      GetComponent<Animator>().SetTrigger("killTrigger");
    }
    
    void DestroyEnemy()
    {
        Debug.Log("enemy explosion done");
        Destroy(this.gameObject);
    }
}
