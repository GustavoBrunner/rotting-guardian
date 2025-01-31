using Game.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Enemy
{
    public class EnemyHud : MonoBehaviour
    {
        [SerializeField] private Image HpBar;
        private void Awake()
        {
            //CombatEvents.onSendEnemyData.AddListener(UpdateHpBar);
        }
        public void UpdateHpBar(float hp, float hpMax)
        {
            Debug.Log($"Att enemy life bar {hp / hpMax}");
            this.HpBar.fillAmount = Mathf.Lerp(HpBar.fillAmount, hp / hpMax, .5f);
        }

    }
}