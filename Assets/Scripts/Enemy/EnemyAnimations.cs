

using UnityEngine;

public class EnemyAnimations 
{
    private const string HIT_ANIMATION = "Hitted_Enemy1_Anim";
    private const string ATTACK_ANIMATION = "Attack_Enemy1_Anim";
    private const string DEATH_ANIMATION = "Death_Enemy1_Anim";
    private readonly Animator _animator;

    AnimatorClipInfo[] clipInfos;

    public bool isAnimationCooldown {  get; private set; }

    public EnemyAnimations(Animator animator)
    {
        _animator = animator;
    }
    private AnimatorClipInfo[] GetCurrentAnimationClip()
    {
        clipInfos = _animator.GetCurrentAnimatorClipInfo(0);
        return clipInfos;
    }
    public AnimatorStateInfo GetCurrentAnimatorState()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }
    public void PlayHitAnim()
    {
        if (GetCurrentAnimatorState().IsName(HIT_ANIMATION))
        {
            return;
        }
        this._animator.Play(HIT_ANIMATION);
    }

    public void PlayAttackAnim()
    {
        this._animator.Play(ATTACK_ANIMATION);
    }

    public void PlayDeathAnim()
    {
        this._animator.Play(DEATH_ANIMATION);
    }
}
