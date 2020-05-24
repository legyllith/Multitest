using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


public class Player : NetworkBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3();
    [SyncVar] public double Gold = 0;
    public double startGold = 500;
    public CanvasPlayer canvasPlayer;
    public GameObject objectToSpawn;
    [Client]
    

    public override void OnStartLocalPlayer()
    {
        foreach (Node i in FindObjectsOfType<Node>())
        {
            i.p = this;
        }
        canvasPlayer.gameObject.SetActive(true);
        CmdSetUp();
    }

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

    [Command]//assigné l authorité pour pouvoir modifé l objet.
    public void CmdRequestAuthority(NetworkIdentity id)
    {
        id.AssignClientAuthority(connectionToClient);

    }

    [Command]//retiré l authorité
    public void CmdRemoveAuthority(NetworkIdentity id)
    {
        id.RemoveClientAuthority();
    }

    [Command]
    public void Cmdbuild(NetworkIdentity id, Vector3 spawnPoint)
    {
        RpcPay();
        //spawn un objet sur le serveur
        GameObject build = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
        NetworkServer.Spawn(build);
        id.GetComponent<Node>().color = new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0, 1F), UnityEngine.Random.Range(0, 1F));
    }

    [ClientRpc]//ne fait payé que le client et pas tout les client
    public void RpcPay()
    {
        Gold -= 100;
        canvasPlayer.changeGold(Gold);
    }

    [Command]//envoie au serveur qu'une fonction est joué
    public void CmdSetUp()
    {
        RpcSetUp();
    }

    [ClientRpc]//joue sur le client et ne set up que 1 client
    public void RpcSetUp()
    {
        Gold = startGold;
        canvasPlayer.changeGold(Gold);
    }

}
