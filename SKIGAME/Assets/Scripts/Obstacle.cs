using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action OnPlayerHit;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPlayerHit?.Invoke();
        }
        
    }
    
}