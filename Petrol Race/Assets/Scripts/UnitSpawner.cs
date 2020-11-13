using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{
    public Player ownerRef;

    public GameObject spawnableCollector;
    public GameObject spawnableHarrier;
    public GameObject spawnableFlamethrower;
    public Transform spawnPoint;
    public Transform spawnPointFlyingUnit;
    public int rowSize = 5;
    public float rowStretch = 1.2f;
    public int columnSize = 5;
    public float columnStretch = 1.2f;


    private List<Vector3> availableSpawnLocationsFly = new List<Vector3>();
    private List<Vector3> availableSpawnLocationsGround= new List<Vector3>();

    private float overlapSpawnAmmount = 0.5f;
   

    private void OnDrawGizmos()
    {
        //FLYING
        Gizmos.color = Color.cyan;
        for (int i = -rowSize; i <= rowSize; i++)
        {
            for (int j = 0; j >= -columnSize; j--)
            {
                Vector3 nextPoint = spawnPointFlyingUnit.position;
                nextPoint = new Vector3(nextPoint.x + i*rowStretch, nextPoint.y, nextPoint.z+j*columnStretch);

                Gizmos.DrawSphere(nextPoint, 0.2f);
            }
        }
        //GROUND
        Gizmos.color = Color.red;
        for (int i = -rowSize; i <= rowSize; i++)
        {
            for (int j = 0; j >= -columnSize; j--)
            {
                Vector3 nextPoint = spawnPoint.position;
                nextPoint = new Vector3(nextPoint.x + i* rowStretch, nextPoint.y, nextPoint.z + j*columnStretch);

                Gizmos.DrawSphere(nextPoint, 0.2f);
            }
        }
    }

    private void SpawnPointsSetup()
    {
        //FLY
        for (int i = -rowSize; i <= rowSize; i++)
        {
            for (int j = 0; j >= -columnSize; j--)
            {
                Vector3 nextPoint = spawnPointFlyingUnit.position;
                availableSpawnLocationsFly.Add(new Vector3(nextPoint.x + i * rowStretch, nextPoint.y, nextPoint.z + j* columnStretch));
            }
        }
        //GROUND
        for (int i = -rowSize; i <= rowSize; i++)
        {
            for (int j = 0; j >= -columnSize; j--)
            {
                Vector3 nextPoint = spawnPoint.position;
                availableSpawnLocationsGround.Add(new Vector3(nextPoint.x + i*rowStretch, nextPoint.y, nextPoint.z + j*columnStretch));
            }
        }
    }

    private Vector3 GetAvailableLocationFLY()
    {
        foreach (Vector3 location in availableSpawnLocationsFly)
        {
            if(Physics.OverlapSphere(location, overlapSpawnAmmount).Length==0)
            {
                return location;
            }
        }
        return spawnPointFlyingUnit.position;
    }

    private Vector3 GetAvailableLocationGROUND()
    {
        foreach (Vector3 location in availableSpawnLocationsGround)
        {
            if (Physics.OverlapSphere(location, overlapSpawnAmmount).Length == 0)
            {
                return location;
            }
        }
        return spawnPoint.position;
    }

    //DIFFERENT UNITS TO SPAWN
    public void SpawnCollector()
    {
        if (ownerRef.currentPlayerResources >= UnitsCosts.CollectorCost)
        {
            ownerRef.currentPlayerResources -= UnitsCosts.CollectorCost;
            ownerRef.UpdateResources();
            GameObject newUnit = Instantiate(spawnableCollector, GetAvailableLocationGROUND(), Quaternion.identity, null);
            ownerRef.units.Add(newUnit.GetComponent<Unit>());
            ownerRef.NotEnoughResources.SetActive(false);
        }
        else
        {
            NotEnoughResources();
        }
    }

    public void SpawnHarrier()
    {
        if (ownerRef.currentPlayerResources >= UnitsCosts.HarrierCost)
        {
            ownerRef.currentPlayerResources -= UnitsCosts.HarrierCost;
            ownerRef.UpdateResources();
            GameObject newUnit = Instantiate(spawnableHarrier, GetAvailableLocationFLY(),Quaternion.identity,null);
            //newUnit.GetComponent<NavMeshAgent>().SetDestination();
            ownerRef.units.Add(newUnit.GetComponent<Unit>());
            ownerRef.NotEnoughResources.SetActive(false);
        }
        else
        {
            NotEnoughResources();
        }
    }

    public void SpawnFlamethrower()
    {
        if (ownerRef.currentPlayerResources >= UnitsCosts.FlamethrowerCost)
        {
            ownerRef.currentPlayerResources -= UnitsCosts.FlamethrowerCost;
            ownerRef.UpdateResources();
            GameObject newUnit = Instantiate(spawnableFlamethrower, GetAvailableLocationGROUND(), Quaternion.identity, null);
            ownerRef.units.Add(newUnit.GetComponent<Unit>());
            ownerRef.NotEnoughResources.SetActive(false);
        }
        else
        {
            NotEnoughResources();
        }
    }


    public void NotEnoughResources()
    {
        ownerRef.NotEnoughResources.SetActive(true);
        ownerRef.NotEnoughResources.GetComponent<Animation>().Play();
    }

    private void Awake()
    {
        //ownerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //GENERATE POINTS AROUND SPAWNPOINT
        SpawnPointsSetup();
    }
}
