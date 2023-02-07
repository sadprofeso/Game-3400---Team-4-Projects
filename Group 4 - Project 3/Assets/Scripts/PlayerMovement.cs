using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 12f;
    public float crouchSpeed = 8f;
    Vector3 velocity;
    private float standingHeight = 1.85f;
    private float crouchingHeight = 1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float speed;

    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = moveSpeed;
        controller.height = standingHeight;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            speed = crouchSpeed;
            controller.height = crouchingHeight;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            speed = moveSpeed;
            controller.height = standingHeight;
        }
        
    }
}
