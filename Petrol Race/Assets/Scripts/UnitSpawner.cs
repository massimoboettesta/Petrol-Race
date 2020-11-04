using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Collector,
    Harrier
}
public class UnitSpawner : MonoBehaviour
{
    private Player playerRef;

    public GameObject spawnableCollector;
    public GameObject spawnableHarrier;
    public Transform spawnPoint;
    public Transform spawnPointFlyingUnit;
    public SpawnType type;

    public void SpawnCollector()
    {
        GameObject newUnit = Instantiate(spawnableCollector, spawnPoint);
        playerRef.units.Add(newUnit.GetComponent<Unit>());
    }
    public void SpawnHarrier()
    {
        GameObject newUnit = Instantiate(spawnableHarrier, spawnPointFlyingUnit);
        playerRef.units.Add(newUnit.GetComponent<Unit>());
    }

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
