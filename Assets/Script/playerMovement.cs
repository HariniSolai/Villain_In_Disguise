using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 3.0f;
    public float rotationSpeed = 45;
    public float jumpForce = 700f;

    Rigidbody rb;
    Animator anim;

    float moveInput;
    float rotateInput;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        // Get keyboard input
        moveInput = Input.GetAxis("Vertical");     // W / S
        rotateInput = Input.GetAxis("Horizontal"); // A / D

        // Send movement value to animator
        if (anim != null) {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void FixedUpdate() {
        // Move forward/back
        Vector3 movement = transform.forward * moveInput * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Rotate left/right
        Quaternion turn = Quaternion.Euler(0f, rotateInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
}