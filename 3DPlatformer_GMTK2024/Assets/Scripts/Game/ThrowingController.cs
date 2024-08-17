using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public GameObject biggerPotion;
    public GameObject smallerPotion;

    [Header("Settings")]
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    private StarterAssetsInputs _inputs;
    private bool _readyToThrow;

    private void Awake()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _readyToThrow = true;
        
    }

    private void Update()
    {
        // Right click to make thing bigger
        if(_inputs.throwBigger && _readyToThrow)
        {
            Throw(biggerPotion, attackPointRight.position);
            _inputs.throwBigger = false;
        }
        
        if(_inputs.throwSmaller && _readyToThrow)
        {
            Throw(smallerPotion, attackPointLeft.position);
            _inputs.throwSmaller = false;
        }
        
    }

    private void Throw(GameObject objectToThrow, Vector3 attackPoint)
    {
        _readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        
        // implement throwCooldown
        StartCoroutine(ResetThrow());
    }

    private IEnumerator ResetThrow()
    {
        yield return new WaitForSeconds(throwCooldown);
        _readyToThrow = true;
    }
}
