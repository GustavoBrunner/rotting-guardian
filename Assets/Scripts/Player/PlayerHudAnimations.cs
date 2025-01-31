using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHudAnimations
    {
        public const string DAMAGE_ANIMATION = "Player_Damage_Ui_Anim";
        public const string ATTACK_ANIMATION = "Player_Attack_Anim";
        public const string IDLE_ANIMATION = "Player_Sprite_Anim";

        private readonly Animator animator;

        public bool isCooldown { get; private set; }

        public PlayerHudAnimations(Animator animator)
        {
            this.animator = animator;
            isCooldown = false;
        }

        public void PlayTakeDamageAnim()
        {
            //this.animator.Play(DAMAGE_ANIMATION);
            animator.SetTrigger("TakeDamageTrigger");
            Debug.Log("Damage animation playing");
            
        }
        public void PlayAttackAnim()
        {
            
            this.animator.Play(ATTACK_ANIMATION);
        }

        private AnimatorStateInfo GetCurrentAnimatorStateInfo()
        {
            return animator.GetCurrentAnimatorStateInfo(0);
        }
        private AnimatorClipInfo[] GetCurrentAnimationClipInfo()
        {
            return animator.GetCurrentAnimatorClipInfo(0);
        }

        
        public IEnumerator AnimationCooldown()
        {
            yield return new WaitForSeconds(0.6f);
            isCooldown = false;
        }

    }

}