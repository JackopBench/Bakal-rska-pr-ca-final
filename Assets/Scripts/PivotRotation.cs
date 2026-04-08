using UnityEngine;
using UnityEngine.AI;

public class PivotRotation : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && agent.velocity.sqrMagnitude > 0.01f)
        {
            Vector2 direction = agent.velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}