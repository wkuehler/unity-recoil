using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector
    public float projectileSpeed = 10f; // Speed of the projectile
    public float recoilForce = 0.25f; // Recoil force applied to the shooter
    private Rigidbody2D rb; // Rigidbody2D of the shooter


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        TrackMouse();

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
            ApplyRecoil();
        }
    }

    void TrackMouse()
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z; // Ensure the Z coordinate is the same as the GameObject

        // Calculate the direction from the GameObject to the mouse position
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calculate the angle to rotate towards the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the GameObject
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FireProjectile()
    {
        // Instantiate the projectile at the GameObject's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply force to the projectile in the direction the GameObject is facing
            rb.velocity = transform.right * projectileSpeed; // Assuming the GameObject's right side is the forward direction
        }
    }

    void ApplyRecoil()
    {
        // Apply recoil force in the opposite direction of the GameObject's forward direction
        rb.AddForce(-transform.right * recoilForce, ForceMode2D.Impulse);
    }
}
