using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    private Vector3 _previousPosition;
    public Vector3 _velocity;
    private GameObject obj;
    public GameObject FollowPrefab; 
    private bool EnterPossible = true;
    private void Start()
    {
       GameObject obj= Instantiate(FollowPrefab);
        obj.GetComponent<FollowPlatform>().Platform = gameObject;
        _previousPosition = transform.position;
    }

    private void Update()
    {
        _velocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
        // obj.transform.GetChild(0).GetComponent<StarterAssets.PlayerController>()._verticalVelocity = 0;
        if(obj)
        obj.transform.position = transform.position;

    }
 
    // player script gets the Objects's velocity from here
    public Vector3 GetVelocity()
    {
        return _velocity;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("called");
        if (other.tag == "Player"&&EnterPossible)
        {
            obj = other.transform.parent.gameObject;
            obj.GetComponent<StarterAssets.PlayerController>()._OnPlatform = true;
            other.transform.parent.parent.SetParent(transform);
            EnterPossible = false;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           
            obj.GetComponent<StarterAssets.PlayerController>()._OnPlatform = false;
            obj = null;
            other.transform.parent.parent.SetParent(null);
            EnterPossible = true;
        }
    }
}
