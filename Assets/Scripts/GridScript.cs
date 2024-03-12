using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GridScript : MonoBehaviour
{
    public int rowCount;// number of rows and columns of enemies
    public int columnCount;
    public float width;//size of the grid
    public float height;
    private Vector2 startPosition;
    public Vector3 saucerTransform;
    public float startSpeed;//starting movement speed
    public float advanceSpeed;//current movement speed
    public bool goingLeft;
    public GameObject mainCamera;
    private float shakeDuration;
    private float shakeIntensity;
    private float shakeStartTime;
    private Vector3 cameraHomePosition;
    
    public GameObject spaceInvader;
    public GameObject saucer;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject barrier;
    public AudioSource sounds;
    public AudioSource music;
    public AudioClip yay;
    public float pitch;
    
    public float initialInterval = 1f; // Initial interval between movements
    public float minInterval = 0.1f; // Minimum interval between movements

    private float currentInterval; // Current interval between movements
    private bool reverse = false;
    public int maxInvaders;
    public int invadersLeft;
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
        pitch = 1;
        cameraHomePosition = mainCamera.transform.position;
        shakeIntensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        reverse = false;
        Shake();
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
                SpaceInvader script = invader.GetComponent<SpaceInvader>();
                script.OnDeath += OnInvaderDeath;//subscribe to death of the invader
                script.demoMode = false;
                if (row == 0)
                {
                    script.setType(0);
                }
                else if (row >= 1 && row <= 2)
                {
                    script.setType(1);
                }
                else
                {
                    script.setType(2);
                }
            }
        }

        maxInvaders = rowCount * columnCount;
        invadersLeft = rowCount * columnCount;
    }
    
    void OnInvaderDeath(int pointValue)
    {
        // Handle space invader death event...
        sounds.Play();
        if (pointValue < 100)
        {
            invadersLeft--;
            advanceSpeed += 0.01f;
            currentInterval -= 0.01f;
        }
        TriggerShake(0.5f);
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
        if (invadersLeft <= 0)
        {
            //end the game
            music.Stop();
            music.pitch = 1;
            music.loop = false;
            music.clip = yay;
            music.Play();
            player.GetComponent<Defender>().invincible = true;//make sure player cant be hurt after winning
            StartCoroutine(ShowCredits());
            return;
        }
        //Speed up music
        float ratio = 1 - ((float)invadersLeft / (float)maxInvaders);
        // Debug.Log("Ratio: "+ratio);
        pitch = Mathf.Lerp(1f, 2f, ratio);
        music.pitch = pitch;
    }
    
    private void Shake()
    {
        float shakeTime = shakeDuration - (Time.time - shakeStartTime);
        if (shakeTime <= 0f)
        {
            mainCamera.transform.position = cameraHomePosition;
            return;
        }
        // Calculate shake intensity based on remaining shake time
        float currentIntensity = Mathf.Clamp01(shakeTime / shakeDuration);
        float shakeSpeed = 10;
        
        // Calculate shake offset using Perlin noise
        float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2f - 1f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2f - 1f;
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0f) * (shakeIntensity * currentIntensity);

        // Apply shake offset to camera position
        mainCamera.transform.position = cameraHomePosition + shakeOffset;
    }
    
    public void TriggerShake(float intensity)
    {
        shakeIntensity = intensity;
        shakeDuration = Mathf.Clamp(intensity / 2.5f,0f,1f);
        shakeStartTime = Time.time;
        Shake();
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
            //Chance to spawn a saucer
            int randomNumber = Random.Range(0, 1000);

            // Check if the random number is within the spawn chance range
            if (randomNumber < 100 || Input.GetKey(KeyCode.E))
            {
                // Spawn a saucer
                GameObject bonusInvader = Instantiate(saucer, saucerTransform, Quaternion.identity);
                bonusInvader.GetComponent<FlyingSaucer>().OnDeath += OnInvaderDeath;//subscribe to death of the invader
                Destroy(bonusInvader, 8f);
            }
            
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
    
    IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(5f);
        AsyncOperation async = SceneManager.LoadSceneAsync("CreditsScene");
        while (!async.isDone)
        {
            yield return null;
        }
    }

}
