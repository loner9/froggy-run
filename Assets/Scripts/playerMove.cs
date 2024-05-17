using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float jumpingPower = 6f;

    private PlayerAction actions;
    private Animator animator;
    private AudioSource audioSource;
    private bool _isGrounded;
    // Start is called before the first frame update

    private void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        actions = new PlayerAction();

        actions.Player.Jump.performed += ctx => Jump(ctx);
        actions.Player.Jump.canceled += ctx => Jump(ctx);
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    private bool isGrounded()
    {
        bool onGround = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        // Debug.Log("is on ground"+onGround);
        return onGround;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            // animator.SetBool("isJumping", isGrounded());
            audioSource.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        scoreDisplay.Instance.IncreaseScore(10 * Time.deltaTime);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.name.Contains("Tilemap"))
        {
            PauseMenu.Instance.GameOver();
        }
    }

    private void animEnd()
    {
        Debug.Log("player_idle reached the last frame");
    }
}
