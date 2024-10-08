using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject HammerObject;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        HammerObject.transform.localRotation *= Quaternion.Euler(0, 0, 3);
    }
    public void SwingHammer()
    {
        StartCoroutine(SwingHammerRoutine());
    }
    private void OnCollisionEnter(Collision collision)
    {
        PotionObject potionObject;
            collision.gameObject.TryGetComponent<PotionObject>(out potionObject);

        if (potionObject != null && potionObject._type == PotionType.BIGGER)
            StartCoroutine(Grow());
    }
    private IEnumerator Grow()
    {
        float Iterator = 0;
        while(Iterator<0.5f)
        {
            yield return new WaitForEndOfFrame();
            Iterator += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2, Iterator / 0.5f);
        }
    }
    private IEnumerator SwingHammerRoutine()
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
