using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GhostAI : MonoBehaviour
{
    public Transform[] waypoints;//An array of Transform references to waypoints the ghost will navigate between.
    private int currentWaypointIndex = 0;//Tracks the current target waypoint.
    public float speed = 5f; //Movement speed of the ghost.
    public Transform player; 
    public GhostType ghostType; 

    
    public enum GhostType
    {
        Chaser,
        Ambusher,
        Random,
        Shy
    }

    private float ambushDistance = 10f; //Distance used by the Ambusher to target ahead of the player.
    private float chaseTriggerDistance = 8f; 
    private float tooCloseDistance = 3f; 

    void Start()
    {
        GhostManager.Instance.RegisterGhost(this); 
        if (waypoints.Length > 0)
        {
            currentWaypointIndex = 0;
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to " + gameObject.name);
        }
    }

    void Update()
    {
        switch (ghostType)
        {
            case GhostType.Chaser:
                SetDestination(player.position);
                break;
            case GhostType.Ambusher:
                Vector3 playerFuturePosition = player.position + (player.forward * ambushDistance);
                SetDestination(playerFuturePosition);
                break;
            case GhostType.Random:
                if (Vector3.Distance(transform.position, player.position) < chaseTriggerDistance)
                {
                    SetDestination(player.position);
                }
                else if (ReachedWaypoint())
                {
                    PickRandomWaypoint();
                }
                break;
            case GhostType.Shy:
                if (Vector3.Distance(transform.position, player.position) < tooCloseDistance)
                {
                    PickRandomWaypoint();
                }
                else
                {
                    SetDestination(player.position);
                }
                break;
        }

        MoveToCurrentWaypoint();
    }

    void SetDestination(Vector3 target)//Sets the current waypoint to the closest one to the given target.
    {
        //Find the closest waypoint to the target
        Transform closest = waypoints.OrderBy(w => (w.position - target).sqrMagnitude).FirstOrDefault();
        if (closest != null)
        {
            currentWaypointIndex = System.Array.IndexOf(waypoints, closest);
            //If closest one is reachable
        }
    }

    void MoveToCurrentWaypoint()//Moves the ghost towards the current waypoint.
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if ((targetWaypoint.position - transform.position).sqrMagnitude < 0.01f)
            //checks if the ghost is close enough to the waypoint to consider it reached
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }//create a continuous loop along the waypoints

    bool ReachedWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        return (targetWaypoint.position - transform.position).sqrMagnitude < 0.01f;
    }

    void PickRandomWaypoint()
    {
        currentWaypointIndex = Random.Range(0, waypoints.Length);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PacmanController playerController = collision.collider.GetComponent<PacmanController>();
            if (playerController && playerController.canKillEnemies)
            {
                Destroy(gameObject);
            }
            else
            {
                FindObjectOfType<GameManager>().Die();
            }
        }
    }

    void OnDestroy()
    {
        if (GhostManager.Instance != null)
        {
            GhostManager.Instance.RemoveGhost(this); 
        }
    }

    public void UpdateSpeed(float factor)
    {
        speed *= factor;
    }
}
