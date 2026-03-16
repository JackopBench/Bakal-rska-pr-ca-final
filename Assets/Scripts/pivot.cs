using UnityEngine;
using UnityEngine.AI;

public class pivot : MonoBehaviour
{
    public Transform player;
    public NPCController npcController;

    private NavMeshAgent agent;
    private Transform enemyTransform;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        enemyTransform = transform.parent; // Enemy objekt
    }

    void Update()
    {
        // ROTÁCIA PIVOTU
        if (npcController.IsChasing())
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
        else
        {
            if (agent != null && agent.velocity.sqrMagnitude > 0.01f)
            {
                Vector2 direction = agent.velocity.normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f);
            }
        }

}
}