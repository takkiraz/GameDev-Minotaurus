using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawning script on Player, Spawns enemy if Player enters SpawningTag
public class Spawning : MonoBehaviour
{
    [SerializeField] GameObject enemy = default;
    Vector3 spawnPos1;
    Vector3 spawnPos2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnTag"))
        {
            Spawn();
            if (other != null)
                Destroy(other.gameObject);
        }
    }

    private void Spawn()
    {
        Camera cam = GameObject.FindWithTag("CameraCurrent").GetComponent<Camera>();
        if (enemy == null) return;

        // Spawn Enemy to the right of Camera
        spawnPos1 = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 10f));
        spawnPos1.y = enemy.transform.position.y;

        // Spawn Enemy to the left of Camera
        spawnPos2 = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 10f));
        spawnPos2.y = enemy.transform.position.y;

        Instantiate(enemy, spawnPos1, Quaternion.identity);
        Instantiate(enemy, spawnPos2, Quaternion.identity);
    }
}
