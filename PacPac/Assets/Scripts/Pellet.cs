using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to the score
            ScoreManager.instance.AddScore(points);
            // Then disable or destroy the pellet
            gameObject.SetActive(false);


            FindObjectOfType<GameManager>().AddPellet();
        }
    }
}

