using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int numberOfNPCsToSpawn = 10;
    public float spawnRadius = 10.0f;

    private void Start()
    {
        if (npcPrefab == null)
        {
            Debug.LogError("NPC prefab is not assigned.");
            enabled = false;
            return;
        }

        for (int i = 0; i < numberOfNPCsToSpawn; i++)
        {
            Vector3 randomPosition = RandomGroundPosition();
            Instantiate(npcPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 RandomGroundPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius + transform.position;
        Ray ray = new Ray(randomPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point;
        }

        // If no ground was found, return the original position
        return randomPosition;
    }
}
