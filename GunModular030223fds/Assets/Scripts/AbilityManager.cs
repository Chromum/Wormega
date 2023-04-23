using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Ability AbilitySO;

    public delegate void AbilityOver();

    public AbilityOver abOver;

    public GameObject gun;

    public virtual void StartAbility() { }
    public virtual void EnableEffect() { }
    public virtual void StopAbility() { }
}
