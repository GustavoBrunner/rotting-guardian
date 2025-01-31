using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker 
{
    
    public bool CheckIfHit(float defense)
    {
        var prob = Random.Range(0, 100);


        return prob > defense;
    }

    public bool CheckIfHitCritical(float critChance)
    {
        var prob = Random.Range(0, 100);

        return prob < critChance;
    }
}
