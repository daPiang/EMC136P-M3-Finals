using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int npcCount;
    public int foodCount;
    public int woodCount;
    public int stoneCount;

    public static GameObject[] npcs;

    public GameObject focusObject;

    public enum TimeOfDay
    {
        Day,
        Night
    }

    public TimeOfDay timeOfDay;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        timeOfDay = TimeOfDay.Day;
    }

    private void Update()
    {

        // Debug.Log(Sun.rotation.eulerAngles.x);
        npcCount = GameObject.FindGameObjectsWithTag("NPC").Length;

        if(Sun.rotation.eulerAngles.x >= 0f && Sun.rotation.eulerAngles.x <= 90f)
        {
            Debug.Log("It's Day");
            timeOfDay = TimeOfDay.Day;
        }
        else if(Sun.rotation.eulerAngles.x >= 270f && Sun.rotation.eulerAngles.x <= 360f)
        {
            Debug.Log("It's Night");
            timeOfDay = TimeOfDay.Night;
        }
    }

    // public GameObject FindNpcByState(NPC.NpcState state, List<GameObject> list)
    // {
    //     for (int i = 0; i < list.Count; i++) // Change here: iterate until i < list.Count
    //     {
    //         GameObject npc = list[i];
    //         if (npc.GetComponent<NPC>().state == state)
    //         {
    //             Debug.Log("Found NPC #" + i);
    //             return npc;
    //         }
    //     }

    //     Debug.LogError("No NPCs with " + state + " found!");

    //     return null;
    // }

    // public GameObject FindNpcByState(NPC.NpcState state)
    // {
    //     for (int i = 0; i < npcs.Count; i++) // Change here: iterate until i < npcs.Count
    //     {
    //         GameObject npc = npcs[i];
    //         if (npc.GetComponent<NPC>().state == state)
    //         {
    //             Debug.Log("Found NPC #" + i);
    //             return npc;
    //         }
    //     }

    //     Debug.LogError("No NPCs with " + state + " found!");

    //     return null;
    // }

}
