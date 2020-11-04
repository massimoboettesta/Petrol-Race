using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitType
{
    NotSpecified,
    Collector,
    Harrier,
    Flamethrower
}
public class Unit : MonoBehaviour
{
    public UnitType myType;
    public int HP = 100;
    public bool alive = true;
    public bool attacking = false;
    public Unit objectiveUnit;
    public float attackRange = 30.0f;
    public bool isSelected;
    public GameObject unitSelectionUI;
    public GameObject unitInfoUI;


    private float smoothRotation = 10.0f;
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


    public void LookAtObjective()
    {
        switch (myType)
        {
            case UnitType.NotSpecified:
                break;
            case UnitType.Collector:

                break;
            case UnitType.Harrier:
                if (attacking)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(objectiveUnit.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
                }
                break;
            case UnitType.Flamethrower:
                if (attacking)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(objectiveUnit.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
                }
                break;
            default:
                break;
        }
    }

    /* void OnDrawGizmosSelected()
     {
         Vector3 locationToGo = transform.position + (objectiveUnit.transform.position - transform.position);
         Gizmos.color = Color.red;
         Gizmos.DrawSphere(locationToGo,.3f);
     }*/

    public bool CheckAttackRange()
    {
        if (objectiveUnit != null)
        {
            float dist = Vector3.Distance(objectiveUnit.transform.position, transform.position);
            if (dist <= attackRange)
            {
                Debug.DrawLine(transform.position, objectiveUnit.transform.position, Color.red);
                pathINFO.isStopped = true;
                return attacking = true;
            }
            else
            {
                //IF WE ARE OUT OF RANGE WE SHOULD MOVE CLOSE
                pathINFO.isStopped = false;
                Vector3 locationToGo = transform.position + (objectiveUnit.transform.position - transform.position);
                pathINFO.SetDestination(locationToGo);

                return attacking = false;
            }
        }
        return attacking = false;
    }

    //START ATTACKING UNIT
    public void AttackUnit()
    {
        //ATTACK FUNCTION
        switch (myType)
        {
            case UnitType.NotSpecified:
                break;
            case UnitType.Collector:
                break;
            case UnitType.Harrier:
                GetComponent<LaunchMissile>().StartAttackUnit();
                break;
            case UnitType.Flamethrower:
               
                break;
            default:
                break;
        }
    }
    //STOP ATTACKING UNIT
    public void StopAttackingUnit()
    {
        attacking = false;
        switch (myType)
        {
            case UnitType.NotSpecified:
                break;
            case UnitType.Collector:
                break;
            case UnitType.Harrier:
                GetComponent<LaunchMissile>().StopAttackingUnit();
                break;
            case UnitType.Flamethrower:
                break;
            default:
                break;
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
        if (objectiveUnit != null)
        {
            CheckAttackRange();
            LookAtObjective();
        }
        
    }
}
