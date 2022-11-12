using UnityEngine;
using System.Collections;
using Mirror;

public class BOSSAI : NetworkBehaviour
{

    public GameObject Projectile;
    public float ProjectileForce = 100f;
    public float ProjectileFireDelay = 1f;
    [HideInInspector] public Vector3 BossToplayerDir;
    [HideInInspector] public Vector3 PlayerTransform;
    [HideInInspector] public Vector3 PlayerTransformOld;


    public Transform laserTransform;
    public LineRenderer line;

    private float timer = 0f;
    private Vector3 projectileDirectionOffset = new Vector3(0f, 0f, 0f); // So the boss aims above the player and lobs the projectile in an arc


    // Start is called before the first frame update
    void Start()
    {

    }

 

    [ClientRpc]
    void SFireProjectile(Vector3 start, Vector3 end)
    {

        Ray ray = new Ray(laserTransform.position, laserTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit Com, 200f))
        {
            Debug.Log("WTF");
            //we hit something - draw the line
            //Check hit
            var playerHealth = Com.collider.gameObject.GetComponent<PlayerHealth>();
          

            Debug.Log(playerHealth);
             
                
            if (playerHealth)
            {
                //respawn
                playerHealth.PlayerDamage(20);

                Debug.Log("WTF");


            }
            RpcDrawLaser(start, end);
        }
        else
        {
            Debug.Log("You are here");
          RpcDrawLaser(start, laserTransform.forward);
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
        yield return new WaitForSeconds(0.1f);

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
    }

    

    void Update()
    {
        

        timer += Time.deltaTime;

        if (timer >= ProjectileFireDelay)
        {
            SFireProjectile(transform.position, PlayerTransform);
          
            timer = 0;
        }
    }

    
    public void locationtoshoot(Vector3 Location)
    {
      PlayerTransform = Location;
        PlayerTransformOld = PlayerTransform;
        
    }



   

    void ChargeAttack()
    {
        // Build-up phase
        // Charge phase
        // rest phase
    }
}
