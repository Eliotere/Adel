using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 10; // Damage dealt by the projectile

    private GameObject owner; // The shooter of the projectile
    private Vector3 direction;

    public void Initialize(Vector3 shootDirection, GameObject shooter)
    {
        owner = shooter; // Set the shooter as the owner
        direction = shootDirection.normalized;

        // Ignore collisions with the shooter for 0.2 seconds
        Collider ownerCollider = shooter.GetComponent<Collider>();
        Collider projectileCollider = GetComponent<Collider>();
        if (ownerCollider != null && projectileCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, ownerCollider, true);
            Invoke(nameof(EnableCollisionWithOwner), 0.2f);
        }

        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void EnableCollisionWithOwner()
    {
        // Re-enable collision with the shooter
        Collider ownerCollider = owner.GetComponent<Collider>();
        Collider projectileCollider = GetComponent<Collider>();
        if (ownerCollider != null && projectileCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, ownerCollider, false);
        }
    }

    void Update()
    {
        // Move forward in its own facing direction
        transform.position += direction * speed * Time.deltaTime;

        // Align the forward direction visually
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.forward = direction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with the shooter
        if (other.gameObject == owner)
        {
            return;
        }

        // Check for collision with objects using tags
        if (other.CompareTag("Enemy"))
        {
            // Example: Apply damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile on impact
        }
        else if (other.CompareTag("Wall"))
        {
            // Destroy the projectile if it hits a wall
            Destroy(gameObject);
        }
    }
}