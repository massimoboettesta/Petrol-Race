using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public bool isSelected;
    public GameObject unitSelectionUI;
    public GameObject unitInfoUI;

    private NavMeshAgent pathINFO;
    private LineRenderer pathVisual;
    public void SelectUnit()
    {
        if(pathVisual!=null)
           pathVisual.enabled = true;

        isSelected = true;
        unitSelectionUI.SetActive(isSelected);
        if(unitInfoUI!=null)
        unitInfoUI.SetActive(isSelected);
    }
    public void DeselectUnit()
    {
        if (pathVisual != null)
            pathVisual.enabled = false;

        isSelected = false;
        unitSelectionUI.SetActive(isSelected);
        if(unitInfoUI!=null)
        unitInfoUI.SetActive(isSelected);
    }

    public void ShowPath()
    {
        if (pathINFO != null)
        {
            NavMeshPath navPath = pathINFO.path;
            if (pathINFO.hasPath)
            {
                pathVisual.positionCount=navPath.corners.Length;
                pathVisual.SetPosition(0, transform.position);

                for (int i = 1; i < navPath.corners.Length; i++)
                {
                
                    pathVisual.SetPosition(i, navPath.corners[i]);

                }
            }
            else
            {
                pathVisual.positionCount = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        pathINFO = GetComponent<NavMeshAgent>();
        pathVisual = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
