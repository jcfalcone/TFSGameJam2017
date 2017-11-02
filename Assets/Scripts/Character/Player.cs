using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] Transform spawnPoint;

    [SerializeField] [Range(50, 150)] float gravityForce;
    [SerializeField] [Range(100, 500)] float upForce;
    [SerializeField] [Range(100, 500)] float moveSpeed;

    void Start ()
    {
        SetupRigidbody();
        SetupAnimator();

        transform.position = spawnPoint.position;

        if (this.upForce <= 0.0f) this.upForce = 10.0f;
        if (this.moveSpeed <= 0.0f) this.moveSpeed = 3.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        int axisX = 0;
        int axisY = 0;

		if (Input.GetKey(KeyCode.W))        axisY = 1;
        if (Input.GetKey(KeyCode.D))        axisX = 1;
        if (Input.GetKey(KeyCode.A))        axisX = -1;

        rb.velocity = ((transform.forward * axisX) + (transform.up * axisY)) * this.moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.up * gravityForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
