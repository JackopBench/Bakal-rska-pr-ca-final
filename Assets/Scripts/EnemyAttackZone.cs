using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAttackZone : MonoBehaviour
{
    public Animator enemyAnimator;
    public NavMeshAgent agent;

    public int health = 3;
    public BarController barController;
    public NPCController npcController;

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("hitBox"))
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        enemyAnimator.SetTrigger("attack");

        // naplní bar
        barController.leftBar.fillAmount = 1f;
        barController.rightBar.fillAmount = 1f;

        // okamžite začne chase
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