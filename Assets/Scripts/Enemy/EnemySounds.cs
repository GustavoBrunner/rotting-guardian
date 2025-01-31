
using Game.Audio;
using Game.Player;
using UnityEngine;

public class EnemySounds
{
    
    public void PlayHitSound(Vector3 pos)
    {
       AudioManager.Instance.PlayPlayerAttackSound(AudioEvents.Instance.PlayerAttackEvent, pos);
    }
    public void PlayMissSound(Vector3 pos) 
    {
        AudioManager.Instance.PlayMissedAttackSound(AudioEvents.Instance.MissedAttackEvent, pos);
    }
}
