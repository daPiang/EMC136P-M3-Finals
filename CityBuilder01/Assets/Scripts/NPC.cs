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
        NPC_Working,
        NPC_Sleeping,
        NPC_Talking
    }

    public NpcState state;

    public enum NpcJob {
        NPC_Wood,
        NPC_Stone,
        NPC_Food
    }

    public NpcJob job;

    [SerializeField] private Animator animator;

    private void Start()
    {
        state = NpcState.NPC_Idle;
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        if(GameManager.instance.timeOfDay == GameManager.TimeOfDay.Day) state = NpcState.NPC_Working;
        else state = NpcState.NPC_Idle;

        switch(state)
        {
            case NpcState.NPC_Idle:
                SetRandomDestination();
                break;
            case NpcState.NPC_Working:
                JobHandler();
                break;
        }

        if(animator != null) AnimHandler();
        
    }

    private void JobHandler()
    {
        switch(job)
        {
            case NpcJob.NPC_Wood:
                break;
            case NpcJob.NPC_Food:
                break;
            case NpcJob.NPC_Stone:
                break;
        }
    }

    void AnimHandler()
    {
        animator.SetBool("moving", navMeshAgent.velocity.magnitude > 0.1f);
        animator.SetBool("talking", state == NpcState.NPC_Working); //temp
        animator.SetBool("sleeping", state == NpcState.NPC_Sleeping);
    }

    private void SetRandomDestination()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
            randomDirection += initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRange, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                navMeshAgent.SetDestination(targetPosition);
            }
        }
    }
}
