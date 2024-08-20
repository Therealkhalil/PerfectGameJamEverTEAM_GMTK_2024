using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// This Class should be attached to the potion object, it is responsible for ensuring that the object follows the outlined parabola
/// </summary>
public class PotionThrow : MonoBehaviour
{
    bool BandAidDone = false;
    public LayerMask WhatIsGround;
    private float TotalTime = 2, CurrentTime = 0, Height;
    Vector3 StartPoint, EndPoint;
    public float DetectionHeight;

    private SphereCollider _sphereCollider;
    private Rigidbody _rigidbody;

    public FMODUnity.EventReference growSound;
    public FMODUnity.EventReference shrinkSound;

    private void Update()
    {
        RepositionObject();
    }

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        
        DetectionHeight = 2;

        //May have to define ground manually*
        //WhatIsGround = GetComponent<QueenBomb>().WhatIsGround;
    }

    public void SetValues(Vector3 Start, Vector3 End, float Heightt, float Time)
    {
        StartPoint = Start;
        EndPoint = End;
        Height = Heightt;
        TotalTime += (Time / 10.0f) * (TotalTime / 5);
    }

    public void RepositionObject()
    {
        CurrentTime += Time.deltaTime;
        //
        if (Physics.Raycast(_sphereCollider.bounds.center, Vector3.down, DetectionHeight, WhatIsGround) == true)
        {
            _rigidbody.isKinematic = false;
            if (BandAidDone == false)
            {
                _rigidbody.AddForce(Vector3.down * 10, ForceMode.Impulse);
            }
            return;
        }
        transform.SetPositionAndRotation(SampleParabola(StartPoint, EndPoint, Height, CurrentTime / TotalTime), Quaternion.identity);
    }

    /// <summary>
    /// This function spawns the gizmos that will show the predictive path of a projectile, does not consider ricochets.
    /// </summary>
    /// <param name="start">The Spawn Point For the Object</param>
    /// <param name="end">The End Point (usually given through a Raycast)</param>
    /// <param name="height">How high from the ground the spawnpoint is</param>
    /// <param name="t">Length of time travel should take</param>
    /// <returns>A Parabola that should be given to the following: Gizmos.DrawLine(lastVec, vec);</returns>
    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
            return result;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //This is where we trigger the explosion VFX and output a signal to our environments

        //DO Explosions VFX
        //Send out a Signal to observers
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_sphereCollider.bounds.center, _sphereCollider.bounds.center - new Vector3(0.0f, DetectionHeight, 0.0f));
    }
}
