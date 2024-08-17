using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionType _type;

    private void Awake()
    {
        // Destroy this object if this isn't destroy.
        Destroy(this.gameObject, 20.0f); 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            return; 
        }
        Destroy(this.gameObject);
    }
}

public enum PotionType
{
    BIGGER,
    SMALLER
}