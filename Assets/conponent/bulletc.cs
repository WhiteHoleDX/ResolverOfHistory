using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletc : MonoBehaviour
{
    //private float damage;
    public float bulletSpeed;
    public Rigidbody2D rb;

    private void Start()
    {
        damage = 50f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * Time.maximumDeltaTime * 10;
        Destroy(gameObject,8.5f);
    }

    private void onTriggerEnter2D(GameObject hitInfo)
    {
        Destroy(gameObject);
        if (hitInfo.tag == "Monster")
        {
            Debug.Log(hitInfo.name);
            hitInfo.hurt(this.damage);
        }
    }

    

}
