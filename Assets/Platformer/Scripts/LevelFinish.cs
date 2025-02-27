using System;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GameManager.instance.OnPassLevel(this);
        
    }
}
