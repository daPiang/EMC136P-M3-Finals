using System;
using UnityEngine;

[System.Serializable]
public class Schedule {
    public string scheduledTask;
    public float hour; //NPC should leave for destination at this time.
    public float minute;

    public NPC.NpcState npcStateAtDestination;
    public Vector3 pointInWorldToWalkTo;
    public bool willTalkToSomeone;
    public GameObject PersonToTalkTo;
}