using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionToAllClients : NetworkBehaviour
{
    public GameObject[] startGameObjects;
    public GameObject LoadingScreenCamera;

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);
        foreach (GameObject object_ in startGameObjects)
        {
            object_.SetActive(true);
        }

    }

    [ClientRpc]
    public void SendMessageToAllClients()
    {
        Debug.Log("START GAME CALLBACK");
        if (LoadingScreenCamera != null) { LoadingScreenCamera.SetActive(false); }

        StartCoroutine(GameStart());
    }
    private void OnEnable()
    {
        SendMessageToAllClients();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
