using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float sprintMultiplier = 10f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDrain = 30f;
    public float staminaRegen = 15f;
    public float minStaminaToSprint = 10f;
    public Image staminaFill;

    private float stamina;
    private bool canSprint = true;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    public DDAManager ddaManager;
    public Light2D visionLight;
    public float visionSmoothSpeed = 3f;

    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stamina = maxStamina;
        staminaFill.fillAmount = 1f;
    }

    void Update()
{
    UpdateVisionLight();
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
    movement = movement.normalized;

    animator.SetFloat("xVelocity", movement.magnitude);

    
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

    void UpdateVisionLight()
    {
        if (ddaManager == null || visionLight == null) return;

        int difficulty = ddaManager.GetDifficultyLevel();

        float t = (difficulty - 1) / 9f;
        float invertedT = 1f - t;

        float targetIntensity = Mathf.Lerp(1f, 2f, invertedT);
        float targetInner = Mathf.Lerp(2f, 3f, invertedT);
        float targetOuter = Mathf.Lerp(4.5f, 6f, invertedT);

        visionLight.intensity = Mathf.Lerp(visionLight.intensity, targetIntensity, Time.deltaTime * visionSmoothSpeed);
        visionLight.pointLightInnerRadius = Mathf.Lerp(visionLight.pointLightInnerRadius, targetInner, Time.deltaTime * visionSmoothSpeed);
        visionLight.pointLightOuterRadius = Mathf.Lerp(visionLight.pointLightOuterRadius, targetOuter, Time.deltaTime * visionSmoothSpeed);
    }
}