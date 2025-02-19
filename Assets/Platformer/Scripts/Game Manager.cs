using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinText;
    public Camera cam;
    int coins = 0;
    public InputActionReference move;
    private Vector3 camDirection;
    public float speed = 10f;
    public AudioClip brickSound;
    public AudioClip coinSound;

    void Update()
    {
        camDirection = move.action.ReadValue<Vector3>();
        
        Vector3 newPos = cam.transform.position + camDirection * speed * Time.deltaTime;
        cam.transform.position = newPos;
        
        int timeLeft = 300 - (int)(Time.time);
        timeText.text = $"Time: {timeLeft}";

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            
            Ray cursor = cam.ScreenPointToRay(screenPos);
            bool rayHit = Physics.Raycast(cursor, out RaycastHit hit);
            if (rayHit && hit.collider.CompareTag("Brick"))
            {
                AudioSource audioSrc = GetComponent<AudioSource>();
                audioSrc.clip = brickSound;
                audioSrc.Play();
                Destroy(hit.collider.gameObject);
            }

            if (rayHit && hit.collider.CompareTag("Question"))
            {
                AudioSource audioSrc = GetComponent<AudioSource>();
                audioSrc.clip = coinSound;
                audioSrc.Play();
                coins++;
                coinText.text = $"x{coins}";
            }
        }
    }
    
    
}
