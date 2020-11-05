using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public TextMeshProUGUI resourcesAmmountUI;
    public AudioListener audioListener;
    public GameObject myCamera;
    public GameObject UI;
    public GameObject LoadingScreen;
    public GameObject NotEnoughResources;
    [SyncVar]
    public int currentPlayerResources = 0;
    [SyncVar]
    public int connectionID;
    public List<Unit> units = new List<Unit>();

    

    public void UpdateResources()
    {
        resourcesAmmountUI.text = currentPlayerResources.ToString();
    }

    public void AddCollectorsToUnits()
    {
        GameObject[] collectors = GameObject.FindGameObjectsWithTag("Collector");

        foreach (GameObject go in collectors)
        {
            if (go.GetComponent<Unit>())
            {
                units.Add(go.GetComponent<Unit>());
            }
        }
    }


    private void Awake()
    {
        UpdateResources();
        AddCollectorsToUnits();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer)
        {
            LoadingScreen.SetActive(false);
            myCamera.SetActive(true);
            UI.SetActive(true);
        }
        else if (!isLocalPlayer)
        {
            if(audioListener!=null)
            Destroy(audioListener);

            LoadingScreen.SetActive(false);
            myCamera.SetActive(false);
            UI.SetActive(false);
        }
    }
}
