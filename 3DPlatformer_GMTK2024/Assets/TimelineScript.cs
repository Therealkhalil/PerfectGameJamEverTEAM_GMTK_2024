using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineScript : MonoBehaviour
{
    [SerializeField] private Camera[] _camera;
    private bool isSwitching = false;
    private float timerScene = 0f;

    // Update is called once per frame
    void Update()
    {
        timerScene += Time.deltaTime;

        if(timerScene> 90)
        {
            StopCoroutine(SwitchCamera());
            _camera[0].gameObject.SetActive(false);
            _camera[1].gameObject.SetActive(false);
            _camera[2].gameObject.SetActive(true);
        }

        if (timerScene<=90)
        {
            if(isSwitching == false)
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        isSwitching = true;
        yield return new WaitForSeconds(10f);
        _camera[0].gameObject.SetActive(false);
        _camera[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        _camera[0].gameObject.SetActive(true);
        _camera[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        _camera[0].gameObject.SetActive(false);
        _camera[1].gameObject.SetActive(true);
        isSwitching = false; 
    }
}
