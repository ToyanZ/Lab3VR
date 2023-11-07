using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float growScale = 7;
    public Rigidbody rb;
    public float destroyTime = 6;
    public float activeTime = 3;
    float counter = 0;
    public ParticleSystem particles;
    bool firstContact = false;
    private void Update()
    {
        counter += Time.deltaTime;
        if(counter >= destroyTime) Destroy(gameObject);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (firstContact) return;
        firstContact = true;
        transform.localScale = transform.localScale * growScale;
        rb.velocity = Vector3.zero;

        Instantiate(particles, collision.contacts[0].point, Quaternion.identity);

        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            
        }
        else
        {

        }

        if(collision.gameObject.GetComponent<IA_Enemies>() != null)
        {
            collision.gameObject.GetComponent<IA_Enemies>().KillEnemy();
        }
        
        Destroy(gameObject, activeTime);

    }
}
