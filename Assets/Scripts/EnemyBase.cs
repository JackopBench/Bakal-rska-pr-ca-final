using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public abstract void OnBarFilled();
    public abstract void OnBarEmpty();
}