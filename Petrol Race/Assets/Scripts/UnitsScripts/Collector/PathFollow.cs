using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollow : MonoBehaviour
{
    //[SerializeField]
    private Transform[] points = new Transform[2];

    private int destinationPoint = 0;

    private NavMeshAgent agent;

    private ResourcesHolder recollected;
    // Start is called before the first frame update
    void Start()
    {
        recollected = GetComponent<ResourcesHolder>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        //SETUP BASE POINT
        FindNearestMotherboard();
        FindNearestCrystalSource();

        GoToNextPoint();
    }
   
    private void FindNearestMotherboard()
    {
        GameObject[] motherBoards = GameObject.FindGameObjectsWithTag("Motherboard");
        GameObject closestMotherBoard = new GameObject();
        float lowestDist = Mathf.Infinity;
        for (int i = 0; i < motherBoards.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, motherBoards[i].transform.position);
            if(dist< lowestDist)
            {
                lowestDist = dist;
                closestMotherBoard = motherBoards[i];
            }
        }
        if (closestMotherBoard != null)
        {
            //SETS THE SECOND POINT TO THE MOTHERBOARD
            
            points[1] = closestMotherBoard.transform;
        }
    }

    private void FindNearestCrystalSource()
    {
        GameObject[] availableCrystals = GameObject.FindGameObjectsWithTag("Resource");
        GameObject closestCrystalSource = new GameObject();
        float lowestDist = Mathf.Infinity;
        for (int i = 0; i < availableCrystals.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, availableCrystals[i].transform.position);
            if (dist < lowestDist)
            {
                lowestDist = dist;
                closestCrystalSource = availableCrystals[i];
            }
        }
        if (closestCrystalSource != null)
        {
            //SETS THE SECOND POINT TO THE MOTHERBOARD
            points[0] = closestCrystalSource.transform;
        }
    }
    public void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            Debug.Log("No Destinations Set: " + transform.name);
            return;
        }
        agent.destination = points[destinationPoint].position;

        destinationPoint = (destinationPoint+1) % points.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance && !recollected.inventoryFull)
        {
            GoToNextPoint();
        }
    }
}
