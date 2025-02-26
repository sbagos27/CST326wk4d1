using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;

    [Header("Debug")] 
        public bool isGrounded;
    
    Animator animator;
    Rigidbody rb;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("In Air", !isGrounded);
    }

    void Update()
    {
        UpdateAnimation();
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);
        
        float horizontalSpeed = rb.linearVelocity.x;
        float verticalSpeed = rb.linearVelocity.y;

        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);
        verticalSpeed = Mathf.Clamp(verticalSpeed, -maxSpeed*2, maxSpeed*2);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        newVelocity.y = verticalSpeed;
        rb.linearVelocity = newVelocity;
            
        Collider collider = GetComponent<Collider>();
        Vector3 startPoint = transform.position;
        float castDistance = collider.bounds.extents.y + 0.01f;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);
        
        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);
        
        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            if (isGrounded) {
                //apply impulse force upward
                rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
            }
        } else if (Input.GetKey(KeyCode.Space) && !isGrounded) {

            if (rb.linearVelocity.y > 0f)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);

            }
        }
        if (horizontalAmount == 0f)
        {
            Vector3 decayVelocity = rb.linearVelocity;
            decayVelocity.x *= 1f - Time.deltaTime * 5f;
            rb.linearVelocity = decayVelocity;
        }
        else
        {
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }

    }
}
