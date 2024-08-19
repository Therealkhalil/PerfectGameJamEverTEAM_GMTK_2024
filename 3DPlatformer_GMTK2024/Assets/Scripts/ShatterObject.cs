using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterObject : MonoBehaviour
{
    //The object's current health point total
    [SerializeField]
    private GameObject brokenPrefab;
    public bool UseAdditionalUpForce = false;
    public float shatterForce = 10f;
    private float[] randoms = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f };
    bool Shattered = false;

 
    public void ShatterObj(Transform context)
    {
        if (Shattered == true)
            return;

        Shattered = true;
        foreach (BoxCollider box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = false;
        }
        Debug.Log("Handle Object Death called from " + context.name);
        //create our broken object and reparent it
        GameObject newObject = Instantiate(brokenPrefab, transform.position, transform.rotation);

        int index = 0;
        //iterate through children and apply a force
        foreach (Rigidbody rb in newObject.GetComponentsInChildren<Rigidbody>())
        {

            Vector3 force = (rb.transform.position - context.transform.position).normalized * shatterForce;

            float rand = Random.Range(0, 1);

            if (UseAdditionalUpForce == true)
                rb.velocity = Vector3.up * (shatterForce / 10.0f) * randoms[index];

            if (index == 9)
                index = 0;

            rb.AddForce(force);
            index++;
        }
       

   
        Destroy(gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Hammer")
            ShatterObj(other.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hammer")
            ShatterObj(collision.transform);
    }
    private void Awake()
    {
        //ShatterObj(transform);
    }
}