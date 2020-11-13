using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MyNetworkManager :  NetworkManager
{
    public int ConnectionsToStartGame = 2;

    public MessageToAllClients MessageToAllClients;

    public override void OnStartServer()
    {
        Debug.Log("Server Started!");

    }
    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected to Server!");
        MessageToAllClients.numberOfClients = NetworkManager.numPlayers;
        Debug.Log("NUMBER OF CLIENTS: "+ numPlayers);
        StartGameButtonEnabled();
    }


    public override void OnServerReady(NetworkConnection conn)
    {

    }

    void StartGameButtonEnabled(){
        
        if(MessageToAllClients.numberOfClients>=ConnectionsToStartGame){
            MessageToAllClients.UnlockButton();
        }
    }
    
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Disconnected from Server!");
        MessageToAllClients.numberOfClients--;
    }
}
