using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    private float speed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = moveSpeed;
        characterController.height = standingHeight;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            speed = crouchSpeed;
            characterController.height = crouchingHeight;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            speed = moveSpeed;
            characterController.height = standingHeight;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded && characterController.height == standingHeight)
        {
            moveDirection.y = jumpSpeed;
        }

        moveDirection.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection);
    }
}