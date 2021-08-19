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
    Transform camerObject, firstCamPos;
    Vector3 moveDirection, targetDirection;
    Quaternion targetRotaion, playerRotaion;

    public override bool hasTheBomb { get; set; }
    public override bool isWalking { get; set; }
    public override bool isDead { get; set; }
    public override bool isWin { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camerObject = Camera.main.transform;
        hasTheBomb = true;
        canSwitch = hasTheBomb;
        
    }


    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }


    private void HandleInput()
    {
        horizontal = (joystickInput.Horizontal >= 0.2f) ? playerData.moveSpeed : (joystickInput.Horizontal <= -0.2f) ? -playerData.moveSpeed : 0f;
        vertical = (joystickInput.Vertical >= 0.2f) ? playerData.moveSpeed : (joystickInput.Vertical <= -0.2f) ? -playerData.moveSpeed : 0f;
    }


    private void HandleMovement()
    {
        moveDirection = Vector3.zero;
        moveDirection = camerObject.forward * vertical;
        moveDirection = moveDirection + camerObject.right * horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= playerData.moveSpeed;

        rb.velocity = moveDirection;

    }

    private void HandleRotation()
    {
        targetDirection = Vector3.zero;
        targetDirection = camerObject.forward * vertical;
        targetDirection = targetDirection + camerObject.right * horizontal;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        targetRotaion = Quaternion.LookRotation(targetDirection);
        playerRotaion = Quaternion.Slerp(transform.rotation, targetRotaion, playerData.rotaionSpeed * Time.deltaTime);

        transform.rotation = playerRotaion;

    }



    public override void Move()
    {

    }
}
