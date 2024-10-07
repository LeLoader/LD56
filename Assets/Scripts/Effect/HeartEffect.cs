using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartEffect : Effect
{
    public bool normalHeart = true;
    public bool positive = true;
    public bool player = true;
    public int amount;

    public override void ApplyEffect()
    {
        base.ApplyEffect();
    }

    public void ApplyEffect(Player player, Enemy[] enemies)
    {
        
        List<Character> targets = new();

        if (!positive)
        {
            amount = -amount;
        }

        if (this.player)
        {
            targets.Add(player);
        }
        else
        {
            targets = enemies.ToList<Character>();
        }

        if (normalHeart)
        {
            foreach (Character target in targets)
            {
                target.AddLife(amount);
            }
        }
        else
        {
            foreach (Character target in targets)
            {
                target.AddBonusLife(amount);
            }
        }
       
        ApplyEffect();
    }
}
