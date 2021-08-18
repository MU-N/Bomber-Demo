using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickControlles : MonoBehaviour
{
    [SerializeField] Joystick joystickInput;
    [SerializeField] private float playerSpeed;



    private float horizontal;
    private float vertical;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        horizontal = (joystickInput.Horizontal >= 0.2f) ? playerSpeed : (joystickInput.Horizontal <= -0.2f) ? -playerSpeed : 0f;

        vertical = (joystickInput.Vertical >= 0.2f) ? playerSpeed : (joystickInput.Vertical <= -0.2f) ? -playerSpeed : 0f;

        rb.velocity = new Vector3( horizontal , rb.velocity.y, vertical);

    }
}
