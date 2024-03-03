using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class Bullet : MonoBehaviour
{
  private Rigidbody2D myRigidbody2D;

  public float speed;
    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }
    
    private void Fire()
    {
      myRigidbody2D.velocity = Vector2.up * speed; 
    }
}
