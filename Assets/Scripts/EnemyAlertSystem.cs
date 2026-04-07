using UnityEngine;
using System;

public static class EnemyAlertSystem
{
    public static Action<Vector3> OnPlayerSpotted;

    public static void AlertEnemies(Vector3 playerPosition)
    {
        OnPlayerSpotted?.Invoke(playerPosition);
    }
}