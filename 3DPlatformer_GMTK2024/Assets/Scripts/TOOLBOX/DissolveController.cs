using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public MeshRenderer MeshReg;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.02f;

    private Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        if (MeshReg != null)
            materials = MeshReg.materials;
    }
    public void StartDissolve()
    {
        StartCoroutine(DissolveCo());
    }
    IEnumerator DissolveCo()
    {
        if(materials.Length > 0)
        {
            float counter = 0;
            while(materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i=0; i<materials.Length; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
