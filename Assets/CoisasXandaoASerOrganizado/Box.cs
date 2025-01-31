using Game.Player;
using FMODUnity;
using Game.Controllers;
using Game.Objects;
using System.Collections;
using UnityEngine;

public class Box : InteractableProp
{
    private const string CUT_ANIM = "Cut_Anim";
    public GameObject item;
    int durability = 1;

    public GameObject _this;
    
    public EventReference HitInTheBoxSFX01;
    public EventReference HitInTheBoxSFX02;

    public EventReference WodenBreakingSFX;

    private PlayerDataController _playerDataController;
    public override void Start()
    {
       base.Start();
        _playerDataController = FindObjectOfType<PlayerDataController>();
    }
    public override void Interact()
    {
        _playerDataController = FindObjectOfType<PlayerDataController>();
        if (PlayerDataController._isHoldingWeapon)
        {
            PopUpDelegates.CreatePopup($"{_playerDataController.PlayerAttributes.Damage}");
            GameObject hitboxParticle = Instantiate(hitSmoke, transform.position, Quaternion.identity);
            hitboxParticle.GetComponent<ParticleSystem>().Play();
            HudAnimator.Play(CUT_ANIM);
            RuntimeManager.PlayOneShot(HitInTheBoxSFX02);
            durability--;
            if (durability < 0) 
            {
                GameObject breakingboxParticle = Instantiate(breakingParticle, transform.position, Quaternion.identity);
                hitboxParticle.GetComponent<ParticleSystem>().Play();
                RuntimeManager.PlayOneShot(WodenBreakingSFX);
                Reward();
            }
        }
        else
        {
            PopUpDelegates.CreatePopup("0");
            GameObject hitboxParticle = Instantiate(hitSmoke, transform.position, Quaternion.identity);
            hitboxParticle.GetComponent<ParticleSystem>().Play();
            RuntimeManager.PlayOneShot(HitInTheBoxSFX01);
        }
    }

    public virtual void Reward()
    {
        mesh.gameObject.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Instantiate(item, transform.position, Quaternion.identity);
        _this.SetActive(false);
    }
}
