using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    float speed = 4;
    float rotationSpeed = 80;
    float gravity = 8;
    float rotation = 0;

    Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;

    Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetInteger("Condition_Walk", 1);
                moveDirection = new Vector3(0, 0, 1);
                moveDirection *= speed;
                moveDirection = transform.TransformDirection(moveDirection);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetInteger("Condition_Walk", 0);
                moveDirection = Vector3.zero;
            }
        }

        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
