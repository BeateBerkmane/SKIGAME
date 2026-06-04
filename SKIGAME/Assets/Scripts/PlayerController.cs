using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;

    [SerializeField] private float rotationSpeed = 12f;   
    [SerializeField] private float moveSpeed = -5f;       
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabled = false;
    [SerializeField] private float disableTime = 0.7f;
    private float lastDisableTime;

    public static Transform playerPos;
    private Rigidbody rb;
    private Animator Anim;

    void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        playerPos = transform;
    }

    private void OnEnable()
    {
        move?.Enable();
        Obstacle.OnPlayerHit += TakeDamage;   
    }

    private void OnDisable()
    {
        move?.Disable();
        Obstacle.OnPlayerHit -= TakeDamage;   
    }
    
    void TakeDamage()
    {
        rb.AddForce(pushbackForce, ForceMode.Impulse); 
        disabled = true;
        lastDisableTime = Time.timeSinceLevelLoad;
        Debug.Log("I got Hit");
    }

    public void DisableMovement(float time)
    {
        disabled = true;
        lastDisableTime = Time.timeSinceLevelLoad;
    }
    
    void FixedUpdate()
    {

        isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.2f,
            Vector3.down,
            1.4f,
            groundLayers
        );
        
   

        Color col = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down, col);
        
        if (Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;

        if (isGrounded && !disabled)
        {
            Vector2 moveInput = move.ReadValue<Vector2>();

         
            transform.Rotate(0, moveInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
            
            float turnAngle = Mathf.Abs(180 - transform.localEulerAngles.y);
            float speedMult = Mathf.Cos(turnAngle * Mathf.Deg2Rad);

         
            rb.AddForce(transform.forward * moveSpeed * speedMult * Time.fixedDeltaTime);
        }

        
        Anim.SetBool("grounded", isGrounded);
        Anim.SetFloat("playerspeed", rb.velocity.magnitude); 
    }
}
