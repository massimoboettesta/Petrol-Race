using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    public GameObject WorldSpaceMoveUIEffect;
    public RectTransform selectionBox;
    public LayerMask unitLayerMask;
    public LayerMask MovementLayerMask;

    private Player myPlayer;
    private bool enemyDetected;

    private List<Unit> selectedUnits = new List<Unit>();
    private Vector2 startPos;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        myPlayer = GetComponentInParent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectSelection();
    }

    //CHECK IF IM OVER UI
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    //SELECTION OF UNITS
    private void DetectSelection()
    {
        //CLICK
        if (Input.GetMouseButtonDown(0)&& !IsMouseOverUI())
        {
            startPos = Input.mousePosition;

            ClearSelectedUnits();
            SelectUnit(Input.mousePosition);
        }
        //CLICK HELD DOWN
        if (Input.GetMouseButton(0) && !IsMouseOverUI())
        {
            UpdateSelectionBox(Input.mousePosition);
        }


        //RELEASE CLICK
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }


        //ORDER UNITS TO MOVE
        if (Input.GetMouseButtonDown(1) && !IsMouseOverUI())
        {
            DetectEnemy(Input.mousePosition);
            MoveAgentsToDest(Input.mousePosition);
        }

        //UPDATE PATHS OF SELECTED UNITS
        foreach (Unit currUnit in selectedUnits)
        {
            currUnit.ShowPath();
        }

    }

    //TRY TO DETECT ENEMY
    private void DetectEnemy(Vector2 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Vector3 destination;

            //IF I CLICKED OVER A UNIT
            if (Physics.Raycast(ray, out hit, 100, unitLayerMask))
            {
                destination = hit.point;
                

                if (selectedUnits.Count > 0)
                {
                    //SPAWN MOVE AGENTS UI ELEMENT
                    //Instantiate(WorldSpaceMoveUIEffect, destination, Quaternion.identity);

                    foreach (Unit currUnit in selectedUnits)
                    {
                        if (currUnit.gameObject.GetComponent<NavMeshAgent>() != null && currUnit.objectiveUnit == null 
                        && currUnit != hit.collider.gameObject.GetComponent<Unit>())
                        {
                            currUnit.StopAttackingUnit();
                            currUnit.objectiveUnit = hit.collider.gameObject.GetComponent<Unit>();
                            currUnit.AttackUnit();
                            enemyDetected = true;
                        }
                    }
                }

            }
            else
            {
                enemyDetected = false;
            }
        
    }

    //MOVE AGENTS TO DESTINATION
    private void MoveAgentsToDest(Vector2 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Vector3 destination;

            //IF I CLICKED OVER GROUND
            if (Physics.Raycast(ray, out hit, 100, MovementLayerMask)&& !enemyDetected)
            {
                destination = hit.point;

                if (selectedUnits.Count > 0)
                {
                    //SPAWN MOVE AGENTS UI ELEMENT
                    Instantiate(WorldSpaceMoveUIEffect, destination, Quaternion.identity);

                    foreach (Unit currUnit in selectedUnits)
                    {
                        if(currUnit != null){
                            if (currUnit.gameObject.GetComponent<NavMeshAgent>() != null)
                            {
                                enemyDetected = false;
                                currUnit.StopAttackingUnit();
                                currUnit.objectiveUnit = null;
                                currUnit.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                                currUnit.gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
                                
                            }
                        }
                    }
                }
            }
    }
    //SELECTION BOX CREATION AND UPDATE
    private void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }
    //SELECTION BOX RELEASE
    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        //CHECKING OVERLAP WITH ALL PLAYER UNITS
        foreach (Unit currUnit in myPlayer.units)
        {
            if(currUnit!=null){
                Vector3 screenPos = cam.WorldToScreenPoint(currUnit.transform.position);

                if(screenPos.x>min.x && screenPos.x<max.x && screenPos.y>min.y && screenPos.y < max.y)
                {
                    selectedUnits.Add(currUnit);
                    currUnit.SelectUnit();
                }

            }
        }
    }

    private void ClearSelectedUnits()
    {
        //DESELECT ALL UNITS
        foreach (Unit currentUnit in selectedUnits)
        {
            if(currentUnit!=null){
                 currentUnit.DeselectUnit();
            }
        }
        selectedUnits.Clear();
    }
    //SELECT THE MOUSE POSITION UNIT
    private void SelectUnit(Vector2 screenPos)
    {
        
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 100, unitLayerMask))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                //SEECT SECIFIC UNIT
                selectedUnits.Add(unit);
                unit.SelectUnit();
                
            }
        }
        
    }
}
