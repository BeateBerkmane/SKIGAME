using System;
using Unity.Cinemachine;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource AudioSource;
    [SerializeField] private AudioClip obstacleHitSound;
    
    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayObstacleHitSound;
    }

    private void OnDisable()
    {
        Obstacle.OnPlayerHit -= PlayObstacleHitSound;
    }

    private void PlayObstacleHitSound()
    {
        AudioSource.PlayOneShot(obstacleHitSound);
    }
}
