using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject grid;
    public GameObject defender;
    public bool gameActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameActive)
        {
            // Instantiate(grid, new Vector3(0, 6, 0),Quaternion.identity);
            // Instantiate(defender, new Vector3(0, -4, 0),Quaternion.identity);
            grid.SetActive(true);
            defender.SetActive(true);
            gameActive = true;
        }
    }
}
