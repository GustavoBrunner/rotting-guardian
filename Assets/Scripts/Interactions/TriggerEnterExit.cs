using UnityEngine;
using UnityEngine.Events;

namespace Monu.Core
{
    public class TriggerEnterExit : MonoBehaviour
    {
        private string m_Name = "Player";

        public UnityEvent<Collider> OnTriggerEnterEvent;
        public UnityEvent<Collider> OnTriggerExitEvent;

        private void OnTriggerEnter(Collider collider)
        {
            if (string.IsNullOrEmpty(m_Name) || collider.name == m_Name)
            {
                OnTriggerEnterEvent?.Invoke(collider);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (string.IsNullOrEmpty(m_Name) || collider.name == m_Name)
            {
                OnTriggerExitEvent?.Invoke(collider);
            }
        }
    }
}