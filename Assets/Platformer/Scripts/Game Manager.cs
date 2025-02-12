using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI timeText;
    
    void Update()
    {
        int timeLeft = 300 - (int)(Time.time);
        timeText.text = $"Time: {timeLeft}";
    }
}
