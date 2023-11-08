using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float wanderRange = 10.0f;
    private NavMeshAgent navMeshAgent;
    private Vector3 patrolPoint;

    public enum NpcState {
        NPC_Wandering,
        NPC_WalkingToDestination,
        NPC_Working,
        NPC_Sleeping,
        NPC_Talking,
        NPC_Dancing
    }

    public NpcState state;
    [SerializeField] private Animator animator;
    [SerializeField] private Schedule[] scheduleArray;
    [SerializeField] private int scheduleIndex = 0;
    private bool talking;
    private bool hasMoved;

    private void Start()
    {
        state = NpcState.NPC_Sleeping;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch(state)
        {
            case NpcState.NPC_Wandering:
                SetRandomDestination();
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


        if(!sched.willTalkToSomeone) //Execute regular scheduling stuff if NPC will not talk someone on current queued sched
        {
            if(time.Hour == sched.hour && time.Minute == sched.minute) //Check if NPC should start walking to destination
            {
                state = NpcState.NPC_WalkingToDestination; //FOR WALKING
                navMeshAgent.SetDestination(destination);
                hasMoved = true;
            }

            if(Vector3.Distance(transform.position, destination) < 0.1f && hasMoved) //Check if NPC is at destination
            {
                state = sched.npcStateAtDestination; //Once at destination, change state to do task
                scheduleIndex = (scheduleIndex + 1) % scheduleArray.Length;
                talking = false;
                hasMoved = false;
            }
        }
        else //Code for if an NPC has to talk to someone
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
        animator.SetBool("talking", state == NpcState.NPC_Talking);
        animator.SetBool("working", state == NpcState.NPC_Working);
        animator.SetBool("sleeping", state == NpcState.NPC_Sleeping);
        animator.SetBool("dancing", state == NpcState.NPC_Dancing);
    }

    private void SetRandomDestination()
    {
        if (navMeshAgent.remainingDistance < 1f)
        {
            // Debug.Log($"new Patrol point");
            patrolPoint = GetRandomPoint();
            navMeshAgent.SetDestination(patrolPoint);
        }
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f;
        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, 10f, -1);

        var destination = navHit.position;
        return destination;
    }

}
