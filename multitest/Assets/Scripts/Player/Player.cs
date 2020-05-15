using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


public class Player : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();
    [Client]
    private void Update()
    {
        if(!hasAuthority) { return; }
        if (!Input.GetKey(KeyCode.Space)) { return; }
        //CmdMove();
        transform.Translate(0, 0, 1f * Time.deltaTime);
        MoveCamera();
    }

    /*[Command]
    private void CmdMove()
    {
        //validate logic here 
        RpcMove();
    }

    [ClientRpc]
    private void RpcMove()
    {
        transform.Translate(0, 0, 1f * Time.deltaTime);
        //Convert.ToSingle(NetworkTime.timeSd)
    }*/

    [Client]
    private void MoveCamera()
    {
        Camera.main.transform.Translate(movement.x, movement.z * Time.deltaTime, movement.y);
    }
}
