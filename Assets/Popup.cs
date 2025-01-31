using Game.Player;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private Vector3 RandommizeItensity = new Vector3(0.2f, 0f, 0f);

    void Start()
    {
        Destroy(this.gameObject, 2f);
        //transform.localPosition += new Vector3(Random.Range(-RandommizeItensity.x, RandommizeItensity.x), Random.Range(-RandommizeItensity.y, RandommizeItensity.y), Random.Range(-RandommizeItensity.z, RandommizeItensity.z));

    }

    private void FixedUpdate()
    {
        this.transform.position += new Vector3(0, 0.01f, 0f) ;
        
    }
}
