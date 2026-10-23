using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed, jumpForce;
    private Rigidbody2D rig;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.linearVelocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
            (transform.up * rig.linearVelocity.y);

        if (rig.linearVelocity.x < 0.1f)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (rig.linearVelocity.x < -0.1f)
            GetComponent<SpriteRenderer>().flipX = true;

        if (Input.GetButtonDown("Jump"))
        {
            rig.AddForce(transform.up * jumpForce);
        }
        anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
        anim.SetFloat("velocityY", rig.linearVelocity.y);
    }
}
