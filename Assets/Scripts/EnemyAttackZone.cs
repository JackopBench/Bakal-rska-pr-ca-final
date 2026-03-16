using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackZone : MonoBehaviour
{
    public Animator enemyAnimator;
    public NavMeshAgent agent;

    public int health = 3;
    public BarController barController;
    public NPCController npcController;

    void Start()
    {
        if (barController == null)
            barController = FindFirstObjectByType<BarController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            enemyAnimator.SetTrigger("attack");

            barController.leftBar.fillAmount = 1f;
            barController.rightBar.fillAmount = 1f;

            npcController.StartChase();

            PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("hitBox"))
        {
            agent.isStopped = false;
        }
    }
}