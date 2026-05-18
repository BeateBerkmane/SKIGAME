using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = 20;
    private Rigidbody rb;
    public static Transform playerPos;
    private Animator Anim;


    void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        playerPos = transform;


    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position,
            transform.position - transform.up, Color.red);
        isGrounded = Physics.Linecast(Transform.position,
            transform.position - transform.up, groundLayers);
        if (Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;
        if (isGrounded && !disabled)
        {
            Vector2 moveVector = move.ReadValue<Vector2>();
            float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
            float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
            rb.AddForce(transform.forward * (moveSpeed * speedMultiplier * Time.fixedDeltaTime));
            //Debug.Log("move x: " + moveVector.x + "move y:" + moveVector.y);
            transform.Rotate(0, moveVector.x * rotationSpeed * Time.fixedDeltaTime, 0);
        }

        Anim.SetBool("grounded", isGrounded);
        Anim.SetFloat("playerspeed", rb.linearVelocity.magnitude);
        ///Debug.Log("slope angle:" + slopeAngle + "sin:" + Mathf.Sin(slopeAngle) + ", cos:" + Mathf.Cos(slopeAngle));
        Debug.Log("Speed:" + rb.linearVelocity.magnitude);
    }
}
