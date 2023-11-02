using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int npcCount;
    public int foodCount;
    public int woodCount;
    public int stoneCount;

    public static List<NPC> npcs = new();

    public GameObject focusObject;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    private void Update()
    {
        npcCount = npcs.Count;
    }

    public NPC FindNpcByState(NPC.NpcState state, List<NPC> list)
    {
        for(int i = 0; i < list.Count - 1; i++)
        {
            NPC npc = list[i];
            if(npc.state == state)
            {
                return npc;
            }
        }

        Debug.LogError("No NPCs with " + state + " found!");

        return null;
    }

    public NPC FindNpcByState(NPC.NpcState state)
    {
        for(int i = 0; i < npcs.Count - 1; i++)
        {
            NPC npc = npcs[i];
            if(npc.state == state)
            {
                return npc;
            }
        }

        Debug.LogError("No NPCs with " + state + " found!");

        return null;
    }
}
