using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        goingLeft = true;
        advanceSpeed = startSpeed;
        spawnEnemies();
        scoreText.enabled = false;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemies()
    {
        // Reset position
        transform.position = this.startPosition;
        // Instantiate(rockPrefab, newPos,Quaternion.identity,environmentRoot);
        // Calculate the starting position for the grid
        Vector2 startPosition = transform.position - new Vector3(width * (columnCount - 1) / 2f, height * (rowCount - 1) / 2f);

        // Iterate over the grid
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                // Calculate the position for the current prefab
                Vector2 position = startPosition + new Vector2(col * width, row * height);

                // Spawn the prefab at the calculated position
                var invader = Instantiate(spaceInvader, position, Quaternion.identity, transform);
                // var.invaderType=?
            }
        }
    }
}
