using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50; // Enemy's health
    public float moveSpeed = 2f; // Movement speed
    public Transform target; // Target to follow (e.g., the player)
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public float fireRate = 2f; // Time between shots

    private float shootTimer = 0f;

    private void Update()
    {
        // Move towards the target if assigned
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Rotate to face the target
            if (direction.sqrMagnitude > 0.001f)
            {
                transform.forward = direction;
            }

            // Handle shooting
            HandleShooting();
        }
    }

    private void HandleShooting()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= fireRate)
        {
            ShootAtTarget();
            shootTimer = 0f;
        }
    }

    private void ShootAtTarget()
    {
        if (projectilePrefab != null && target != null)
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);

            // Initialize the projectile's direction
            Vector3 shootDirection = (target.position - transform.position).normalized;
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(shootDirection, gameObject); // Pass the shooter as the owner
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy the enemy object
        Destroy(gameObject);
    }
}