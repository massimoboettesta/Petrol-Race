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
    public int MaxHP = 100;
    public int HP = 100;
    public bool alive = true;
    public bool attacking = false;
    public Unit objectiveUnit;
    public float attackRange = 30.0f;
    public bool isSelected;
    public GameObject unitSelectionUI;
    public GameObject unitInfoUI;
    public ProgressBar HPUI;


    public float smoothRotation = 10.0f;
    private NavMeshAgent pathINFO;
    private LineRenderer pathVisual;
    private Color lineColor;
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
            if (pathINFO.hasPath && !attacking)
            { 
                if (objectiveUnit != null)
                {
                    pathVisual.material.color = Color.red;
                }
                else
                {
                    pathVisual.material.color = lineColor;
                }
                pathVisual.positionCount=navPath.corners.Length;
                pathVisual.SetPosition(0, transform.position);

                for (int i = 1; i < navPath.corners.Length; i++)
                {
                    if (objectiveUnit!=null && i==navPath.corners.Length-1)
                    {
                       
                        pathVisual.SetPosition(i, objectiveUnit.transform.position);
                    }
                    else
                    {
                        pathVisual.SetPosition(i, navPath.corners[i]);
                    }
                }
            }
            else
            {
                pathVisual.positionCount = 0;
            }
        }
    }

    private Quaternion LookAtRotation(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(objectiveUnit.transform.position - transform.position);
        Quaternion lerpedRotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
        lerpedRotation.x = 0;
        return lerpedRotation;
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
                    transform.rotation = LookAtRotation(objectiveUnit.transform);
                }
                break;
            case UnitType.Flamethrower:
                if (attacking)
                {
                    transform.rotation = LookAtRotation(objectiveUnit.transform);
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
                GetComponent<FlamethrowerAttack>().StartAttackUnit();
                break;
            default:
                break;
        }
    }
    //UPDATE HEALTH
    public void UpdateHealth()
    {
        HPUI.current = HP;
        HPUI.GetCurrentFill();
        //KILL UNIT
        if (HP <= 0)
        {
            alive = false;
            Destroy(gameObject);
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
            GetComponent<FlamethrowerAttack>().StopAttackingUnit();
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
        if(pathVisual!=null)
        lineColor = pathVisual.material.color;

        if (HPUI != null)
        {
            HPUI.maximum = MaxHP;
            UpdateHealth();
        }
        
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
