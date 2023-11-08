using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private TextMeshProUGUI npcCount;
    [SerializeField] private TextMeshProUGUI goldCount;
    [SerializeField] private GameObject nightDecal, dayDecal;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        // npcCount.text = GameManager.instance.npcCount.ToString();
        goldCount.text = GameManager.instance.goldCount.ToString();

        if(GameManager.instance.timeOfDay == GameManager.TimeOfDay.Day)
        {
            dayDecal.SetActive(true);
            nightDecal.SetActive(false);
        }
        else
        {
            dayDecal.SetActive(false);
            nightDecal.SetActive(true);
        }
    }
}
