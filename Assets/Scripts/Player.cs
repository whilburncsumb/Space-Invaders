using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;

  public Transform shottingOffset;

  private void Start()
  {
    Enemy.OnEnemyDeath += EnemyOnOnEnemyDeath;

    void EnemyOnOnEnemyDeath(int pointValue)
    {
      Debug.Log("Players recieved EnemyDeath, Points: " + pointValue);
    }
  }

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }
}
