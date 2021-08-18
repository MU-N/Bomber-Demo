using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControlles : Charcter
{
    [SerializeField] Joystick joystickInput;
    [SerializeField] PlayerData playerData;



    private float horizontal;
    private float vertical;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasTheBomb = true;
    }


    void Update()
    {
        horizontal = (joystickInput.Horizontal >= 0.2f) ? playerData.MoveSpeed : (joystickInput.Horizontal <= -0.2f) ? -playerData.MoveSpeed : 0f;

        vertical = (joystickInput.Vertical >= 0.2f) ? playerData.MoveSpeed : (joystickInput.Vertical <= -0.2f) ? -playerData.MoveSpeed : 0f;

        rb.velocity = new Vector3( horizontal , rb.velocity.y, vertical);

    }
    private void OnBecameInvisible()
    {
        Debug.Log("Ins");
    }

    public override void Move()
    {
        
    }
}
