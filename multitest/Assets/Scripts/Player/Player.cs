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

    [Command]
    public void CmdRequestAuthority(NetworkIdentity id)
    {
        Debug.Log("ok");
        id.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        //CmdRequestAuthority(id);
    }

    /*[Command]
    public void CmdRequestAuthority(NetworkIdentity id)
    {
        //validate logic here 
        Debug.Log("ok 2");
        id.AssignClientAuthority(connectionToClient);
    }*/


    /*[Command]
    public void Cmdbuild(Node node)
    {
        //validate logic here 
        Rpcbuild(node);
    }

    [ClientRpc]
    private void Rpcbuild(Node node)
    {
         node.myRenderer.material.color = Color.blue;
    }*/
}
