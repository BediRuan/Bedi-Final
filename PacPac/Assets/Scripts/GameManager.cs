using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Transform pelletParent;


    public GameObject panel_win;
    public GameObject panel_die;
    private int pelletCounter;

    private void Start()
    {
        panel_win.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        panel_die.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

    }
    public void AddPellet()
    {
        pelletCounter++;

        if(pelletCounter == pelletParent.childCount)
        {
            panel_win.SetActive(true);
            FindObjectOfType<GhostAI>().gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        panel_die.SetActive(true);
        FindObjectOfType<PacmanController>().gameObject.SetActive(false);
    }
}
