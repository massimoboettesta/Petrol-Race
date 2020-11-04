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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Unit>()&& collision.gameObject != whoIsMyParent && !collision.gameObject.GetComponent<HomingMissile>())
        {
            //Damage Objective
            collision.gameObject.GetComponent<Unit>().HP -= 5;
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
