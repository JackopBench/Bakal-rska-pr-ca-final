using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public float waitTime = 1f;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private float waitCounter;
    private bool waiting;

    [Header("Player")]
    public Transform player;
    private PlayerController playerController;

    [Header("States")]
    private bool isChasing = false;

    
    public float hearingRadius = 10f;
    public float investigateWaitTime = 2f;

    private bool isInvestigating = false;
    private bool isWaitingAtPoint = false;

    private Vector3 investigatePosition;
    private float investigateTimer;

    [Header("DDA")]
    public DDAManager ddaManager;
    public float baseSpeed = 2f;
    public float maxSpeed = 6f;

    public float minHearingRadius = 5f;
    public float maxHearingRadius = 12f;

    private Animator animator;

    void Start()
    {
        ddaManager = FindFirstObjectByType<DDAManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            playerController = player.GetComponent<PlayerController>();
        }

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        UpdateSpeed();

        animator.SetBool("isChasing", isChasing);
        animator.SetBool("isInvestigating", isInvestigating);

        
        if (!isChasing && !isInvestigating && playerController != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= hearingRadius && playerController.IsSprinting)
            {
                investigatePosition = player.position;
                isInvestigating = true;
                agent.SetDestination(investigatePosition);
            }
        }

        
        if (isChasing)
        {
            agent.speed = baseSpeed * 1.3f;
            agent.SetDestination(player.position);
        }
        else if (isInvestigating)
        {
            HandleInvestigate();
        }
        else
        {
            Patrol();
        }

        
        Vector3 currentRotation = transform.eulerAngles;

        if (agent.velocity.x > 0.01f)
        {
            transform.rotation = Quaternion.Euler(currentRotation.x, 0f, currentRotation.z);
        }
        else if (agent.velocity.x < -0.01f)
        {
            transform.rotation = Quaternion.Euler(currentRotation.x, -180f, currentRotation.z);
        }
    }

    
    void HandleInvestigate()
    {
        if (!isWaitingAtPoint)
        {
            agent.SetDestination(investigatePosition);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isWaitingAtPoint = true;
                investigateTimer = investigateWaitTime;
            }
        }
        else
        {
            investigateTimer -= Time.deltaTime;

            if (investigateTimer <= 0f)
            {
                isInvestigating = false;
                isWaitingAtPoint = false;

                if (waypoints.Length > 0)
                {
                    agent.SetDestination(waypoints[currentIndex].position);
                }
            }
        }
    }

   
    void Patrol()
    {
        if (waypoints.Length > 0)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!waiting)
                {
                    waiting = true;
                    waitCounter = waitTime;
                }
                else
                {
                    waitCounter -= Time.deltaTime;

                    if (waitCounter <= 0f)
                    {
                        GoToNextPoint();
                    }
                }
            }
        }
    }

    void GoToNextPoint()
    {
        currentIndex = (currentIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentIndex].position);
        waiting = false;
    }

    
    public void StartChase()
    {
        if (isChasing) return;

        isChasing = true;
        isInvestigating = false;
        MusicManager.instance.StartChase();
    }

    public void StopChase()
    {
        if (!isChasing) return;

        isChasing = false;
        MusicManager.instance.StopChase();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }

    
    void UpdateSpeed()
    {
        if (ddaManager == null) return;

        int difficulty = ddaManager.GetDifficultyLevel();

        switch (difficulty)
        {
            case 1: baseSpeed = 2f; break;
            case 2: baseSpeed = 2.1f; break;
            case 3: baseSpeed = 2.2f; break;
            case 4: baseSpeed = 2.3f; break;
            case 5: baseSpeed = 2.4f; break;
            case 6: baseSpeed = 2.5f; break;
            case 7: baseSpeed = 2.6f; break;
            case 8: baseSpeed = 2.7f; break;
            case 9: baseSpeed = 2.8f; break;
            case 10: baseSpeed = 2.9f; break;
            default: baseSpeed = 2f; break;
        }

        if (!isChasing)
        {
            agent.speed = baseSpeed;
        }
        float t = (difficulty - 1) / 9f;
        hearingRadius = Mathf.Lerp(minHearingRadius, maxHearingRadius, t);
    }

    public bool IsChasing()
    {
        return isChasing;
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(investigatePosition, 0.2f);
    }
}