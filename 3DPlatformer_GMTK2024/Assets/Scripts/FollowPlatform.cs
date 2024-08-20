using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    public GameObject Platform; 
    // Start is called before the first frame update
    void Start()
    {
        if(Platform == null)
        {
            Debug.LogError("Platform is not assigned");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Platform != null)
        {
            transform.position = Platform.transform.position;
        }
    }
    private void OnValidate()
    {
        if (Platform != null)
        {
            transform.position = Platform.transform.position;
        }
    }
}
