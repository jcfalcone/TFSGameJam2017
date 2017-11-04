using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] PlayerStateManager stateManager = new PlayerStateManager();

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform mesh;
    [SerializeField] ParticleSystem aura;

    [SerializeField] [Range(50, 150)] float gravityForce;
    [SerializeField] [Range(100, 500)] float moveSpeed;
    [SerializeField] float turnRate;

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

    Animations currentAnimation;
    Renderer currentLanternRender;
    Material currentLanternMaterial;


    void Start ()
    {
        this.stateManager.Init(this);

        SetupRigidbody();

        transform.position = spawnPoint.position;

        if (this.gravityForce <= 0.0f) this.gravityForce = 100.0f;
        if (this.turnRate <= 0.0f) this.turnRate = 10.0f;

        this.stateManager.SetState(PlayerStateTemplate.States.Default);
        
        this.isDead = false;
        this.isMovable = true;
        this.isFacingRight = true;
        this.canFloat = true;

        this.stamina = 100.0f;
	}
	
	void Update ()
    {
        if (this.isDead)
        {
            this.stateManager.SetState(PlayerStateTemplate.States.Dead);

            this.stateManager.Tick();
            return;
        }
        
        CheckInput();

        this.stateManager.Tick();

        rb.velocity = ((transform.forward * this.axisX) + (transform.up * this.axisY)) * this.moveSpeed * Time.deltaTime;

        rb.AddForce(-transform.up * gravityForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    

    void LateUpdate()
    {
        float percAmount = this.stamina / 100f;
        this.currentLanternMaterial.SetFloat("_FillAmount", 0.2f * percAmount + 0.25f);
    }

    void CheckInput()
    {
        this.axisX = 0;
        this.axisY = 0;

        /* CHECK INPUTS */
        this.currentAnimation = (int)Animations.Idle;
        if (this.isMovable)
        {
            if (Input.GetKey(KeyCode.W) && this.canFloat)
            {
                this.axisY = 1;
                this.currentAnimation = Animations.Walk;
                SpendStamina();
            }

            if (Input.GetKey(KeyCode.D))
            {
                this.axisX = 1;
                this.currentAnimation = Animations.Walk;
                this.isFacingRight = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.axisX = -1;
                this.currentAnimation = Animations.Walk;
                this.isFacingRight = false;
            }
        }


        /* CHANGE STATES FOR PLAYER */

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            this.aura.Play();
        }

        if (Input.GetMouseButton(0))
        {
            this.stateManager.SetState(PlayerStateTemplate.States.Intangible);
        }
        else if(Input.GetMouseButton(1))
        {
            this.stateManager.SetState(PlayerStateTemplate.States.Solid);
        }
        else
        {
            this.stateManager.SetState(PlayerStateTemplate.States.Default);
        }

        FlipMesh();

        if (anim)
            anim.SetInteger("State", (int)currentAnimation);
    }

    public void SetIsMovable( bool condition ) { this.isMovable = condition; }

    public void SetupAnimator( Animator newAnim ) { this.anim = newAnim; }

    public void SetLanternRender( Renderer render)
    {
        this.currentLanternRender = render;

        if (render)
        {
            this.currentLanternMaterial = render.material;
        }
    }

    void FlipMesh()
    {
        if(!isFacingRight)
        {
            mesh.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            mesh.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            mesh.localRotation = Quaternion.identity;
            mesh.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public void SpendStamina()
    {
        this.stamina -= this.staminaBurnRate * Time.deltaTime;

        if(this.stamina <= 0.0f)
        {
            this.stamina = 0.0f;
            this.canFloat = false;
        }
    }

    public void RecoverStamina()
    {
        this.stamina += this.staminaRecoverRate * Time.deltaTime;

        if(this.stamina >= 100.0f)
        {
            this.stamina = 100.0f;
            this.canFloat = true;
        }
    }

    public void SetMovePseed( float newMoveSpeed ) { this.moveSpeed = newMoveSpeed; }
    public void SetStaminaBurnRate(float newBurnRate) { this.staminaBurnRate = newBurnRate; }
    public void SetStaminaRecoverRate( float newRecoverRate ) { this.staminaRecoverRate = newRecoverRate; }

    public PlayerStateTemplate.States GetState()
    {
        return this.stateManager.GetState();
    }
}