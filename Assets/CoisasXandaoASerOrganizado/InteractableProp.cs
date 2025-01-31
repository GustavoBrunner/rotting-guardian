using FMODUnity;
using Game.Controllers;
using Game.Objects;
using Game.Player;
using System.Collections;
using UnityEngine;

public class InteractableProp : Interactable, IInteractable
{
    private const string CUT_ANIM = "Cut_Anim";
    public Animator HudAnimator;
    public int goldAmountMin, goldAmountMax;
    public Transform mesh;

    public GameObject hitSmoke;
    public GameObject breakingParticle;

    [SerializeField]
    private EventReference GoldSFX;
    [SerializeField]
    private EventReference BreakSFX;


    public virtual void Start()
    {
        this.HudAnimator = GetComponentInChildren<Animator>();
        mesh = transform.Find("Modelos");

    }
    public new virtual void Interact()
    {
        GameObject hitvaseParticle = Instantiate(breakingParticle, transform.position, Quaternion.identity);
        hitvaseParticle.GetComponent<ParticleSystem>().Play();
        StartCoroutine(HideMeshPlaySound());
    }
    public IEnumerator HideMeshPlaySound()
    {
        this. mesh.gameObject.SetActive(false);
        this.HudAnimator.Play(CUT_ANIM);
        GameController.instance.Gold += Random.Range(goldAmountMin, goldAmountMax);
        RuntimeManager.PlayOneShot(BreakSFX);
        
        yield return new WaitForSeconds(.5f);
        RuntimeManager.PlayOneShot(GoldSFX);
        this.gameObject.SetActive(false);
    }
}
    