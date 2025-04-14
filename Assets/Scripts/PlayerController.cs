using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

// The player shmovement you shmell me ?
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 10f;

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float fireRate = 0.25f;

    private float fireTimer = 0f;

    private CharacterController controller;
    private Vector2 inputVector;
    private Vector3 smoothMove;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => inputVector = Vector2.zero;

        controls.Player.Shoot.performed += ctx => ShootPressed();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Smoothly interpolate movement input
        Vector3 targetMove = new Vector3(inputVector.x, 0, inputVector.y);
        smoothMove = Vector3.Lerp(smoothMove, targetMove, acceleration * Time.deltaTime);

        controller.Move(smoothMove * moveSpeed * Time.deltaTime);
        RotateTowardMouse();

        fireTimer -= Time.deltaTime;
    }

    // Player rotation towards mouse position
    void RotateTowardMouse()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 direction = (hitPoint - transform.position);
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
            }
        }
    }

    // Fire projectile from the shoot point
        void FireProjectile()
    {
        if (projectilePrefab == null || shootPoint == null) return;

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector3 direction = transform.forward;

        proj.GetComponent<Projectile>().Initialize(direction);
    }

        void ShootPressed()
    {
        if (fireTimer <= 0f)
        {
            FireProjectile();
            fireTimer = fireRate;
        }
    }

}


