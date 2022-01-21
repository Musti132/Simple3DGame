using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerJumpBehaviour : MonoBehaviour
{
    public KeyCode jumpKey;

    public Rigidbody rb;

    private bool isPlayerGrounded;

    Animation animations;

    private bool canJump = true;
    public float jumpThrust = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animations = GetComponent<Animation>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpKey)
            && isPlayerGrounded)
        {
            canJump = true;
        }
    }

    void FixedUpdate()
    {
        if (canJump)
        {
            canJump = false;
            animations.Play("JumpAir");
            rb.AddForce(transform.up * jumpThrust, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "GroundObject")
        {
            isPlayerGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "GroundObject")
        {
            isPlayerGrounded = false;
        }
    }
}
