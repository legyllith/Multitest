using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Node : NetworkBehaviour
{
    public Player p;
    public MeshRenderer myRenderer;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        myRenderer.material.color = Color.blue;
    }

    public void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    public void OnMouseEnter()
    {
        myRenderer.material.color = Color.red ;
        
    }
    public void OnMouseExit()
    {
        myRenderer.material.color = Color.white;

    }
    public void OnMouseDown()
    {
        // Cmdbuild();
        Debug.Log("entre");
        //this.GetComponent<NetworkIdentity>().AssignClientAuthority(p.GetComponent<NetworkIdentity>().connectionToClient);
        p.CmdRequestAuthority(GetComponent<NetworkIdentity>());

    }

    [Command]
    public void Cmdbuild()
    {
        //validate logic here 
        p.CmdRequestAuthority(GetComponent<NetworkIdentity>());
        //GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        /*Rpcbuild();
        GetComponent<NetworkIdentity>().RemoveClientAuthority();*/
    }

    [ClientRpc]
    private void Rpcbuild()
    {
        myRenderer.material.color = Color.blue;
    }
}
