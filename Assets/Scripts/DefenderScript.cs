using System;
using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Defender : MonoBehaviour
{
  public GameObject invader;
  public GameObject bullet;
  public GameObject grid;
  public Transform shottingOffset;
  public float moveSpeed;
  public Animator anim;
  private AudioSource _audio;
  public AudioClip explosion;
  public AudioClip failSound;
  public bool invincible;
  
  //prefab particle systems
  public GameObject playerExplosionParticle;
  public GameObject playerFireParticle;
  public GameObject playerMuzzleParticle;

  private void Start()
  {
    invincible = false;
    anim = GetComponent<Animator>();
    anim.speed = 1;
    _audio = GetComponent<AudioSource>();
  }

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Instantiate(playerMuzzleParticle, shottingOffset.position, Quaternion.Euler(-90, 0, 0));
        _audio.Play();
        Destroy(shot, 3f);
      }
      
      transform.Translate(new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed,0,0));
      if (transform.position.x > 9)
      {
        transform.position = new Vector3(9, transform.position.y, 0);
      } 
      else if (transform.position.x < -9)
      {
        transform.position = new Vector3(-9, transform.position.y, 0);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      // Debug.Log("Defender touches trigger");
      if (other.CompareTag("EnemyBullet") && !invincible)
      {
        //play death animation
        anim.Play("defenderExplosion");
        moveSpeed = 0;
        _audio.clip = explosion;
        _audio.Play();
        grid.GetComponent<GridScript>().TriggerShake(2f);
        invincible = true;
        Instantiate(playerExplosionParticle, transform.position, Quaternion.identity);
      }
    }

    public void Die()
    {
      StartCoroutine(ShowCredits());
      this.GetComponent<SpriteRenderer>().enabled = false;
      this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void spawnFlames()
    {
      Debug.Log("spawning fire");
      Instantiate(playerFireParticle, transform.position + Vector3.down*.5f, Quaternion.identity);
      _audio.clip = failSound;
      _audio.Play();
    }
    

    IEnumerator ShowCredits()
    {
      yield return new WaitForSeconds(1.5f);
      AsyncOperation async = SceneManager.LoadSceneAsync("CreditsScene");
      while (!async.isDone)
      {
        yield return null;
      }
    }
}
