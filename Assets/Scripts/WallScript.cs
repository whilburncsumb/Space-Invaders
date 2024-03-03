using System;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public GridScript gridController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision happened");
        if (other.CompareTag("Enemy"))
        {
            // Signal the grid controller that an enemy has touched the wall
            gridController.HandleWallCollision();
            // Debug.Log("Enemy hit the edge, fleet will reverse.");
        }
    }
    
}