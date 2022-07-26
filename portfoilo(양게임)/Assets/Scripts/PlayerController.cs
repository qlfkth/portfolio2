using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 100f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigibody;
    private Animator animator;
    private AudioSource playerAudio;


    private void Start()
    {
        playerRigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        animator.SetBool("Buttensriding", false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && jumpForce < 2)
        {
            jumpCount++;
            playerRigibody.velocity = Vector2.zero;
            playerRigibody.AddForce(new Vector2(0, jumpCount));
            playerAudio.Play(); 
        }
        else if(Input.GetMouseButtonUp(0)&& playerRigibody.velocity.y > 0)
        {
            playerRigibody.velocity = playerRigibody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

        if (Input.GetMouseButtonDown(2))
        {
            animator.SetBool("buttensriding", true);
        }
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigibody.velocity = Vector2.zero;
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
