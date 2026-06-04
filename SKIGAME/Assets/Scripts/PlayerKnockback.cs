using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float force = 10f;
    [SerializeField] private float disableTime = 0.7f;

    private PlayerController controller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += ApplyKnockback;
    }

    private void OnDisable()
    {
        Obstacle.OnPlayerHit -= ApplyKnockback;
    }

    private void ApplyKnockback()
    {
        controller.DisableMovement(disableTime);

        Vector3 dir = -transform.forward; 
        rb.AddForce(dir * force, ForceMode.Impulse);
    }
}