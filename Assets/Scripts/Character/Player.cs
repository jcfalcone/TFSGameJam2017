using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] PlayerStateManager stateManager = new PlayerStateManager();

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform mesh;

    [SerializeField] [Range(50, 150)] float gravityForce;
    [SerializeField] [Range(100, 500)] float moveSpeed;
    [SerializeField] float turnRate;
    [SerializeField] [Range(0.1f, 1f)] float slowMotion;

    bool isMovable;
    bool isFacingRight;
    bool canFloat;

    int axisX;
    int axisY;

    [SerializeField] float staminaBurnRate;
    [SerializeField] float staminaRecoverRate;

    enum Animations : int
    {
        Idle,
        Walk,
        Dead
    }
    int currentAnimation;

    void Start ()
    {
        this.stateManager.Init(this);

        SetupRigidbody();

        transform.position = spawnPoint.position;

        if (this.gravityForce <= 0.0f) this.gravityForce = 100.0f;
        if (this.turnRate <= 0.0f) this.turnRate = 10.0f;

        this.stateManager.SetState(TemplateState.States.Default);
        
        this.isDead = false;
        this.isMovable = true;
        this.isFacingRight = true;
        this.canFloat = true;

        this.stamina = 100.0f;
	}
	
	void Update ()
    {
        if (this.isDead) return;
        
        CheckInput();

        this.stateManager.Tick();

        rb.velocity = ((transform.forward * this.axisX) + (transform.up * this.axisY)) * this.moveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.up * gravityForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void CheckInput()
    {
        this.axisX = 0;
        this.axisY = 0;

        
        if (this.isMovable)
        {
            if (Input.GetKey(KeyCode.W) && this.canFloat)
            {
                this.axisY = 1;
                this.currentAnimation = (int)Animations.Walk;
                SpendStamina();
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.axisX = 1;
                this.currentAnimation = (int)Animations.Walk;
                this.isFacingRight = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.axisX = -1;
                this.currentAnimation = (int)Animations.Walk;
                this.isFacingRight = false;
            }

            if(!Input.anyKey) this.currentAnimation = (int)Animations.Idle;
        }


        /* CHANGE STATES FOR PLAYER */
        
        if (Input.GetMouseButton(0))
        {
            this.stateManager.SetState(TemplateState.States.Intangible);
        }
        else if(Input.GetMouseButton(1))
        {
            this.stateManager.SetState(TemplateState.States.Solid);
        }
        else
        {
            this.stateManager.SetState(TemplateState.States.Default);
        }

        FlipMesh();

        if (anim)
            anim.SetInteger("State", currentAnimation);
    }

    public void SetIsMovable( bool condition ) { this.isMovable = condition; }

    public void SetupAnimator( Animator newAnim ) { this.anim = newAnim; }

    void FlipMesh()
    {
        if(!isFacingRight)
        {
            mesh.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            mesh.localRotation = Quaternion.identity;
        }
    }

    public void SpendStamina()
    {
        this.stamina -= this.staminaBurnRate;

        if(this.stamina <= 0.0f)
        {
            this.stamina = 0.0f;
            this.canFloat = false;
        }
    }

    public void RecoverStamina()
    {
        this.stamina += this.staminaRecoverRate;

        if(this.stamina >= 100.0f)
        {
            this.stamina = 100.0f;
            this.canFloat = true;
        }
    }

    public void SetMovePseed( float newMoveSpeed ) { this.moveSpeed = newMoveSpeed; }
    public void SetStaminaBurnRate(float newBurnRate) { this.staminaBurnRate = newBurnRate; }
    public void SetStaminaRecoverRate( float newRecoverRate ) { this.staminaRecoverRate = newRecoverRate; }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.name == "Projectile")
        {
            Time.timeScale = slowMotion;
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.name == "Projectile")
        {
            Time.timeScale = slowMotion;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.name == "Projectile")
        {
            Time.timeScale = 1.0f;
        }
    }
}