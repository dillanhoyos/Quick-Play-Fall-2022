using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject Projectile;
    public float ProjectileForce = 100f;
    public float ProjectileFireDelay = 1f;

    private float timer = 0f;
    private Vector3 projectileDirectionOffset = new Vector3(0f,10f,0f); // So the boss aims above the player and lobs the projectile in an arc


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);

        Rigidbody projRb = projectile.GetComponent<Rigidbody>();
        Vector3 playerDir = PlayerTransform.position - transform.position;
        playerDir += projectileDirectionOffset;
        projRb.AddForce(playerDir * ProjectileForce);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= ProjectileFireDelay)
        {
            FireProjectile();
            timer = 0;
        }
    }
}
