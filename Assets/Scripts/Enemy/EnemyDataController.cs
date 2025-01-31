using FMODUnity;
using Game.Combat;
using Game.Controllers;
using Game.Data;
using Game.Data.Attributes;
using Game.Enemy;
using Game.TurnSystem;
using Game.Ui.Hud;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class EnemyDataController : MonoBehaviour
{
    //IGOR MEXEU AQUI :)
    
    
    
    public Vector3 RandommizeItensity = new(10f, 50f, 10f);

    [Tooltip("SFX gold.")]
    [SerializeField] private FMODUnity.StudioEventEmitter sfxCoins, sfxDamage, sfxDeath;

    public int MaxGold, MinGold;

    public EnemyData EnemyData;
    public EnemyAttributes Attributes;
    public EnemyController Controller;

    private TurnSystemController turnController;
    [field: SerializeField] public EnemyHud Hud { get; private set; }


    private void Awake()
    {
        this.Hud = this.GetComponentInChildren<EnemyHud>();   
        turnController = FindAnyObjectByType<TurnSystemController>();
    }
    private void Update()
    {
        CheckLife();
    }

    private void CheckLife()
    {
        if(this.Attributes.Hp <= 0)
        {
            StartCoroutine(MORTE_ANIM());
            this.Hud.UpdateHpBar(0, Attributes.MaxHp);

        }
    }
    private void ResetHp()
    {
        this.Attributes.Hp = 100;
        this.Hud.UpdateHpBar(this.Attributes.Hp, Attributes.MaxHp);
    }
    public void TakeDamage(int playerDamage, float playerCritical, bool isCrit = false)
    {
        
        sfxDamage.Play();
        var popUpTxt = string.Empty;

        if (isCrit)
        {
            var totalDamage = playerDamage + (int)(playerDamage * ((float)playerCritical / 100));
            this.Attributes.Hp -= totalDamage;
            popUpTxt = $"CRIT {totalDamage}";
        }
        else
        {
            this.Attributes.Hp -= playerDamage;
            if (Attributes.Hp > 0)
                popUpTxt = playerDamage.ToString();
        }
        PopUpDelegates.CreatePopup(popUpTxt);
        Debug.Log(this.gameObject.transform.rotation);
        this.Hud.UpdateHpBar(Attributes.Hp, Attributes.MaxHp);
    }

    //IGOR AQUI DE NOVO :)
    

    private void UpdateHud()
    {
        CombatEvents.onSendEnemyData.Invoke(this);
    }

    IEnumerator MORTE_ANIM()
    {
        Controller.PlayDeathAnimation();
        Controller.isDead = true;
        yield return new WaitForSeconds(1.75f);        
        GameController.instance.EnemyKilled.Add(this);
        GetComponent<IndividualTurnController>().FinishAction();
        gameObject.SetActive(false);
        ResetHp();
        ItemPoolDelegates.InstantiateItem.Invoke(this.transform.position.Vector3ToVector3Int());
        turnController.RemoveEnemy(GetComponent<EnemyController>());
        this.EnemyData.GoldQtd = Random.Range(MinGold, MaxGold);
        GameController.instance.Gold += this.EnemyData.GoldQtd;
        sfxCoins.Play();
    }
}
