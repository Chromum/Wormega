using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wormega/Attacks/BossAttack")]
public class BossAttack : Attack
{
    public List<AttackAndCooldown> f;
    public List<AttackAndCooldown> WaveOne;
    public List<AttackAndCooldown> WaveTwo;
    public List<AttackAndCooldown> WaveThree;

    public override void Execute(Enemy en)
    {
        BossAI ai = (BossAI)en;
        AttackAndCooldown t = null;
        bool attackNotFind = false;
        while (!attackNotFind)
        {
            if(ai.wave == 1)
            {
                AttackAndCooldown ta = WaveOne[Random.Range(0, WaveOne.Count)];
                if (ta.countDown.HasFinished())
                {
                    t = ta;
                    ta.countDown.StartCountdown();
                    attackNotFind = true;
                    
                }
                else return;
            }
            if (ai.wave == 2)
            {
                AttackAndCooldown ta = WaveTwo[Random.Range(0, WaveTwo.Count)];
                if (ta.countDown.HasFinished())
                {
                    t = ta;
                    ta.countDown.StartCountdown();
                    attackNotFind = true;
                }
                else return;            
            }
            if (ai.wave == 3)
            {
                AttackAndCooldown ta = WaveThree[Random.Range(0, WaveThree.Count)];
                if (ta.countDown.HasFinished())
                {
                    t = ta;
                    ta.countDown.StartCountdown();
                    attackNotFind = true;
                }
                else return;            
            }
        }
        t.attack.Execute(en);
        en.b.CurrentState = ai.CurrentResetState;
        t = null;
    }
}

