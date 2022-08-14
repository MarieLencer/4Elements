/* Reference Note:
 * The following tutorials / documentation was used in this script:
 * Player Detection: https://www.youtube.com/watch?v=j1-OyLo77ss
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    

    [SerializeField] public float detectionsRange = 30.0f;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] public Animator animator;
    private EnemyState state = EnemyState.Spawning;

    private GameObject playerTarget;

    private Vector3 startPatrolling;

    private Vector3 stopPatrolling;

    private EnemyState previousState;

    private EnemySpawner spawner;

    private bool startTurn = false;
    
    private AttackManager attackManager;

    private bool firing = false;

    private float stopAtPatrol = 0.0f;
    private float stopAtAttack = 10.0f;
    
    void Update()
    {
        switch (state)
        {
            case EnemyState.Spawning:
                Spawning();
                break;
            case EnemyState.Patrolling :
                animator.SetBool("attacking", false);
                playerTarget = null;
                agent.SetDestination(stopPatrolling);
                break;
            case EnemyState.Turn:
                if(!startTurn) TurnAroundPatrolling();
                break;
            case EnemyState.PlayerDetected:
                agent.SetDestination(playerTarget.transform.position);
                animator.SetBool("attacking", false);
                break;
            case EnemyState.Attack:
                if (!firing)
                {
                    StartCoroutine("Attack");
                }
                break;
            case EnemyState.Dying:
                StopCoroutine(CheckState());
                StartCoroutine(Death());
                break;
        }
    }

    private bool checkForPlayer()
    {
        Collider[] playerColliders = new Collider[4];
        int numPlayersFound = Physics.OverlapSphereNonAlloc(transform.position, detectionsRange, playerColliders, playerMask);
        if (numPlayersFound > 0)
        {
            GameObject closestPlayer = playerColliders[0].gameObject;
            for (int i = 0; i < numPlayersFound; i++)
            {
                if (playerColliders[i].gameObject.GetComponent<PlayerLife>().isDead())
                {
                    continue;
                }
                if (Vector3.Distance(transform.position, playerColliders[i].transform.position) <
                    Vector3.Distance(transform.position, closestPlayer.transform.position))
                {
                    
                    closestPlayer = playerColliders[i].gameObject;
                }
            }

            if (closestPlayer.GetComponent<PlayerLife>().isDead())
            {
                return false;
            }
            playerTarget = closestPlayer;
        }

        return numPlayersFound > 0;
    }

    IEnumerator CheckState ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (checkForPlayer())
            {
                if (agent.stoppingDistance >= Vector3.Distance(transform.position, playerTarget.transform.position))
                {
                    SetState(EnemyState.Attack);
                }
                else
                {
                    SetState(EnemyState.PlayerDetected);
                    agent.stoppingDistance = stopAtAttack;
                }
            } else
            {
                if (previousState != EnemyState.Patrolling)
                {
                    SetState(EnemyState.Patrolling);
                    StartPatrolling();
                }
                else if (agent.remainingDistance <= stopAtPatrol && previousState != EnemyState.Turn)
                {
                    SetState(EnemyState.Turn);
                }
                
            }
        }
    }

    private void StartPatrolling()
    {
        startTurn = false;
        startPatrolling = transform.position;
        stopPatrolling = new Vector3(-transform.position.x, transform.position.y,transform.position.z);
        agent.stoppingDistance = stopAtPatrol;
    }

    private void TurnAroundPatrolling()
    {
        agent.speed = 0f;
        stopPatrolling = startPatrolling;
        startPatrolling = transform.position;
        agent.SetDestination(stopPatrolling);
        agent.speed = 3.5f;
    }

    public void SetState(EnemyState newState)
    {
        previousState = state;
        state = newState;
    }

    IEnumerator Death()
    {
        animator.SetBool("dying", true);
        yield return new WaitForSeconds(2f);
        spawner.removeEnemy(gameObject);
        Destroy(gameObject);
    }

    private void Spawning()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        spawner = gameObject.GetComponentInParent<EnemySpawner>();
        attackManager = gameObject.GetComponent<AttackManager>();
        animator.SetBool("spawnEnd", true);
        StartCoroutine(CheckState());
    }
    
    IEnumerator Attack()
    {
        firing = true;
        transform.LookAt(playerTarget.transform);
        animator.SetBool("attacking", true);
        attackManager.OnSpawnAttack();
        yield return new WaitForSeconds(1.75f);
        animator.SetBool("attacking", false);
        firing = false;
    }
}
