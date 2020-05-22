using UnityEngine;
using Mirror;

public class AuthRequestPlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        FindObjectOfType<AuthRequestObject>().p = this;
    }

    [Command]
    public void CmdRequestAuthority(NetworkIdentity id)
    {
        Debug.Log("AuthRequestPlayer:CmdRequestAuthority");
        id.AssignClientAuthority(connectionToClient);
        
    }

    [Command]
    public void Cmdbuild(NetworkIdentity id)
    {
        id.GetComponent<AuthRequestObject>().color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }

    [Command]
    public void CmdRemoveAuthority(NetworkIdentity id)
    {
        Debug.Log("AuthRequestPlayer:CmdRequestAuthority");
        id.RemoveClientAuthority();
    }

}
