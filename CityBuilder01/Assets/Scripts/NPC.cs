using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float wanderRange = 10.0f;
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public enum NpcState {
        NPC_Wandering,
        NPC_Waiting,
        NPC_WalkingToDestination,
        NPC_Working,
        NPC_Sleeping,
        NPC_Talking,
        NPC_ForceTalking,
        NPC_FollowSched
    }

    public NpcState state;

    [SerializeField] private Animator animator;

    [SerializeField] private Schedule[] scheduleArray;
    private int scheduleIndex = 0;

    private void Start()
    {
        state = NpcState.NPC_Sleeping;
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        // SetRandomDestination();
    }

    private void Update()
    {
        switch(state)
        {
            case NpcState.NPC_Wandering:
                SetRandomDestination();
                break;
            case NpcState.NPC_FollowSched:
                // ScheduleHandler();
                break;
        }

        if(animator != null) AnimHandler();
        if(scheduleArray != null) ScheduleHandler();
    }

    private void ScheduleHandler()
    {
        DateTime time = TimeManager.instance.GetCurrentTime();
        Schedule sched = scheduleArray[scheduleIndex];
        Vector3 destination = new(sched.pointInWorldToWalkTo.x, transform.position.y, sched.pointInWorldToWalkTo.z);

        // Debug.Log("Task: " + sched.scheduledTask);
        // Debug.Log("Schedule Index: " + scheduleIndex);
        if(time.Hour == sched.hour && time.Minute == sched.minute) //Check if NPC should start walking to destination
        {
            state = NpcState.NPC_WalkingToDestination; //FOR WALKING
            navMeshAgent.SetDestination(destination);
        }

        // Debug.Log(navMeshAgent.destination);
        if(Vector3.Distance(transform.position, destination) < 0.1f) //Check if NPC is at destination
        {
            state = sched.npcStateAtDestination; //Once at destination, change state to do task
            scheduleIndex = (scheduleIndex + 1) % scheduleArray.Length;
        }
    }

    void AnimHandler()
    {
        animator.SetBool("moving", navMeshAgent.velocity.magnitude > 0.1f);
        animator.SetBool("talking", state == NpcState.NPC_Talking || state == NpcState.NPC_ForceTalking);
        animator.SetBool("working", state == NpcState.NPC_Working);
        animator.SetBool("sleeping", state == NpcState.NPC_Sleeping);
    }

    private void SetRandomDestination()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRange;
            randomDirection += initialPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRange, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                navMeshAgent.SetDestination(targetPosition);
            }
        }
    }
}
