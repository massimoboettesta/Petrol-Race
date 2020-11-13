using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    public GameObject objective;
    public GameObject whoIsMyParent;
    public GameObject explosionEffect;
    public float homingForce = 30.0f;
    public float rotationForce = 200.0f;

    public int damageAmmount = 10;
    
    private Rigidbody rb;


    private void HomeMissile(GameObject target)
    {
        if (target != null)
        {
            var direction = (target.transform.position - rb.position).normalized;
            Vector3 rotationAmmount = Vector3.Cross(transform.up, direction);
            rb.angularVelocity = rotationAmmount * rotationForce;
            rb.velocity = transform.up * homingForce;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Unit>()&& collision.gameObject != whoIsMyParent && !collision.gameObject.GetComponent<HomingMissile>())
        {
            Unit myRef = collision.gameObject.GetComponent<Unit>();
            //Damage Objective
            myRef.HP = Mathf.Clamp(myRef.HP-damageAmmount, 0, myRef.MaxHP);
            myRef.UpdateHealth();
            Instantiate(explosionEffect, collision.contacts[0].point,Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        HomeMissile(objective);
    }
}
