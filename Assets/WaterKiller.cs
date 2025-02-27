using UnityEngine;

public class WaterKiller : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GameManager.instance.KillMario(this);
        
    }
}
