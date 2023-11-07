using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int npcCount;
    public float goldCount;
    public float goldReward = 10;
    public NPC[] npcs;

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
        goldCount += goldReward * npcCount + Time.deltaTime;

        if(TimeManager.instance.GetCurrentTime().Hour >= 6f && TimeManager.instance.GetCurrentTime().Hour <= 20.5f)
        {
            // Debug.Log("It's Day");
            timeOfDay = TimeOfDay.Day;
        }
        else if(TimeManager.instance.GetCurrentTime().Hour > 20.5f || TimeManager.instance.GetCurrentTime().Hour < 6f)
        {
            // Debug.Log("It's Night");
            timeOfDay = TimeOfDay.Night;
        }
    }
}
