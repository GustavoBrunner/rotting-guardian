using Game.Player;
using FMODUnity;
using Game.Controllers;
using Game.Objects;
using System.Collections;
using UnityEngine;
public class Barrel : Box
{
    public EventReference coins;
    public override void Reward()
    {
        RuntimeManager.PlayOneShot(coins);
        GameController.instance.Gold += Random.Range(goldAmountMin, goldAmountMax);
        mesh.gameObject.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        _this.SetActive(false);
    }
}