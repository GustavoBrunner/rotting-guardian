using Game.Combat;
using UnityEngine;
using UnityEngine.Events;

public class TrapApplyDamage : MonoBehaviour
{
    private string m_Name = "Player";

    [SerializeField] private FMODUnity.StudioEventEmitter applyDamage;

    private void OnTriggerEnter(Collider collider)
    {
        if (string.IsNullOrEmpty(m_Name) || collider.name == m_Name)
        {
            applyDamage.Play();
            collider.GetComponent<IDamageble>().TakeTrapDamage(10);
        }
    }
}
