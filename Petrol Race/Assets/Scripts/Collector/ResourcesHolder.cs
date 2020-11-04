using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHolder : MonoBehaviour
{
    public int CrystalCollectionPower = 5;
    public bool inventoryFull;
    public GameObject VisualizerCrystals;
    public int GasCollectionPower = 5;

    public int CrystalsCollected = 0;

    private ResourceSource SourceRef;
    private Player myPlayer;
    //GIVE RESOURCES TO FUNCTION CALLER
    public int DeliverResources()
    {
       return CrystalsCollected;
    }
    //EMPTY OUT THE COLLECTOR
    public void EmptyCollector()
    {
        CrystalsCollected = 0;
        inventoryFull = false;
    }


    private void Awake()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (CrystalsCollected == 0 && other.gameObject.CompareTag("Resource"))
        {
           StartCoroutine(StartCollecting(other.gameObject));
        }
    }

    IEnumerator StartCollecting(GameObject source)
    {
        Debug.Log("Collecting...");
        inventoryFull = true;
        yield return new WaitForSeconds(1.0f);
        CrystalsCollected = source.GetComponent<ResourceSource>().GatherResource(CrystalCollectionPower, myPlayer);
        VisualizerCrystals.SetActive(true);
        
        Debug.Log("Collected");
        GetComponent<PathFollow>().GoToNextPoint();
    }

}
