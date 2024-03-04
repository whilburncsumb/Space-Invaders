using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int health = 4;
    public Sprite[] animationFrames;

    public void lowerHealth()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        health--;
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
        rend.sprite = animationFrames[health+1];
    }
}
