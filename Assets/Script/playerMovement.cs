using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 45;
    public float jumpForce = 700f;

    public AudioSource runAudio;
    public AudioSource jumpAudio;

    public Transform forestSpawn;
    public Transform villageSpawn;
    public Transform caveSpawn;

    Rigidbody rb;
    Animator anim;

    float moveInput;
    float rotateInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get keyboard input
        moveInput = Input.GetAxis("Vertical");     // W / S
        rotateInput = Input.GetAxis("Horizontal"); // A / D

        // Send movement value to animator
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        // Play movement/running sound
        HandleRunSound();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);

            if (jumpAudio != null)
            {
                jumpAudio.Play();
            }
        }

        // Attack (UNCHANGED - still F)
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (anim != null)
            {
                anim.SetTrigger("Attack");
            }
        }

        // Teleport controls
        if (Input.GetKeyDown(KeyCode.B)) // Forest
        {
            TeleportTo(forestSpawn);
        }

        if (Input.GetKeyDown(KeyCode.V)) // Village
        {
            TeleportTo(villageSpawn);
        }

        if (Input.GetKeyDown(KeyCode.C)) // Cave
        {
            TeleportTo(caveSpawn);
        }
    }

    void FixedUpdate()
    {
        // Move forward/back
        Vector3 movement = transform.forward * moveInput * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Rotate left/right
        Quaternion turn = Quaternion.Euler(0f, rotateInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

    void HandleRunSound()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0.1f;

        if (isMoving)
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
    }
}