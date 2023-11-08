using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int npcCount;
    public float goldCount;
    public float goldReward = 420;
    public GameObject focusObject;

    public enum TimeOfDay
    {
        Day,
        Night
    }

    public TimeOfDay timeOfDay;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        timeOfDay = TimeOfDay.Day;

        cameraTransform.position = new(playerTransform.position.x, 0, playerTransform.position.z); //TPs the camera to center on the player
    }

    private void Update()
    {
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }
    }
}
