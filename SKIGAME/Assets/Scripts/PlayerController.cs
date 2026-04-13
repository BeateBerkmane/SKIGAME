using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = 20;
    private Rigidbody rb;
    
    

    void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
        
    }
    
    void FixedUpdate()
    {
        Vector2 moveVector = move.ReadValue<Vector2>(); 
        float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
        float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        rb.AddForce(transform.forward * (moveSpeed * speedMultiplier * Time.fixedDeltaTime));
        //Debug.Log("move x: " + moveVector.x + "move y:" + moveVector.y);
        transform.Rotate(0, moveVector.x *  rotationSpeed * Time.fixedDeltaTime, 0);
       
        Debug.Log("slope angle:" + slopeAngle + "sin:" + Mathf.Sin(slopeAngle) + ", cos:" + Mathf.Cos(slopeAngle));
    }
}
