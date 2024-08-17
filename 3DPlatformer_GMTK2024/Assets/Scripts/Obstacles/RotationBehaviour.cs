using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    [SerializeField] bool rotateX;
    [SerializeField] bool rotateY;
    [SerializeField] bool rotateZ;

    [SerializeField] float rotationSpeed = 360;


    // Update is called once per frame
    void Update()
    {
        Vector3 rotationVector = new Vector3();

        if (rotateX)
            rotationVector.x += 1;
        if (rotateY)
            rotationVector.y += 1;
        if (rotateZ)
            rotationVector.z += 1;

        transform.Rotate(rotationVector, rotationSpeed * Time.deltaTime);
    }

}
