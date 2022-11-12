using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class BossHealth : NetworkBehaviour
{

    [SyncVar]
    public int health = 100;



    [ClientRpc]
    public void BossDamage(int amount)
    {
        health -= amount;
        if (health <= 0)

        {

            health = 100;
            StartCoroutine(Respawn(gameObject));
        }

    }



    [Server]
    IEnumerator Respawn(GameObject go)
    {
        NetworkIdentity identity = go.GetComponent<NetworkIdentity>();
        NetworkServer.UnSpawn(go);
        Debug.Log(identity.hasAuthority);
        Debug.Log(identity.assetId);
        Transform newPos = NetworkManager.singleton.GetStartPosition();
        go.transform.position = newPos.position;
        go.transform.rotation = newPos.rotation;
        yield return new WaitForSeconds(1f);
        NetworkServer.Spawn(go, identity.assetId, identity.connectionToServer);



    }

}
