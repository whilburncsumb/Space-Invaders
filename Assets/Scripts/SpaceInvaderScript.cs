﻿
using UnityEngine;

public class SpaceInvader : MonoBehaviour
{
    public int invaderType = 1;
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

    public void setType(int input)
    {
        invaderType = input;
        switch (input)
        {
            case 0:
            {
                points = 10;
                break;
            }
            case 1:
            {
                points = 20;
                break;
            }
            case 2:
            {
                points = 30;
                break;
            }
        }
    }
    
    void DestroyEnemy()
    {
        Debug.Log("enemy explosion done");
        Destroy(this.gameObject);
    }
}
