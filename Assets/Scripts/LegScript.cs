using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegScript : MonoBehaviour
{
    Rigidbody enemy;
    public Health health;

    private void OnTriggerEnter(Collider other) 
    {
       
            enemy = other.GetComponent<Rigidbody>();
            Debug.Log("Got him!");
            pow(enemy);
    }

    public void pow(Rigidbody enemy)
    {
        StartCoroutine(Kickback(enemy));
    }

    private IEnumerator Kickback(Rigidbody body)
    {
        yield return new WaitForSeconds(0.5f);

        body.AddRelativeForce(Vector3.forward * 200f);
        health.blood -= 5;
        Debug.Log(health.blood);

    }
}
