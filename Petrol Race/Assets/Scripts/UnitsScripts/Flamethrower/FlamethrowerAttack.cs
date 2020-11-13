using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerAttack : MonoBehaviour
{
    public GameObject flamesRefParticle;
    public float damageTick = 1.5f;
    public float checkTick = 0.25f;
    public int damageAmmount = 8;

    private List<Unit> objectivesRef  =new List<Unit>();
    IEnumerator FlamethrowerCoroutine(){
        while (GetComponent<Unit>().alive)
        {
             yield return new WaitForSeconds(checkTick);
            if(GetComponent<Unit>().attacking){
                flamesRefParticle.SetActive(true);
                foreach (Unit item in objectivesRef)
                {
                    if(item!=null){
                    item.HP = Mathf.Clamp(item.HP-damageAmmount, 0, item.MaxHP);
                    item.UpdateHealth();
                    }
                }
                
                yield return new WaitForSeconds(damageTick);
            }
        }
    }
    private void OnTriggerEnter(Collider objective){
        if(objective.transform.GetComponent<Unit>()!=null){
            objectivesRef.Add(objective.transform.GetComponent<Unit>());
        }
    }
    private void OnTriggerExit(Collider objective){
        if(objective.transform.GetComponent<Unit>()!=null){
            objectivesRef.Remove(objective.transform.GetComponent<Unit>());
        }
    }


     public void StartAttackUnit()
    {
        StopAllCoroutines();
        StartCoroutine(FlamethrowerCoroutine());
        
    }
    public void StopAttackingUnit()
    {
        StopAllCoroutines();
        flamesRefParticle.SetActive(false);
    }

}
