using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : Life
{
    public EnemyController controller;

    private EnemySpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        GameObject trigger = other.gameObject;
        if (trigger.CompareTag("Player Attack"))
        {
            lifePoints -= trigger.GetComponent<BasicAttack>().getDamagePoints();
            Destroy(trigger);
            CheckLife();
        }
    }

    public override void Dying()
    {
        controller.SetState(EnemyState.Dying);
    }

    public override void Spawn()
    {
        maxLifePoints = 40;
        lifePoints = maxLifePoints;
        controller = gameObject.GetComponent<EnemyController>(); 
        controller.SetState(EnemyState.Spawning);
    }

    public override void CheckLife()
    {
        if (lifePoints <= 0)
        {
            Dying();
        }
    }
}
