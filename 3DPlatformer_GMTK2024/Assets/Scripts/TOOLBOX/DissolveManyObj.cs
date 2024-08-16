using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManyObj : MonoBehaviour
{
    public List<Material> Materials;
    public MeshRenderer[] MeshRenderers;

    private void Awake()
    {
        MeshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (MeshRenderers != null)
        for (int i = 0; i < MeshRenderers.Length; i++)
        {
            for (int j = 0; j < MeshRenderers[i].materials.Length; j++)
                Materials.Add(MeshRenderers[i].materials[j]);
        }
    }

    private void Health_OnObjectDeath(Transform obj)
    {
        StartCoroutine(DissolveMeshEffect());
    }

    IEnumerator DissolveMeshEffect()
    {
        if (GetComponent<BoxCollider>())
            GetComponent<BoxCollider>().enabled = false;

        if (Materials.Count > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += 0.0075f;
                for (int i = 0; i < Materials.Count; i++)
                {
                    Materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(0.025f);
            }
        }
    }
}
