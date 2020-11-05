using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MyNetworkManager :  NetworkManager
{
    public int ConnectionsToStartGame = 2;
    public MotherboardGather MotherBoardClient;


    private List<NetworkConnection> ClientConnections = new List<NetworkConnection>();


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
        ClientConnections.Add(conn);
    }



    public override void OnServerReady(NetworkConnection conn)
    {
/*
        
        //NetworkServer.AddPlayerForConnection(conn, newPlayer);
        
        if (ClientConnections.Count >= ConnectionsToStartGame)
        {
            Debug.Log("TRYING TO CALL START GAME!");
            //SPAWN NEW PLAYER
            Debug.Log("New Player " + conn.connectionId + " spawned!");
            GameObject newPlayer = Instantiate(singleton.playerPrefab);
            newPlayer.GetComponent<Player>().connectionID = conn.connectionId;
            //MOTHERBOARD REFERENCE TO PLAYER
            MotherBoardClient.myPlayer = newPlayer.GetComponent<Player>();

            //ADD IT TO CONNECTION
            NetworkServer.AddConnection(conn);
            GetComponent<FunctionToAllClients>().enabled = true;
        }
*/
    }
    
    
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        ClientConnections.Remove(conn);
        Debug.Log("Disconnected from Server!");
    }
}
