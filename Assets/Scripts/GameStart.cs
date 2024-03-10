using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject grid;
    public GameObject defender;
    public GameObject barricade;
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
            Instantiate(barricade, new Vector3(-5, -3, 0),Quaternion.identity);
            Instantiate(barricade, new Vector3(-2, -3, 0),Quaternion.identity);
            Instantiate(barricade, new Vector3(2, -3, 0),Quaternion.identity);
            Instantiate(barricade, new Vector3(5, -3, 0),Quaternion.identity);
            grid.SetActive(true);
            defender.SetActive(true);
            gameActive = true;
        }
    }
}
