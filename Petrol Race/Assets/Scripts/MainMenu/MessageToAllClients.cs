using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MessageToAllClients : NetworkBehaviour
{
    public GameObject GOButton;

    [SyncVar]
    public int numberOfClients;

    [ClientRpc]
    public void UnlockButton(){
        GOButton.SetActive(true);
    }
}
