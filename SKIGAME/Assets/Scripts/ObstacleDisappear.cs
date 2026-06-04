using UnityEngine;

public class ObstacleDisappear : Obstacle
{
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);   
        Destroy(gameObject);                
    }
}