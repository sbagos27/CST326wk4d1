using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    
    public Camera cam;
    public InputActionReference move;
    
    private Vector3 camDirection;
    public float speed = 10f;
    
    public AudioClip brickSound;
    public AudioClip coinSound;
    
    private int coins = 0;
    private int score = 0;

    public GameObject mario;
    public GameObject marioHead;
    public LevelFinish levelFinish;
    
    public static GameManager instance; 

    private void Awake()
    {
        instance = this;  
    }

    void Update()
    {
        camDirection = move.action.ReadValue<Vector3>();
        Vector3 newPos = cam.transform.position + camDirection * speed * Time.deltaTime;
        cam.transform.position = newPos;

        int timeLeft = 10 - (int)(Time.time);
        timeText.text = $"Time: {timeLeft}";

        if (timeLeft <= 0)
        {
            Debug.Log("YOU LOST!");
            timeText.text = $"Time: 0";
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            Ray cursor = cam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(cursor, out RaycastHit hit))
            {
                HandleBlockHit(hit.collider);
            }
        }
    }

    public void HandleBlockHit(Collider collider)
    {
        if (collider.CompareTag("Brick"))
        {
            score += 100;
            scoreText.text = $"Mario\n{score:D6}";
            PlaySound(brickSound);
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Question"))
        {
            score += 100;
            scoreText.text = $"Mario\n{score:D6}";
            PlaySound(coinSound);
            coins++;
            coinText.text = $"x{coins}";
        }
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource audioSrc = GetComponent<AudioSource>();
        if (audioSrc != null && clip != null)
        {
            audioSrc.clip = clip;
            audioSrc.Play();
        }
    }

    public void OnPassLevel(LevelFinish goal)
    {
        Debug.Log("YOU WINNN");
    }

    public void KillMario(WaterKiller waterKiller)
    {
        Debug.Log("YOU DIED!");
        Destroy(mario);
    }
}