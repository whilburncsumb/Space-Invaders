using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public int rowCount;// number of rows and columns of enemies
    public int columnCount;
    public float width;//size of the grid
    public float height;
    private Vector2 startPosition;
    public float startSpeed;//starting movement speed
    public float advanceSpeed;//current movement speed
    public bool goingLeft;
    public GameObject spaceInvader;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject barrier;
    
    public float initialInterval = 1f; // Initial interval between movements
    public float minInterval = 0.1f; // Minimum interval between movements

    private float currentInterval; // Current interval between movements
    private bool reverse = false;
    private int invadersLeft;
    private int score;
    private int highScore;

    
    // Start is called before the first frame update
    void Start()
    {        
        startPosition = transform.position;
        goingLeft = true;
        advanceSpeed = startSpeed;
        spawnEnemies();
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        setScore();
        StartCoroutine(TriggerMovements());
    }

    // Update is called once per frame
    void Update()
    {
        reverse = false;
    }


    private void spawnEnemies()
    {
        // Reset position
        transform.position = this.startPosition;
        // Calculate the starting position for the grid
        Vector2 startPosition = transform.position - new Vector3(width * (columnCount - 1) / 2f, height * (rowCount - 1) / 2f);

        // Iterate over the grid
        for (int col = 0; col < columnCount; col++)
        {
            for (int row = 0; row < rowCount; row++)
            {
                // Calculate the position for the current prefab
                Vector2 position = startPosition + new Vector2(col * width, -row * height);

                // Spawn the prefab at the calculated position
                var invader = Instantiate(spaceInvader, position, Quaternion.identity, transform);
                invader.GetComponent<SpaceInvader>().OnDeath += OnInvaderDeath;//subscribe to death of the invader
                if (row == 0)
                {
                    invader.GetComponent<SpaceInvader>().setType(0);
                }
                else if (row >= 1 && row <= 2)
                {
                    invader.GetComponent<SpaceInvader>().setType(1);
                }
                else
                {
                    invader.GetComponent<SpaceInvader>().setType(2);
                }
            }
        }
        invadersLeft = rowCount * columnCount;
    }
    
    void OnInvaderDeath(int pointValue)
    {
        // Handle space invader death event...
        invadersLeft--;
        advanceSpeed += 0.01f;
        currentInterval -= 0.01f;
        score += pointValue;
        //Set new high scores
        highScore = Math.Max(highScore, score);
        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            // Debug.Log("setting new high score of " +highScore);
            PlayerPrefs.SetInt("HighScore",highScore);
            PlayerPrefs.Save();
        }
        setScore();
    }

    private void setScore()
    {
        string scoreString = score.ToString("D5");
        string highscoreString = highScore.ToString("D5");
        string text = "SCORE: " + scoreString + "\tHIGH SCORE: " + highscoreString;
        scoreText.text = text;
    }
    public void HandleWallCollision()
    {
        if (reverse)
        {
         return;
        }
        reverse = true;
        goingLeft = !goingLeft;
        // Debug.Log("Moving down by " + advanceSpeed);
        transform.Translate(Vector3.down * advanceSpeed);
    }
    
    IEnumerator TriggerMovements()
    {
        // Initialize the current interval
        currentInterval = initialInterval;

        while (true)
        {
            // Perform movements here
            if (goingLeft)
            {
                // Debug.Log("Moving left by " + advanceSpeed);
                transform.Translate(Vector3.left * advanceSpeed);
            }
            else
            {
                // Debug.Log("Moving right by " + advanceSpeed);
                transform.Translate(Vector3.right * advanceSpeed);
            }
            // Clamp interval to minimum value
            currentInterval = Mathf.Max(currentInterval, minInterval);

            // Wait for the current interval
            yield return new WaitForSeconds(currentInterval);
        }
    }
    
}
