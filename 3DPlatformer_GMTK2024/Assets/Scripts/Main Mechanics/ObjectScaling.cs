using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* SUMMARY */
// This script scales an object based on a custom interpolation function.

/* HOW TO USE */
// 1. Attach this script to the GameObject you want to scale.
// 2. Set the scaleSpeed and scaleValue variables in the Inspector for designing test.
// 3. Optionally, enable or disable scaling in the X, Y, and Z directions. or you can add it when you throw a potion.
// 4. Run the game and observe the object scaling based on the set values.
public class ObjectScaling : MonoBehaviour
{
    // Variables for scaling the object
    [SerializeField] private float scaleSpeed = 0.5f;
    [Range(-5, 5)] public float scaleValue = 0f;
    public bool isScaling = false;


    // Check if the object can scale in the X, Y, and Z directions
    [SerializeField] private bool canScaleX;
    [SerializeField] private bool canScaleY;
    [SerializeField] private bool canScaleZ;

    // Initial and target scales
    private Vector3 initialScale;
    private Vector3 targetScale;

    // Store the previous scale value to detect changes
    private float previousScaleValue;

    private Coroutine scalingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        previousScaleValue = scaleValue;

        CalculateTargetScale();

        // Start the scaling coroutine
        scalingCoroutine = StartCoroutine(ScaleObject());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the scaleValue has changed
        if (Mathf.Abs(scaleValue - previousScaleValue) > Mathf.Epsilon)
        {
            CalculateTargetScale();

            // Restart the scaling coroutine
            if (scalingCoroutine != null)
            {
                isScaling = false;
                StopCoroutine(scalingCoroutine);
            }
            scalingCoroutine = StartCoroutine(ScaleObject());
        }

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

    private IEnumerator ScaleObject()
    {
        float timer = 0f;
        isScaling = true;

        while (timer < 1f)
        {
            // Increase the timer value based on the scaleSpeed
            timer += Time.deltaTime * scaleSpeed;

            // Clamp the timer to the range [0, 1]
            timer = Mathf.Clamp01(timer);

            // Apply the custom interpolation function to the timer
            float t = timer * timer * (3f - 2f * timer);

            // Lerp the scale based on the timer value
            Vector3 newScale = new Vector3(
                Mathf.Lerp(initialScale.x, targetScale.x * initialScale.x, t),
                Mathf.Lerp(initialScale.y, targetScale.y * initialScale.y, t),
                Mathf.Lerp(initialScale.z, targetScale.z * initialScale.z, t)
            );

            // Apply the new scale to the object
            transform.localScale = newScale;

            yield return null; // Wait until the next frame before continuing the loop
        }
        isScaling = false;

        // Once scaling is complete, update the initial scale
        initialScale = transform.localScale;
    }
}
