using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Gun/Barrel")]
public class Barrel : GunPart
{ 
    public ShotMode ShotMode;
    public BarrelType barrelType;

    public virtual void Shoot(int Damage)
    {
        
    }
}


public enum ShotMode {SingleShot,DoubleShot,TrippleShot}
public enum BarrelType {SemiAuto,FullyAuto,SniperBarrel}