using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControlles : MonoBehaviour
{
    [SerializeField] Joystick joystickInput;
    [SerializeField] GameData gameData;
    [SerializeField] PlayerData playerData;



    private float horizontal;
    private float vertical;

    Rigidbody rb;
    Animator anim;

    Transform camerObject, firstCamPos;
    Vector3 moveDirection, targetDirection;
    Quaternion targetRotaion, playerRotaion;

    private bool hasTheBomb;
    private bool isWalking;
    private bool isDead;
    private bool isWin;

    WaitForSeconds waitForSeconds = new WaitForSeconds(3f);


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camerObject = Camera.main.transform;
        
        anim = GetComponentInChildren<Animator>();
        
    }


    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
        UpdateAnimation();
    }


    private  void HandleInput()
    {
        horizontal = (joystickInput.Horizontal >= 0.2f) ? playerData.moveSpeed : (joystickInput.Horizontal <= -0.2f) ? -playerData.moveSpeed : 0f;
        vertical = (joystickInput.Vertical >= 0.2f) ? playerData.moveSpeed : (joystickInput.Vertical <= -0.2f) ? -playerData.moveSpeed : 0f;
        isWalking = vertical != 0 || horizontal != 0 ;
    }


    private  void HandleMovement()
    {
        moveDirection = Vector3.zero;
        moveDirection = camerObject.forward * vertical;
        moveDirection = moveDirection + camerObject.right * horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= playerData.moveSpeed;

        rb.velocity = moveDirection;

    }

    private  void HandleRotation()
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



    private  void UpdateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("hasBomb", hasTheBomb);
        anim.SetBool("isWin", isWin);
        anim.SetBool("isDead", isDead);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            if (gameData.canSwitchTheBomb && GetComponentInChildren<BombController>() != null)
            {
                MoveBombToOther(collision);
                StartCoroutine(WaitSomeSec());
                gameData.canSwitchTheBomb = false;

            }
        }

    }

    private void MoveBombToOther(Collision collision)
    {
        GameObject bomb = GetComponentInChildren<BombController>().gameObject;
        bomb.transform.parent = collision.gameObject.transform;
        bomb.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z + 0.5f);
    }

    IEnumerator WaitSomeSec()
    {
        yield return waitForSeconds;
        gameData.canSwitchTheBomb = true;
    }
}
