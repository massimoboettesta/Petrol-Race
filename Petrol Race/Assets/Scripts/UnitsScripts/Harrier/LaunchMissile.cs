using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileStartLoc, missileStartLoc2;
    public float missileCadency = 0.3f;
    public float attackSpeed = 2.0f;
    public int damageAmmount = 10;
    public float onReachCD = 0.5f;

   IEnumerator LaunchingMissile()
    {
        while (GetComponent<Unit>().alive)
        {
                yield return new WaitForSeconds(missileCadency);
                GameObject currentMissile;
                if (GetComponent<Unit>().objectiveUnit != null && GetComponent<Unit>().attacking)
                {
                    currentMissile = Instantiate(missilePrefab, missileStartLoc);
                    currentMissile.GetComponent<HomingMissile>().objective = GetComponent<Unit>().objectiveUnit.gameObject;
                    currentMissile.GetComponent<HomingMissile>().damageAmmount = damageAmmount;
                    currentMissile.GetComponent<HomingMissile>().whoIsMyParent = gameObject;
                    currentMissile.GetComponent<HomingMissile>().gameObject.transform.parent = null;
                    yield return new WaitForSeconds(missileCadency);
                }else{
                    StopAttackingUnit();
                    yield return null;
                }
               
                if (GetComponent<Unit>().objectiveUnit != null && GetComponent<Unit>().attacking)
                {
                    currentMissile = Instantiate(missilePrefab, missileStartLoc2);
                    currentMissile.GetComponent<HomingMissile>().objective = GetComponent<Unit>().objectiveUnit.gameObject;
                    currentMissile.GetComponent<HomingMissile>().damageAmmount = damageAmmount;
                    currentMissile.GetComponent<HomingMissile>().whoIsMyParent = gameObject;
                    currentMissile.GetComponent<HomingMissile>().gameObject.transform.parent = null;
                    yield return new WaitForSeconds(attackSpeed);
                }else{
                    StopAttackingUnit();
                    yield return null;
                }
            
        }
        yield return null;
    }

    public void StartAttackUnit()
    {
        StopAllCoroutines();
        StartCoroutine(LaunchingMissile());
    }
    public void StopAttackingUnit()
    {
        StopAllCoroutines();
    }

}
