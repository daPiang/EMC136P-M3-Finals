using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : MonoBehaviour
{
    private int npcCount = 0;
    private int npcCountMax = 2;
    [SerializeField] private int baseReward;
    private float rewardTimer = 0;
    private List<NPC> workers = new();

    private void Update() {
        rewardTimer += Time.deltaTime;

        if(npcCount > 0 && rewardTimer > 1)
        {
            rewardTimer = 0;
            GameManager.instance.foodCount += baseReward;
        }

        npcCount = workers.Count;
    }

    public void AddWorker()
    {
        if(npcCount != npcCountMax)
        {
            NPC idleWorker = GameManager.instance.FindNpcByState(NPC.NpcState.NPC_Idle);
            idleWorker.state = NPC.NpcState.NPC_Working;
            workers.Add(idleWorker);
        }
    }

    public void RemoveWorker()
    {
        NPC worker = FindWorkingNPC();
        worker.state = NPC.NpcState.NPC_Idle;
        workers.Remove(worker);
    }

    public NPC FindWorkingNPC()
    {
        return GameManager.instance.FindNpcByState(NPC.NpcState.NPC_Working, workers);
    }
}
