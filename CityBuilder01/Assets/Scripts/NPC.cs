using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float wanderRange = 10.0f;
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public enum NpcState {
        NPC_Idle,
        NPC_Working
    }

    public NpcState state;

    private void Start()
    {
        state = NpcState.NPC_Idle;
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        switch(state)
        {
            case NpcState.NPC_Idle:
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f) SetRandomDestination();
                break;
            case NpcState.NPC_Working:
                break;
        }
        
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += initialPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRange, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}
