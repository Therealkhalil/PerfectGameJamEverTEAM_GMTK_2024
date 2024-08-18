using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHover : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOrigin = transform.position;
    }

    private void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
