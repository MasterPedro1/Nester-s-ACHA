using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    public GameObject VFX;

    private Vector3 _movement = Vector3.zero;
    [Header("Physic Values")]

   
    public float walkSpeed = 6f;
    public float crouchSpeed = 3f;
    public float runningSpeed = 10f;
    public float backRunSpeed = 8f;
    public float standRotationSpeed = 3;
    public float moveRotationSpeed = 3;

    public float currentSpeed;
    public float currentRotationSpeed;

    [Header("Gravity")]
    public float gravity = 20f;
    private Vector3 moveDirection;

    private void Start()
    {
        currentSpeed = walkSpeed;
        currentRotationSpeed = standRotationSpeed;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement(currentSpeed);
        Rotation(currentRotationSpeed);
    }
    public void Death()
    {
        _characterController.enabled = false;
    }

    public void Movement(float thisSpeed)
    {
        _movement = new Vector3(0, 0, Input.GetAxis("Vertical")); //actualiza conforme a los axis
        _movement = transform.TransformDirection(_movement) * thisSpeed;
        _movement.y -= gravity; //en el vector3 se calcula un movimiento en y constante, o sea la gravedad

        if (_characterController.isGrounded)
        {
            _movement.y = 0f;
        }

        _characterController.Move(_movement * Time.deltaTime);
    }

    void Rotation(float thisRotationSpeed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            transform.Rotate(Vector3.up * horizontal * thisRotationSpeed);
           
        }
    }

}

