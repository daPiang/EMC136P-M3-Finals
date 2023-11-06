using System.Collections;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    private GameObject npcTospawn;
    public float spawnRadius = 10.0f;

    private void Start()
    {
        if (npcPrefabs == null)
        {
            Debug.LogError("NPC prefab is not assigned.");
            enabled = false;
            return;
        }
        SpawnNpcType("wood");
        SpawnNpcType("stone");
        SpawnNpcType("food");
    }

    public void SpawnNpcType(string npcType)
    {
        switch(npcType)
        {
            case "wood":
                npcTospawn = npcPrefabs[0];
                break;
            case "stone":
                npcTospawn = npcPrefabs[1];
                break;
            case "food":
                npcTospawn = npcPrefabs[2];
                break;
        }

        Vector3 randomPosition = RandomGroundPosition();
        GameObject npc = Instantiate(npcTospawn, randomPosition, Quaternion.identity);
        // if(npc != null) GameManager.npcs.Add(npc);
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
