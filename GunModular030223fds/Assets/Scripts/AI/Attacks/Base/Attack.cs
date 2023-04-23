using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ScriptableObject
{
    public float Cooldown;
    public float AttackDistance;
    public AnimationParams AnimationSetting;
    public virtual void Execute(Enemy en)
    {
        en.ResetCooldown();
    }

}


public enum AnimationParams { AfterAnim, DuringAnim, BeforeAnim }