using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private TextMeshProUGUI npcCount;
    [SerializeField] private TextMeshProUGUI foodCount;
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        BuildingPanel.SetActive(false);
    }

    private void Update()
    {
        npcCount.text = GameManager.instance.npcCount.ToString();
        foodCount.text = GameManager.instance.foodCount.ToString();

        if (GameManager.instance.focusObject != null && GameManager.instance.focusObject.CompareTag("Building"))
        {
            BuildingPanel.SetActive(true);

            // Add listeners to buttons
            addButton.onClick.AddListener(AddButton);
            removeButton.onClick.AddListener(RemoveButton);
        }
        else
        {
            BuildingPanel.SetActive(false);

            // Remove listeners if the BuildingPanel is not active
            addButton.onClick.RemoveAllListeners();
            removeButton.onClick.RemoveAllListeners();
        }
    }

    private void AddButton()
    {
        if (GameManager.instance.focusObject != null)
        {
            ResourceBuilding resourceBuilding = GameManager.instance.focusObject.GetComponent<ResourceBuilding>();
            if (resourceBuilding != null)
            {
                resourceBuilding.AddWorker();
            }
        }
    }

    private void RemoveButton()
    {
        if (GameManager.instance.focusObject != null)
        {
            ResourceBuilding resourceBuilding = GameManager.instance.focusObject.GetComponent<ResourceBuilding>();
            if (resourceBuilding != null)
            {
                resourceBuilding.RemoveWorker();
            }
        }
    }
}
