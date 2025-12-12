using UnityEngine;
using UnityEngine.AI;

public class SimpleAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 8f;
    public float attackRange = 1.8f;
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    int current = 0;
    enum State { Patrol, Chase, Attack }
    State state = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
            agent.destination = patrolPoints[0].position;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange)
            state = State.Attack;
        else if (dist <= chaseRange)
            state = State.Chase;
        else
            state = State.Patrol;

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.hasPath || agent.remainingDistance < 0.4f)
        {
            current = (current + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[current].position;
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.destination = player.position;
    }

    void Attack()
    {
        agent.isStopped = true;
        // Add punch / bite animation here later
    }
}
