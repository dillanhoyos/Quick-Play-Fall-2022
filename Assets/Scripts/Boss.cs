using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject Projectile;
    public float ProjectileForce = 100f;
    public float ProjectileFireDelay = 1f;
    [HideInInspector] public Vector3 BossToplayerDir;

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

        BossToplayerDir += projectileDirectionOffset;
        projRb.AddForce(BossToplayerDir * ProjectileForce);
    }

    void Update()
    {
        BossToplayerDir = PlayerTransform.position - transform.position;

        timer += Time.deltaTime;

        if (timer >= ProjectileFireDelay)
        {
            FireProjectile();
            timer = 0;
        }
    }

    void ChargeAttack()
    {
        // Build-up phase
        // Charge phase
        // rest phase
    }
}
