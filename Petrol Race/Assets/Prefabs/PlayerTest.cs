using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerTest : NetworkBehaviour
{
    public TextMeshProUGUI ammountText;
    [SyncVar(hook = nameof(OnZChange))]
    public int z;

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal*0.1f,moveVertical*0.1f, 0);
            transform.position = transform.position + movement;
           
        }
    }

    private void Update()
    {
        HandleMovement();
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X)){
            Debug.Log("Sending Hola to server.");
            Hola();
        }
    }

    [Command]
    void Hola(){
        Debug.Log("Received Hola from client.");
        z++;
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola(){
        Debug.Log("Received Hola from server.");
    }

    [ClientRpc]
    void StopMoving(){
        Debug.Log("Stop Moving!");
    }

    void OnZChange(int oldZ, int newZ){
        ammountText.text = z.ToString();
    }

}
