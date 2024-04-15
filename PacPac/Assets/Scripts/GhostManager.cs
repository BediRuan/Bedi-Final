using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance { get; private set; }

    private List<GhostAI> ghosts = new List<GhostAI>();
    public float speedIncreaseFactor = 1.05f; // 5% speed increase

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGhost(GhostAI ghost)
    {
        if (!ghosts.Contains(ghost))
        {
            ghosts.Add(ghost);
        }
    }

    public void RemoveGhost(GhostAI ghost)
    {
        if (ghosts.Contains(ghost))
        {
            ghosts.Remove(ghost);
            IncreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        foreach (var ghost in ghosts)
        {
            ghost.UpdateSpeed(speedIncreaseFactor);
        }
    }
}
