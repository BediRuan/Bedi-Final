using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimerUI : MonoBehaviour
{
    public Slider timeSlider; // Assign this in the inspector
    public PacmanController pacmanController;

    void Update()
    {
        if (pacmanController != null)
        {
            // Update the slider based on the current duration of the power-up
            if (pacmanController.isPowerActive)
            {
                timeSlider.gameObject.SetActive(true);
                timeSlider.value = pacmanController.currentPowerTime / pacmanController.maxPowerTime;
            }
            else
            {
                timeSlider.gameObject.SetActive(false);
            }
        }
    }
}
