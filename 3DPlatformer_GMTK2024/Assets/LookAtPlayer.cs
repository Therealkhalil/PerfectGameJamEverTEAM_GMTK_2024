using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform;

    void Update()
    {
        transform.LookAt(playerTransform);
    }
}
