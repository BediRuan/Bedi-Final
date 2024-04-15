using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject portalPrefabYellow;
    public GameObject portalPrefabBlue;
    private GameObject currentPortalYellow;
    private GameObject currentPortalBlue;

   
    public static PortalManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePortal(Vector3 position, bool isYellow)
    {
        // If it's a yellow portal and one already exists, destroy it
        if (isYellow && currentPortalYellow != null)
        {
            Destroy(currentPortalYellow);
        }
        // If it's a blue portal and one already exists, destroy it
        else if (!isYellow && currentPortalBlue != null)
        {
            Destroy(currentPortalBlue);
        }

        // Create the new portal
        GameObject portalPrefab = isYellow ? portalPrefabYellow : portalPrefabBlue;
        GameObject newPortal = Instantiate(portalPrefab, position, Quaternion.identity);

        
        if (isYellow)
        {
            currentPortalYellow = newPortal;
            // If there's already a blue portal, the new yellow portal should become the teleportation destination
            if (currentPortalBlue != null)
            {
                currentPortalBlue.GetComponent<Portal>().SetDestination(newPortal.transform);
            }
        }
        else
        {
            currentPortalBlue = newPortal;
            // If there's already a yellow portal, the new blue portal should become the teleportation destination
            if (currentPortalYellow != null)
            {
                currentPortalYellow.GetComponent<Portal>().SetDestination(newPortal.transform);
            }
        }

        // Set up teleportation destinations for the new portal
        newPortal.GetComponent<Portal>().SetDestination(isYellow ? currentPortalBlue?.transform : currentPortalYellow?.transform);
    }
}
