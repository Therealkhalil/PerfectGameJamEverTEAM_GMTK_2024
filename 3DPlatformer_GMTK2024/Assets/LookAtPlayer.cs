using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform;

    void Update()
    {
        transform.LookAt(playerTransform);

        if (CompareTag("GOAL"))
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
