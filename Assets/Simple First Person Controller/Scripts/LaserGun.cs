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

    [Command]
    public void CmdShoot()
    {
        Ray ray = new Ray(laserTransform.position, laserTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //we hit something - draw the line
            //Check hit
            var playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth)
            {
                //respawn
             playerHealth.Damage(20);
            }
            RpcDrawLaser(laserTransform.position, hit.point);
        }
        else
        {
            RpcDrawLaser(laserTransform.position, laserTransform.position + laserTransform.position * 100f);
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
