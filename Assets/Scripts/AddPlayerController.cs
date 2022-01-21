using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerController : MonoBehaviour
{

    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    private Vector3 velocity;

    public float stamina = 100;

    private Rigidbody rb;

    private Animation animations;

    private float idleTimer;
    private float smoothMoveVelocity;
    private float smoothInputMagnitude;
    public float smoothMoveTime = .1f;
    public float moveSpeed;

    private float angle;
    int staminaRegenarateTimer = 2;

    private float horizontalInput;
    private float verticalInput;

    public int staminaConsumeMultipler = 2;
    KeyCode sprintKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animations = GetComponent<Animation>();
        animations.Play("Idle01");
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = transform.right * horizontalInput + transform.forward * verticalInput;

        float inputMagnitude = inputDir.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;

        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * 4f * inputMagnitude);

        calculateSpeed();

        if (idleTimer >= staminaRegenarateTimer)
        {
            regenarateStamina();
        }

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.S))
        {
            rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        }

        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    void calculateSpeed()
    {
        if (!Input.GetKey(sprintKey) && horizontalInput != 0 || verticalInput != 0)
        {
            if (Input.GetKey(sprintKey) && stamina > 0) // Player running
            {
                idleTimerStop();

                if (!animations.IsPlaying("BattleRunForward"))
                {
                    animations.Play("BattleRunForward");
                }

                moveSpeed = runSpeed;

                consumeStamina();
            }
            else // Player walking
            {
                idleTimerStart();

                if (!animations.IsPlaying("BattleWalkForward"))
                {
                    animations.Play("BattleWalkForward");
                }

                moveSpeed = walkSpeed;
            }
        }
        else // Player idle
        {
            idleTimerStart();

            animations.Play("Idle01");
        }
    }

    void idleTimerStart()
    {
        idleTimer += Time.deltaTime;
    }
    void idleTimerStop()
    {
        idleTimer = 0;
    }

    void consumeStamina()
    {
        if (stamina > 0)
        {
            stamina -= Time.deltaTime * 5 * staminaConsumeMultipler;
            stamina = Mathf.Clamp(stamina, 0, 100);
        }
    }

    void regenarateStamina()
    {
        if (stamina < 100)
        {
            stamina += Time.deltaTime * 5 * staminaConsumeMultipler;
            stamina = Mathf.Clamp(stamina, 0, 100);
        }
    }
}
