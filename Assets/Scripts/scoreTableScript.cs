using UnityEngine;
using TMPro;

public class SpriteText : MonoBehaviour
{
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        string scoreString = "00000";
        string highscoreString = highScore.ToString("D5");
        string text = $"SCORE: " + scoreString + "\tHIGH SCORE: " + highscoreString +
                      $"\n" +
                      $"*SCORE ADVANCE TABLE*\n" +
                      $"<sprite=0> =? MYSTERY\n" +
                      $"<sprite=1>=30 POINTS\n" +
                      $"<sprite=2>=20 POINTS\n" +
                      $"<sprite=3>=10 POINTS\n\n" + 
                      $"PRESS SPACE TO BEGIN"; 

        GetComponent<TextMeshProUGUI>().text = text;
    }
}