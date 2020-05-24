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

    [Command]
    public void CmdRequestAuthority(NetworkIdentity id)
    {
        id.AssignClientAuthority(connectionToClient);

    }

    [Command]
    public void Cmdbuild(NetworkIdentity id, Vector3 spawnPoint)
    {
        RpcPay();
        RpcBuild(id, spawnPoint);
        id.GetComponent<Node>().color = new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0, 1F), UnityEngine.Random.Range(0, 1F));
    }

    [ClientRpc]
    public void RpcBuild(NetworkIdentity id, Vector3 spawnPoint)
    {
        GameObject build = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
        //NetworkServer.Spawn(build);
    }

    [ClientRpc]
    public void RpcPay()
    {
        Gold -= 100;
        canvasPlayer.changeGold(Gold);
    }

    [Command]
    public void CmdSetUp()
    {
        RpcSetUp();
    }

    [ClientRpc]
    public void RpcSetUp()
    {
        Gold = startGold;
        canvasPlayer.changeGold(Gold);
    }

    [Command]
    public void CmdRemoveAuthority(NetworkIdentity id)
    {
        id.RemoveClientAuthority();
    }
}
