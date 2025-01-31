using FMODUnity;
using Game.Combat;
using Game.Controllers;
using Game.Data;
using Game.Data.Attributes;
using Game.Ui.Hud;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Player
{
    public class PlayerDataController : MonoBehaviour
    {
        public static bool _isHoldingWeapon { get; private set; }
        
        public PlayerData Data;

        public PlayerAttributes PlayerAttributes;

        private WeaponDto primaryWeapon = null;
        private WeaponDto secondaryWeapon = null;
        private Dictionary<ItemType, WeaponDto> equipedItems = new();

        [SerializeField]
        private EventReference takeDamageSFX;



        private void OnDestroy()
        {
            UnlinkDelegates();
        }
        private void Start()
        {
            StartDelegates();
        }
        private void RestoreHp(int hp)
        {
            PlayerAttributes.Hp += hp;
            if(PlayerAttributes.Hp > PlayerAttributes.MaxHp)
                PlayerAttributes.Hp = PlayerAttributes.MaxHp;
            CombatEvents.onSendPlayerData.Invoke(-hp);
        }
        private void ResetPlayerAttr(WeaponDto dto)
        {
            switch (dto.ItemType)
            {
                case ItemType.mainWeapon:
                    PlayerAttributes.Damage -= dto.ItemEffect;
                    PlayerAttributes.CriticalChance -= dto.CritChance;
                    PlayerAttributes.CriticalPercent -= dto.CritPercent;
                    break;
                case ItemType.armor:
                    PlayerAttributes.Defense -= dto.ItemEffect;
                    break;
                case ItemType.secondaryWeapon:
                    if (dto.Name.ToLower().Contains("shield"))
                        PlayerAttributes.Defense -= dto.ItemEffect;
                    else
                        PlayerAttributes.Damage -= dto.ItemEffect;
                    break;
                default:
                    break;
            }
        }
        private void ResetCurrentWeaponStatus()
        {
            ResetPlayerAttr(primaryWeapon);
        }
        private void UpdatePlayerAttr(WeaponDto dto)
        {
            if(equipedItems.ContainsKey(dto.ItemType))
            {
                ResetPlayerAttr(equipedItems[dto.ItemType]);
                equipedItems.Remove(dto.ItemType);
            }

            switch (dto.ItemType)
            {
                case ItemType.mainWeapon:
                    PlayerAttributes.Damage += dto.ItemEffect;
                    PlayerAttributes.CriticalChance += dto.CritChance;
                    PlayerAttributes.CriticalPercent += dto.CritPercent;
                    primaryWeapon = dto;
                    _isHoldingWeapon = true;
                    break;
                case ItemType.armor:
                    PlayerAttributes.Defense += dto.ItemEffect;
                    break;
                case ItemType.secondaryWeapon:
                    if (dto.Name.ToLower().Contains("shield"))
                        PlayerAttributes.Defense += dto.ItemEffect;
                    else
                        PlayerAttributes.Damage += dto.ItemEffect;

                    secondaryWeapon = dto;
                    break;
                default:
                    break;
            }
            equipedItems.Add(dto.ItemType, dto);
        }
        private void ImprovePlayerAttr(TransactionDto transaction)
        {
            Debug.LogWarning("Transaction finished!");
            switch (transaction.Type)
            {
                case TransactionType.none:
                    Debug.LogError("None type passed!");
                    break;
                case TransactionType.hp:
                    PlayerAttributes.MaxHp += (int) transaction.Improvement;
                    HudDelegates.UpdateLife(PlayerAttributes.MaxHp);
                    break;
                case TransactionType.damage:
                    PlayerAttributes.Damage += (int)transaction.Improvement;
                    break;
                case TransactionType.crit:
                    PlayerAttributes.CriticalChance += transaction.Improvement;
                    break;
                case TransactionType.defense:
                    PlayerAttributes.Defense += transaction.Improvement;
                    break;
                default:
                    break;
            }
        }
        public void TakeDamage(int damage)
        {
            RuntimeManager.PlayOneShot(takeDamageSFX);
            PlayerAttributes.Hp -= damage;
            CombatEvents.onSendPlayerData.Invoke(damage);
            if (PlayerAttributes.Hp <= 0)
            {
                GameControllerDelegates.PlayerDied.Invoke();
                ResetLife();
            }
            
        }
        private void ResetLife()
        {
            PlayerAttributes.Hp = PlayerAttributes.MaxHp;
            CombatEvents.onResetLifeBar.Invoke(PlayerAttributes.Hp);
        }
        private void StartDelegates()
        {
            PlayerDelegates.HealHp += RestoreHp;
            PlayerDelegates.ResetWeaponStatus += ResetCurrentWeaponStatus;
            PlayerDelegates.UpdateWeapon += UpdatePlayerAttr;
            InventoryManager.Delegates.ResetSlot += ResetPlayerAttr;
            AttrImprovementDelegates.ImproveAttribute += ImprovePlayerAttr;
        }
        private void UnlinkDelegates()
        {
            PlayerDelegates.HealHp -= RestoreHp;
            PlayerDelegates.ResetWeaponStatus -= ResetCurrentWeaponStatus;
            PlayerDelegates.UpdateWeapon -= UpdatePlayerAttr;
            InventoryManager.Delegates.ResetSlot -= ResetPlayerAttr;
            AttrImprovementDelegates.ImproveAttribute -= ImprovePlayerAttr;
        }
    }
}