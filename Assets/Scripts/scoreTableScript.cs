using UnityEngine;
using TMPro;

public class SpriteText : MonoBehaviour
{
    void Start()
    {
        string text = $"SCORE: 0000\tHIGH SCORE: 0000\n" +
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