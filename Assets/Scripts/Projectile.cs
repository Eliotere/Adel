using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifetime);
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

}
