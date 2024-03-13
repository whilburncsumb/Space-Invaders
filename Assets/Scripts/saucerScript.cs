using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour
{
    public float advanceSpeed;
    public delegate void DeathEventHandler(int pointValue);
    public event DeathEventHandler OnDeath;
    public Animator anim;
    public int points;
    private bool stop;
    public GameObject invaderExplosionParticle;
    
    void Start()
    {
        points = 100;
        anim.Play("invaderS");
        stop = false;
    }
    public void Die()
    {
        OnDeath?.Invoke(points);
        anim.Play("enemyDeath");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Invader entered collision");
        if (other.CompareTag("PlayerBullet") && !stop)
        {
            Destroy(other.gameObject);
            anim.SetTrigger("killTrigger");
            GameObject particle = Instantiate(invaderExplosionParticle, transform.position, Quaternion.identity);
            Die();
            stop = true;
        }
    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            transform.Translate(Vector3.left * (advanceSpeed * Time.deltaTime));
        }
        
        if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }
    
    void DestroyEnemy()
    {
        // Debug.Log("enemy explosion done");
        Destroy(this.gameObject);
    }
}
