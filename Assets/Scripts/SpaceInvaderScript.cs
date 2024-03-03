using System;
using UnityEngine;

public class SpaceInvader : MonoBehaviour
{
    public int invaderType = 1;
    public int points;
    private Animator anim;
    // Declare delegate for the death event
    public delegate void DeathEventHandler(int pointValue);
    // Declare event for the death event
    public event DeathEventHandler OnDeath;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Method to call when the space invader dies
    public void Die()
    {
        // Raise the death event
        OnDeath?.Invoke(points);
        anim.Play("enemyDeath");
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Invader entered collision");
        if (other.collider.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            GetComponent<Animator>().SetTrigger("killTrigger");
            Die();
        }
    }
    
    public void setType(int input)
    {
        invaderType = input;
        anim = GetComponent<Animator>();
        switch (input)
        {
            case 0:
            {
                anim.Play("enemyIdle0");
                points = 10;
                break;
            }
            case 1:
            {
                anim.Play("enemyIdle1");
                points = 20;
                break;
            }
            case 2:
            {
                anim.Play("enemyIdle2");
                points = 30;
                break;
            }
        }
    }
    
    void DestroyEnemy()
    {
        // Debug.Log("enemy explosion done");
        Destroy(this.gameObject);
    }
}
