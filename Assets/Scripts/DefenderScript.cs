using UnityEngine;

public class Defender : MonoBehaviour
{
  public GameObject invader;
  public GameObject bullet;
  public Transform shottingOffset;

  private void Start()
  {

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
        Destroy(shot, 4f);
      }
    }

    void AnimationFrameCallback()
    {
      Debug.Log("Somethign happened in the animation");
    }
}
