using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float LaunchForce = 25;
   

    private void OnTriggerEnter(Collider other)
    {
 
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ScaleSpring(Vector3.one));
            other.gameObject.GetComponent<StarterAssets.PlayerController>()._verticalVelocity = LaunchForce;
          
        }

    }
    private IEnumerator ScaleSpring(Vector3 NewScale)
    {
        float Iterator = 0;
        Vector3 OldScale = transform.localScale; 
        while (Iterator < 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale = Vector3.Lerp(OldScale, NewScale, Iterator / 0.1f);
            Iterator += Time.deltaTime;
        }
        if (NewScale == Vector3.one)
            StartCoroutine(ScaleSpring(new Vector3(1,0.2f,1)));
    }
}
