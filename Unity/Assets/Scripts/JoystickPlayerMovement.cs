using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 0.4f;
    public float turnSmoothTime = 0.2f;
    public float turnSmoothVelocity;

    public Vector3 direction;
    public float horizontal, vertical;

    private GameObject joyStickOBJ;
    private FixedJoystick joyStick;

    public Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        joyStickOBJ = GameObject.FindGameObjectWithTag("JoyStick");
        joyStick = joyStickOBJ.GetComponent<FixedJoystick>();

        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        joyStick_CharacterMovement();
    }

    private void joyStick_CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");



        horizontal = joyStick.input.x;
        vertical = joyStick.input.y;


        // negative values inverts movement direction because of character orientation
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetInteger("Walk", 1);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movDir * speed * Time.deltaTime);
        }
        else
        {
            animator.SetInteger("Walk", 0);
        }

    }

    public void doAttack()
    {
        animator.SetTrigger("Attack");
        if (GameObject.FindGameObjectWithTag("SwordWeapon").GetComponent<CapsuleCollider>().enabled == false)
            GameObject.FindGameObjectWithTag("SwordWeapon").GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine("DisableSwordCollider");
    }

    IEnumerator DisableSwordCollider()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("SwordWeapon").GetComponent<CapsuleCollider>().enabled = false;
    }

}
