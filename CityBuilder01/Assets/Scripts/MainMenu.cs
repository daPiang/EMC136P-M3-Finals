using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if(IsPointOverButton()) SceneManager.LoadScene("SampleScene");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    bool IsPointOverButton()
{
    // Check if the mouse pointer is over a UI element
    PointerEventData eventData = new PointerEventData(EventSystem.current);
    eventData.position = Input.mousePosition;

    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);

    // Check if any of the results are panels and exclude them
    foreach (var result in results)
    {
        if (result.gameObject.CompareTag("Button")) // Change "PanelTag" to the actual tag you use for panels
        {
            return false; // It's over a panel, so return false
        }
    }

    // If there are no UI elements under the mouse (excluding panels), return true
    return results.Count > 0;
}

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
