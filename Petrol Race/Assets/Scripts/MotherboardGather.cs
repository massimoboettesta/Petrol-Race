using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherboardGather : MonoBehaviour
{
    private Player myPlayer;
    //EMPTY COLLECTOR AND GATHER ITS RESOURCES
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collector")){
            myPlayer.currentPlayerResources+= other.gameObject.GetComponent<ResourcesHolder>().DeliverResources();
            myPlayer.UpdateResources();
            other.gameObject.GetComponent<ResourcesHolder>().EmptyCollector();
            other.gameObject.GetComponent<ResourcesHolder>().VisualizerCrystals.SetActive(false);
            other.gameObject.GetComponent<PathFollow>().GoToNextPoint();
        }
    }

    private void Awake()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
