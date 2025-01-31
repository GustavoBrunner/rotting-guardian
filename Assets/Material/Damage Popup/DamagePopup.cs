using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float DestroyTime = 0.5f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 RandommizeItensity = new Vector3 (0.5f, 0f, 0f);

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += new Vector3(Random.Range(-RandommizeItensity.x, RandommizeItensity.x), Random.Range(-RandommizeItensity.y, RandommizeItensity.y), Random.Range(-RandommizeItensity.z, RandommizeItensity.z));
    }

}
