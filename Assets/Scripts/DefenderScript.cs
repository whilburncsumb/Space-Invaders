using UnityEngine;

public class Defender : MonoBehaviour
{
  public GameObject invader;
  public GameObject bullet;
  public Transform shottingOffset;
  public float moveSpeed;

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
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

}
