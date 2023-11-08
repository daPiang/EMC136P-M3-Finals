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
        NPC_FollowSched,
        NPC_Dancing
    }

    public NpcState state;

    [SerializeField] private Animator animator;

    [SerializeField] private Schedule[] scheduleArray;
    [SerializeField] private int scheduleIndex = 0;
    private bool talking;

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
            // case NpcState.NPC_FollowSched:
            //     if(scheduleArray != null) ScheduleHandler();
            //     break;
            // case NpcState.NPC_ForceTalking:
            //     // transform.LookAt();
            //     //Look at player
            //     //Wait for player and npc to finish talking
            //     //change state
            //     break;
            case NpcState.NPC_Waiting:
                break;
        }

        if(animator != null) AnimHandler();
        if(scheduleArray != null) ScheduleHandler();
    }

    private void ScheduleHandler()
    {
        DateTime time = TimeManager.instance.GetCurrentTime();
        Schedule sched = scheduleArray[scheduleIndex];
        Schedule prevSched;
        if(scheduleIndex != 0)
        {
        prevSched = scheduleArray[scheduleIndex - 1];
        } else prevSched = scheduleArray[scheduleArray.Length - 1];
        Schedule nextSched;
        if(scheduleIndex != scheduleArray.Length - 1)
        {
        nextSched = scheduleArray[scheduleIndex + 1];
        } else prevSched = scheduleArray[0];

        Vector3 destination = new(sched.pointInWorldToWalkTo.x, transform.position.y, sched.pointInWorldToWalkTo.z);


        if(!sched.willTalkToSomeone) //Execute regular scheduling stuff if NPC will not talk someone on current queued sched
        {
            if(time.Hour == sched.hour && time.Minute == sched.minute) //Check if NPC should start walking to destination
            {
                state = NpcState.NPC_WalkingToDestination; //FOR WALKING
                navMeshAgent.SetDestination(destination);
            }

            if(Vector3.Distance(transform.position, destination) < 0.1f) //Check if NPC is at destination
            {
                state = sched.npcStateAtDestination; //Once at destination, change state to do task
                scheduleIndex = (scheduleIndex + 1) % scheduleArray.Length;
                talking = false;
            }
        }
        else
        {
            // Debug.Log("has someone to talk to");
            if(time.Hour >= sched.hour && time.Minute >= sched.minute && !talking)
            {
                // Debug.Log("going to this person");
                state = NpcState.NPC_WalkingToDestination; //FOR WALKING
                navMeshAgent.SetDestination(sched.PersonToTalkTo.transform.position);
            }

            if(Vector3.Distance(transform.position, sched.PersonToTalkTo.transform.position) < 3f)
            {
                // Debug.Log("we are talking");
                navMeshAgent.ResetPath();
                talking = true;
                state = NpcState.NPC_Talking;
                transform.LookAt(sched.PersonToTalkTo.transform);
                scheduleIndex = (scheduleIndex + 1) % scheduleArray.Length; //Move to next queued index if current sched has someone to talk to
       
            }
        }
    }

    void AnimHandler()
    {
        animator.SetBool("moving", navMeshAgent.velocity.magnitude > 0.1f);
        animator.SetBool("talking", state == NpcState.NPC_Talking || state == NpcState.NPC_ForceTalking);
        animator.SetBool("working", state == NpcState.NPC_Working);
        animator.SetBool("sleeping", state == NpcState.NPC_Sleeping);
        animator.SetBool("dancing", state == NpcState.NPC_Dancing);
    }

    private void SetRandomDestination()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            // Calculate a random offset within the wanderRange from the NPC's current position.
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * wanderRange;
            Vector3 randomDirection = new(randomOffset.x, 0f, randomOffset.y);
            Vector3 randomPosition = initialPosition + randomDirection;

            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, wanderRange, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                navMeshAgent.SetDestination(targetPosition);
            }
        }
    }

}
