using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject HammerObject;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SwingHammer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SwingHammer()
    {
        float Iterator = 0;
        while (Iterator < 1f)
        {
            yield return new WaitForEndOfFrame();
            HammerObject.transform.localRotation =Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -180),Iterator);
            Iterator += Time.deltaTime;
        }
    }
}
