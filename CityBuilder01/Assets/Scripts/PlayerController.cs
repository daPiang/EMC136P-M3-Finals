using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    public enum PlayerState {
        Player_Wandering,
        Player_Waiting,
        Player_WalkingToDestination,
        Player_Working,
        Player_Sleeping,
        Player_Talking,
        Player_ForceTalking,
        Player_Dancing
    }

    public PlayerState state;

    // Update is called once per frame
    void Update()
    {
        if(animator != null) AnimHandler();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("NavMesh Ground"))
                {
                    navMeshAgent.SetDestination(hit.point);
                }
            }
        }
    }

    void AnimHandler()
    {
        animator.SetBool("moving", navMeshAgent.velocity.magnitude > 0.1f);
        animator.SetBool("talking", state == PlayerState.Player_Talking || state == PlayerState.Player_ForceTalking);
        animator.SetBool("working", state == PlayerState.Player_Working);
        animator.SetBool("sleeping", state == PlayerState.Player_Sleeping);
        animator.SetBool("dancing", state == PlayerState.Player_Dancing);
    }
}
