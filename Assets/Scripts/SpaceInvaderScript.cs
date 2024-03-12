using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpaceInvader : MonoBehaviour
{
    public int invaderType = 1;
    public int points;
    public float spawnChance;
    public GameObject bullet;
    private Animator anim;
    // Declare delegate for the death event
    public delegate void DeathEventHandler(int pointValue);
    // Declare event for the death event
    public event DeathEventHandler OnDeath;
    private bool stop;
    public bool demoMode = true;
    
    //prefab particle systems
    public GameObject invaderExplosionParticle;
    public GameObject invaderMuzzleParticle;

    public void Start()
    {
        anim = GetComponent<Animator>();
        stop = false;
        setType(invaderType);
    }

    // Method to call when the space invader dies
    public void Die()
    {
        // Raise the death event
        OnDeath?.Invoke(points);
        anim.Play("enemyDeath");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Invader entered collision");
        if (other.CompareTag("PlayerBullet") && !stop)
        {
            Destroy(other.gameObject);
            GetComponent<Animator>().SetTrigger("killTrigger");
            GameObject particle = Instantiate(invaderExplosionParticle, transform.position, Quaternion.identity);
            var mainModule = particle.GetComponent<ParticleSystem>().main;
            // mainModule.startSizeMultiplier = 0.3f;
            Die();
            stop = true;
        }
    }

    private void FixedUpdate()
    {
        // Generate a random number between 0 and 99 (inclusive)
        int randomNumber = Random.Range(0, 1000);

        // Check if the random number is within the spawn chance range
        if (randomNumber < spawnChance && demoMode == false)
        {
            // Spawn the bullet prefab
            GameObject shot = Instantiate(bullet, transform.position, Quaternion.identity);
            Instantiate(invaderMuzzleParticle, transform.position+Vector3.down*.2f, Quaternion.Euler(90, 0, 0));
            Destroy(shot, 3f);
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
        Destroy(this.gameObject);
    }
}
