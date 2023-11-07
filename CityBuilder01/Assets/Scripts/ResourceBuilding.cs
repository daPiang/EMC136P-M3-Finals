using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEPRECATED

public class ResourceBuilding : MonoBehaviour
{
    private int npcCount = 0;
    private int npcCountMax = 2;
    [SerializeField] private int baseReward;
    private float rewardTimer = 0;
    private GameObject[] workers;

    private void Update() {
        rewardTimer += Time.deltaTime;

        if(npcCount > 0 && rewardTimer > 1)
        {
            rewardTimer = 0;
            // GameManager.instance.foodCount += baseReward;
        }

        // npcCount = workers.Length;
    }

    // public void AddWorker()
    // {
    //     GameObject idleWorker = GameManager.instance.FindNpcByState(NPC.NpcState.NPC_Idle);
    //     // Debug.Log("Before state change: " + idleWorker);
    //     // idleWorker.GetComponent<NPC>().state = NPC.NpcState.NPC_Working;
    //     // idleWorker.ChangeState(NPC.NpcState.NPC_Working);
    //     // Debug.Log("After state change: " + idleWorker);
    //     workers.Add(idleWorker);
    //     workers[workers.Count - workers.Count].GetComponent<NPC>().state = NPC.NpcState.NPC_Working;
    //     Debug.Log(workers.Count);
    //     // Debug.Log("Added: " + idleWorker + " to list!");
    // }

    // public void RemoveWorker()
    // {
    //     GameObject worker = FindWorkingNPC();
    //     // worker.GetComponent<NPC>().state = NPC.NpcState.NPC_Idle;
    //     workers[workers.Count - workers.Count].GetComponent<NPC>().state = NPC.NpcState.NPC_Idle;
    //     workers.Remove(worker);
    //     Debug.Log(workers.Count);
    // }

    // public GameObject FindWorkingNPC()
    // {
    //     return GameManager.instance.FindNpcByState(NPC.NpcState.NPC_Working, workers);
    // }
}
