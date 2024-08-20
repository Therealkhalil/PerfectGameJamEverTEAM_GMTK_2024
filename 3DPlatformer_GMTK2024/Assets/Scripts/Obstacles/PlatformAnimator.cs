using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public bool loopable = true;
    public float duration = 3;

    private int currentWaypoint = 0;

    void Start()
    {
        StartCoroutine(LerpValue(transform.position, waypoints[currentWaypoint].position));
    }

    private IEnumerator LerpValue(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;

        while(timeElapsed<duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;

        }
        transform.position = end;
        NextWaypoint();
    }


    private void NextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Count && loopable)
            currentWaypoint = 0;
        StartCoroutine(LerpValue(transform.position, waypoints[currentWaypoint].position));

        if (transform.CompareTag("Shark"))
        {
            transform.LookAt(waypoints[currentWaypoint].position);
            transform.Rotate(-90,0,0);
        }
    }
}
