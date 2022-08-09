using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int blood;

    Rigidbody thisGuy;

    private void Awake() {
        blood = 10;
    }

    private void Start()
    {
        thisGuy = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() {

        if(blood <= 0)
        {
            thisGuy.constraints = RigidbodyConstraints.None;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        Debug.Log("KIA");
        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}
