using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] [Range(50, 150)] float gravityForce;
    [SerializeField] [Range(100, 500)] float upForce;
    [SerializeField] [Range(100, 500)] float moveSpeed;

    bool isMovable;
    bool isFacingRight;

    int axisX;
    int axisY;

    enum Animations : int
    {
        Idle,
        Walk,
        Rigid
    }
    int currentAnimation;

    void Start ()
    {
        SetupRigidbody();

        transform.position = spawnPoint.position;

        if (this.gravityForce <= 0.0f) this.gravityForce = 100.0f;
        if (this.upForce <= 0.0f) this.upForce = 250.0f;
        if (this.moveSpeed <= 0.0f) this.moveSpeed = 250.0f;

        this.isDead = false;
        this.isMovable = true;
        this.isFacingRight = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMovable || isDead) return;
        
        CheckInput();

        rb.velocity = ((transform.forward * axisX) + (transform.up * axisY)) * this.moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.up * gravityForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void CheckInput()
    {
        axisX = 0;
        axisY = 0;

        currentAnimation = (int)Animations.Idle;

        if (Input.GetKey(KeyCode.W))
        {
            axisY = 1;
            currentAnimation = (int)Animations.Walk;
        }

        if (Input.GetKey(KeyCode.D))
        {
            axisX = 1;
            currentAnimation = (int)Animations.Walk;
            isFacingRight = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            axisX = -1;
            currentAnimation = (int)Animations.Walk;
            isFacingRight = false;
        }
    }

    public void SetIsMovable( bool condition ) { this.isMovable = condition; }

    public void SetupAnimator( Animator newAnim ) { this.anim = newAnim; }
}
