using System.Collections;
using System.Linq;
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
            GameObject npc = Instantiate(npcPrefab, randomPosition, Quaternion.identity);
            if(npc != null) GameManager.npcs.Add(npc.GetComponent<NPC>());
        }
    }

    private Vector3 RandomGroundPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius + transform.position;
        randomPosition.y = 1;
        Ray ray = new(randomPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point;
        }

        return randomPosition;
    }
}
