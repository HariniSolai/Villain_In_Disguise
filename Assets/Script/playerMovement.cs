using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 45;
    public float jumpForce = 700f;

    public float maxHeight = 125f;
    public float groundCheckDistance = 1.2f;

    public AudioSource runAudio;
    public AudioSource jumpAudio;
    public AudioSource flyAudio;
    public AudioSource attackAudio;

    public Transform forestSpawn;
    public Transform villageSpawn;
    public Transform caveSpawn;

    Rigidbody rb;
    Animator anim;

    float moveInput;
    float rotateInput;

    int jumpCount = 0;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Horizontal");

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        HandleRunSound();
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (anim != null)
            {
                anim.SetTrigger("Attack");
                ScoreManager.instance.reduceDragonhealth(0);
            }

            if (attackAudio != null && attackAudio.clip != null)
            {
                attackAudio.PlayOneShot(attackAudio.clip);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TeleportTo(forestSpawn);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            TeleportTo(villageSpawn);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TeleportTo(caveSpawn);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveInput * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        Quaternion turn = Quaternion.Euler(0f, rotateInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);

        if (transform.position.y >= maxHeight && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }

    void TryJump()
    {
        if (transform.position.y >= maxHeight)
        {
            return;
        }

        rb.AddForce(Vector3.up * jumpForce);

        if (jumpCount < 2)
        {
            if (jumpAudio != null)
            {
                jumpAudio.Play();
            }
        }
        else
        {
            if (flyAudio != null)
            {
                flyAudio.Play();
            }
        }

        jumpCount++;
    }

    void CheckGrounded()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }
    }

    void HandleRunSound()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0.1f;

        if (isMoving && isGrounded)
        {
            if (runAudio != null && !runAudio.isPlaying)
            {
                runAudio.Play();
            }
        }
        else
        {
            if (runAudio != null && runAudio.isPlaying)
            {
                runAudio.Stop();
            }
        }
    }

    void TeleportTo(Transform destination)
    {
        if (destination == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = destination.position;
        transform.rotation = destination.rotation;

        jumpCount = 0;
    }
}