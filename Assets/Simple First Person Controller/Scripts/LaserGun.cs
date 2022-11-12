using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class LaserGun : NetworkBehaviour
{
    public Transform laserTransform;
    public LineRenderer line;


    

    void Update()
    {
        if(isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            CmdShoot();
        }
    }


     void Start()
    {
       
    }

    [Command]
    public void CmdShoot()
    {

        Ray ray = new Ray(laserTransform.position, laserTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //we hit something - draw the line
            //Check hit
            var playerHealth = hit.collider.gameObject.GetComponent<BossHealth>();
            var playerLocation = hit.collider.gameObject.GetComponent<BOSSAI>();
            Vector3 Loc = gameObject.transform.position;
            var playerConnection = hit.collider.gameObject.GetComponent<NetworkIdentity>();

            Debug.Log("are you playinng?");

            if (playerHealth)
            {
                //respawn
             playerHealth.BossDamage(20);
             playerLocation.locationtoshoot(Loc);

            // command send target positiion
                


                
            }
            RpcDrawLaser(laserTransform.position, hit.point);
        }
        else
        {
            RpcDrawLaser(laserTransform.position, laserTransform.forward);
        }
    }


   
 


    [ClientRpc]
    void RpcDrawLaser(Vector3 start, Vector3 end)
    {
        // do laser files enumerator function
        StartCoroutine(LaserFlash(start, end));
    }

    IEnumerator LaserFlash(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        yield return new WaitForSeconds(0.3f);

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
    }

}
