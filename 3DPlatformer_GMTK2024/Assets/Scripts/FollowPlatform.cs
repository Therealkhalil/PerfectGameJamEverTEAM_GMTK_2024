using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    public GameObject Platform; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Platform.transform.position;
    }
    private void OnValidate()
    {
        transform.position = Platform.transform.position;
    }
}
