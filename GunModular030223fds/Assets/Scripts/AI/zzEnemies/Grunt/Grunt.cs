using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grunt : Enemy
{
    public Gun AIGun;
    public void Update()
    {
        base.Update();
        if(cooldown != AIGun.FireRate)
            cooldown = AIGun.FireRate;
    }
}
