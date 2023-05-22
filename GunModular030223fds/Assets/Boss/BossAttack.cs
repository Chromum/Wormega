using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wormega/Attacks/BossAttack")]
public class BossAttack : Attack
{
    public List<AttackAndCooldown> f;
    public BossWave WaveOne;
    public BossWave WaveTwo;
    public BossWave WaveThree;

    public override void Execute(Enemy en)
    {
        BossAI ai = (BossAI)en;
        AttackAndCooldown t = null;
        bool attackNotFind = false;
        while (!attackNotFind)
        {
            if(ai.wave == 1)
            {
                AttackAndCooldown ta = WaveOne.bossAttacks[Random.Range(0, WaveOne.bossAttacks.Count)];
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
                AttackAndCooldown ta = WaveTwo.bossAttacks[Random.Range(0, WaveOne.bossAttacks.Count)];
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
                AttackAndCooldown ta = WaveThree.bossAttacks[Random.Range(0, WaveOne.bossAttacks.Count)];
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

