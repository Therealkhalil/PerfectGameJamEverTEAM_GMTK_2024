using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterObject : MonoBehaviour
{
    [SerializeField]
    private GameObject brokenPrefab;
    public bool UseAdditionalUpForce = false;
    public float shatterForce = 10f;
    bool Shattered = false;

    public void HandleObjectDeath(Transform context)
    {
        if (Shattered == true)
            return;

        Shattered = true;
        //Possibly don't need to check for this breakable tag :3
        foreach (BoxCollider box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = false;
        }
        foreach (BoxCollider box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = false;
        }
        Debug.Log("Handle Object Death called from " + context.name);

        //create our broken object and reparent it (pretty expensive tbh)
        GameObject newObject = Instantiate(brokenPrefab, transform.position, transform.rotation);

        int index = 0;

        //iterate through children and apply a force
        foreach (Rigidbody rb in newObject.GetComponentsInChildren<Rigidbody>())
        {

            //This would normally explode radially, but because the object transform and the context are not in the same position
            //it favors 1 direction
            Vector3 force = (rb.transform.position - context.transform.position).normalized * shatterForce;

            float rand = Random.Range(0, 1);

            if (UseAdditionalUpForce == true)
                rb.velocity = Vector3.up * (shatterForce / 10.0f) * rand;

            if (index == 9)
                index = 0;

            rb.AddForce(force);

            //This is for a shader that dissolves/destroys the children after a short amount of time
            //rb.gameObject.AddComponent<DissolveRock>();
            index++;
        }
        //TODO: Object pooling
        Destroy(gameObject);
    }
}
