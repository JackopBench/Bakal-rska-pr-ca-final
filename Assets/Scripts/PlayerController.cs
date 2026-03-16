using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDrain = 30f;
    public float staminaRegen = 15f;
    public float minStaminaToSprint = 10f;
    public Image staminaFill;

    private float stamina;
    private bool canSprint = true;

    private Rigidbody2D rb;
    private Animator animator; // 👈 PRIDANÉ
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // 👈 PRIDANÉ

        stamina = maxStamina;
        staminaFill.fillAmount = 1f;
    }

    void Update()
{
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
    movement = movement.normalized;

    animator.SetFloat("xVelocity", movement.magnitude);

    // OTOČENIE POSTAVY
    if (movement.x > 0)
        spriteRenderer.flipX = false;
    else if (movement.x < 0)
        spriteRenderer.flipX = true;
}

    void FixedUpdate()
    {
        if (stamina <= 0f)
            canSprint = false;

        if (stamina >= minStaminaToSprint)
            canSprint = true;

        bool sprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;
        float currentSpeed = sprinting ? speed * sprintMultiplier : speed;

        rb.linearVelocity = movement * currentSpeed;

        if (sprinting && movement != Vector2.zero)
            stamina -= staminaDrain * Time.fixedDeltaTime;
        else
            stamina += staminaRegen * Time.fixedDeltaTime;

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaFill.fillAmount = stamina / maxStamina;
    }
}