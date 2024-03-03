using UnityEngine;

public class SpaceInvader : MonoBehaviour
{
    public int invaderType = 1;
    public int points;
    // Declare delegate for the death event
    public delegate void DeathEventHandler();
    // Declare event for the death event
    public event DeathEventHandler OnDeath;

    // Method to call when the space invader dies
    public void Die()
    {
        // Raise the death event
        OnDeath?.Invoke();
        // Perform other death-related actions...
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerBullet"))
        {
                  Destroy(other.gameObject);
                  GetComponent<Animator>().SetTrigger("killTrigger");
        }
    }
    
    public void setType(int input)
    {
        invaderType = input;
        Animator anim = GetComponent<Animator>();
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
        Debug.Log("enemy explosion done");
        Destroy(this.gameObject);
    }
    
    void OnDestroy()
    {
        // Call the Die method when the space invader is destroyed
        Die();
    }
}
