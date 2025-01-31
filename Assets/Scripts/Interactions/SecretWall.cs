using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecretWall : MonoBehaviour
{

    private string m_Name = "Player";

    public BoxCollider m_BoxCollider;
    public bool m_EnterSide;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == m_Name)
        {
            if (m_EnterSide == true)
            {
                m_BoxCollider.enabled = false;
            }
            else
            {
                m_BoxCollider.enabled = true;
            }
        }
    }
}
