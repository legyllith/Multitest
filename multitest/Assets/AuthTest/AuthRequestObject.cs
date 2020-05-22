using UnityEngine;
using Mirror;

public class AuthRequestObject : NetworkBehaviour
{
    public AuthRequestPlayer p;
    [SyncVar] public Color color = Color.white;

    public void Update()
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    public void OnMouseDown()
    {
        Debug.Log("AuthRequestObject:OnMouseDown");
        p.CmdRequestAuthority(netIdentity);
        p.Cmdbuild(netIdentity);
        //Rpcbuild();
        //color = Color.blue;
        //p.CmdRemoveAuthority(netIdentity);
        p.CmdRemoveAuthority(netIdentity);
    }

    

    [ClientRpc]
    public void Rpcbuild()
    {
        color = Color.blue;
        //GetComponent<MeshRenderer>().material.color = Color.blue;
    }
    /*public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }*/
}
