using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SUMMARY */
// This script scales an object based on a custom interpolation function.

  /* HOW TO USE */
  // 1. Attach this script to the GameObject you want to scale.
  // 2. Set the scaleSpeed and scaleValue variables in the Inspector for desiging test.
  // 3. Optionally, enable or disable scaling in the X, Y, and Z directions. or you can add it when you throw a potion.
  // 4. Run the game and observe the object scaling based on the set values.
public class ObjectScaling : MonoBehaviour
{
    // Variables for scaling the object
    [SerializeField] private float scaleSpeed = 0.1f;
    [Range(1, 5)][SerializeField] private float scaleValue = 0.5f;

    // Check if the object can scale in the X, Y, and Z directions
    [SerializeField] private bool canScaleX;
    [SerializeField] private bool canScaleY;
    [SerializeField] private bool canScaleZ;

    // Initial and target scales
    private Vector3 initialScale;
    private Vector3 targetScale;

    // A timer to track the scaling progress
    private float timer;

    // Store the previous scale value to detect changes
    private float previousScaleValue;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        previousScaleValue = scaleValue;

        CalculateTargetScale();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the scaleValue has changed
        if (Mathf.Abs(scaleValue - previousScaleValue) > Mathf.Epsilon)
        {
            CalculateTargetScale();
            timer = 0f; // Reset the timer to start scaling again
        }

        ScaleObject();

        // Update the previous scale value
        previousScaleValue = scaleValue;
    }

    private void CalculateTargetScale()
    {
        // Calculate the new target scale based on the current scale value and axes
        targetScale = new Vector3(
            canScaleX ? scaleValue : transform.localScale.x / initialScale.x,
            canScaleY ? scaleValue : transform.localScale.y / initialScale.y,
            canScaleZ ? scaleValue : transform.localScale.z / initialScale.z
        );
    }

    private void ScaleObject()
    {
        // Increase the timer value based on the scaleSpeed
        timer += Time.deltaTime * scaleSpeed;

        // Clamp the timer to the range [0, 1]
        timer = Mathf.Clamp01(timer);

        // Apply the custom interpolation function to the timer
        float t = timer * timer * (3f - 2f * timer);

        // Lerp the scale based on the timer value
        Vector3 newScale = new Vector3(
            Mathf.Lerp(transform.localScale.x, targetScale.x * initialScale.x, t),
            Mathf.Lerp(transform.localScale.y, targetScale.y * initialScale.y, t),
            Mathf.Lerp(transform.localScale.z, targetScale.z * initialScale.z, t)
        );

        // Apply the new scale to the object
        transform.localScale = newScale;

        // Optional: Reset the timer and target scale when the object reaches the target size
        if (timer >= 1f)
        {
            timer = 0f;
            initialScale = transform.localScale;
        }
    }
}
