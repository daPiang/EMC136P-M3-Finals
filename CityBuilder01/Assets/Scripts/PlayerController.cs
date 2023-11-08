using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    private Vector3 destination = Vector3.zero;
    [SerializeField] private Vector3 workTransform;

    public enum PlayerState {
        Player_Wandering,
        Player_WalkingToDestination,
        Player_Working,
        Player_Sleeping,
        Player_Talking,
        Player_Dancing
    }

    public PlayerState state;
    private bool goingToWork, atWork;

    void Update()
    {
        // Debug.Log(IsPointerOverUIElement());
        Debug.Log(goingToWork);

        if(animator != null) AnimHandler();

        if(state == PlayerState.Player_Working) GameManager.instance.goldCount += GameManager.instance.goldReward * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if(!IsPointerOverUIElement())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("NavMesh Ground"))
                    {
                        atWork = false;
                        state = PlayerState.Player_WalkingToDestination;
                        destination = hit.point;
                        navMeshAgent.SetDestination(destination);
                    }
                }
            }
        }

        if(Vector3.Distance(transform.position, destination) < 2f && !goingToWork && !atWork)
        {
            Debug.Log("Arrived at destination");
            state = PlayerState.Player_Wandering;
        }
        else if(Vector3.Distance(transform.position, destination) < 2f && goingToWork && !atWork)
        {
            Debug.Log("Arrived at work");
            state = PlayerState.Player_Working;
            goingToWork = false;
            atWork = true;
        }
    }

    void AnimHandler()
    {
        animator.SetBool("moving", navMeshAgent.velocity.magnitude > 0.1f);
        animator.SetBool("talking", state == PlayerState.Player_Talking);
        animator.SetBool("working", state == PlayerState.Player_Working);
        animator.SetBool("sleeping", state == PlayerState.Player_Sleeping);
        animator.SetBool("dancing", state == PlayerState.Player_Dancing);
    }

    bool IsPointerOverUIElement()
    {
        // Check if the mouse pointer is over a UI element
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    public void GoToWork()
    {
        if(!atWork)
        {
            state = PlayerState.Player_WalkingToDestination;
            destination = workTransform;
            goingToWork = true;
            navMeshAgent.SetDestination(workTransform);
        }
    }
}
