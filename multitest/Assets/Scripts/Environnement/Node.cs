using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Node : NetworkBehaviour
{
    public Player p;
    [SyncVar] public Color color = Color.white;
    private bool syncColor = true;

    public void Update()
    {
        if (syncColor)
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    /*public void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }*/

    [Client]
    public void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material.color = Color.red ;
        syncColor = false;
    }

    [Client]
    public void OnMouseExit()
    {
        syncColor = true;

    }
    public void OnMouseDown()
    {
        p.CmdRequestAuthority(netIdentity);
        p.Cmdbuild(netIdentity, this.transform.position);
        //Rpcbuild();
        //color = Color.blue;
        //p.CmdRemoveAuthority(netIdentity);
        p.CmdRemoveAuthority(netIdentity);
    }

    /*[Command]
    public void Cmdbuild()
    {
        //validate logic here 
        p.CmdRequestAuthority(GetComponent<NetworkIdentity>());
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        /*Rpcbuild();
        GetComponent<NetworkIdentity>().RemoveClientAuthority();
    }

    [ClientRpc]
    private void Rpcbuild()
    {
        myRenderer.material.color = Color.blue;
    }*/
}
