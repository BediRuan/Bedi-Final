using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{
    public float speed = 5.0f;
    private float originalSpeed;
    private bool nextPortalIsYellow = true;
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool canKillEnemies = false; // Flag to check if player can kill enemies
    public bool isPowerActive = false;
    public float currentPowerTime = 0f;
    public float maxPowerTime = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = speed; // Store the original speed
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");//Basic movement
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PortalManager.Instance.CreatePortal(transform.position, nextPortalIsYellow);
            nextPortalIsYellow = !nextPortalIsYellow;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoost());
    }

    IEnumerator SpeedBoost()
    {
        speed *= 1.1f;
        yield return new WaitForSeconds(8);
        speed = originalSpeed;
    }

    // Coroutine to handle enemy elimination power-up
    public void ActivateKillEnemies()
    {
        StartCoroutine(KillEnemies());
    }

    IEnumerator KillEnemies()
    {
        canKillEnemies = true;  // Set the flag to true to enable killing enemies
        isPowerActive = true;
        currentPowerTime = maxPowerTime;

        while (currentPowerTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentPowerTime -= 1f;
        }

        canKillEnemies = false;  // Reset the flag after the power-up time expires
        isPowerActive = false;
    }

}
