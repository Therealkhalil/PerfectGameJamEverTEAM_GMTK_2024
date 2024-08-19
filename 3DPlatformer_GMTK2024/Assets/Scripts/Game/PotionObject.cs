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
        if (other.gameObject.TryGetComponent<ObjectScaling>(out ObjectScaling obj))
        {
            Debug.Log("This is scalable object : " + _type);
            if (_type == PotionType.BIGGER)
            {
                obj.scaleValue += 2f;
            }
            else if (_type == PotionType.SMALLER)
            {
                obj.scaleValue -= 2f;
            }
            return;
        }
        /*Destroy(this.gameObject);*/
        Destroy(this.gameObject, 10.0f);
    }
}

public enum PotionType
{
    BIGGER,
    SMALLER
}