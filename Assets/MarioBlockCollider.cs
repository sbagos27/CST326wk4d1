using System;
using UnityEngine;

public class MarioBlockCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick") || other.CompareTag("Question"))
        {
            GameManager.instance.HandleBlockHit(other); 
        }
    }
}
