using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;
    [SerializeField] private float minSpawnRadius;
    [SerializeField] private float maxSpawnRadius;
    [SerializeField] private float spawnRate = 1;
    private float spawnTimer = 0;
    private float waveTimer = 5f;

    private void Update()
    {
        this.spawnTimer -= Time.deltaTime;
        if (this.spawnTimer <= 0f)
        {
            this.Spawn();
        }

        this.waveTimer -= Time.deltaTime;
        if (this.waveTimer <= 0f)
        {
            this.spawnRate += 0.1f;
            this.waveTimer = 10f;
        }
    }

    private void Spawn()
    {
        Vector2 spawn = Vector2.zero;
        spawn.x = Random.Range(this.minSpawnRadius, this.maxSpawnRadius);
        spawn.y = Random.Range(this.minSpawnRadius, this.maxSpawnRadius);
        switch (Random.Range(1, 5))
        {
            case 1:
                break;
            case 2:
                spawn.x *= -1;
                break;
            case 3:
                spawn.y *= -1;
                break;
            case 4:
                spawn.x *= -1;
                spawn.y *= -1;
                break;
        }
        GameObject obj = Instantiate(this.zombie);
        obj.transform.position = spawn;
        this.spawnTimer = 1 / this.spawnRate;
    }
}
