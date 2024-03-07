using System;
using UnityEngine;

public class Defender : MonoBehaviour
{
  public GameObject invader;
  public GameObject bullet;
  public Transform shottingOffset;
  public float moveSpeed;
  public Animator anim;
  private AudioSource audio;
  public AudioClip explosion;

  private void Start()
  {
    anim = GetComponent<Animator>();
    anim.speed = 0;
    audio = GetComponent<AudioSource>();
  }

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        audio.Play();
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
      if (other.CompareTag("EnemyBullet"))
      {
        //play death animation
        anim.speed = 1;
        moveSpeed = 0;
        audio.clip = explosion;
        audio.Play();
      }
    }

    public void Die()
    {
      Destroy(this.gameObject);
    }
}
