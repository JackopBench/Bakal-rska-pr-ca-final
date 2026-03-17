using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 1f;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private float waitCounter;
    private bool waiting;
    public Transform player;
    private bool isChasing = false;
    public DDAManager ddaManager;
    public float baseSpeed = 2f;
    public float maxSpeed = 6f;

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

    if (isChasing)
    {
        agent.speed *= 1.3f;
        agent.SetDestination(player.position);
    }
    else
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

    void GoToNextPoint()
    {
        currentIndex = (currentIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentIndex].position);
        waiting = false;
    }

    public void StartChase()
    {
        isChasing = true;
    }

    public void StopChase()
    {
        isChasing = false;

    
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
            case 1: agent.speed = 2f; break;
            case 2: agent.speed = 2.2f; break;
            case 3: agent.speed = 2.4f; break;
            case 4: agent.speed = 2.6f; break;
            case 5: agent.speed = 2.8f; break;
            case 6: agent.speed = 3f; break;
            case 7: agent.speed = 3.2f; break;
            case 8: agent.speed = 3.4f; break;
            case 9: agent.speed = 3.6f; break;
            case 10: agent.speed = 4f; break;

            default:
                agent.speed = 2f;
                break;
        }
}

    public bool IsChasing()
    {
        return isChasing;
    }
}