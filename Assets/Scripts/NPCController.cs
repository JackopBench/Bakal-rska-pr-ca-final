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

    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
        }
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
{
    animator.SetBool("isChasing", isChasing);

    if (isChasing)
    {
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

    // 🔥 Rotácia sa vykoná vždy
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

    public bool IsChasing()
    {
        return isChasing;
    }
}