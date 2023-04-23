using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public Stats PlayerStats;


    public void AddStats(Stats NewStat)
    {
        PlayerStats.Health = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Health, NewStat.Health);
        PlayerStats.Speed = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Speed, NewStat.Speed);
        PlayerStats.JumpHeight = MathsUtils.IncreaseFloatByPercentage(PlayerStats.JumpHeight, NewStat.JumpHeight);
        PlayerStats.GrappleCooldown = MathsUtils.IncreaseFloatByPercentage(PlayerStats.GrappleCooldown, NewStat.GrappleCooldown);
        PlayerStats.AbilityCooldown = MathsUtils.IncreaseFloatByPercentage(PlayerStats.AbilityCooldown, NewStat.AbilityCooldown);
        PlayerStats.Strength = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Strength, NewStat.Strength);
        PlayerStats.Stamina = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Stamina, NewStat.Stamina);
    }

    public void RemoveStats(Stats NewStat)
    {
        PlayerStats.Health = MathsUtils.DecreaseFloatByPercentage(PlayerStats.Health, NewStat.Health);
        PlayerStats.Speed = MathsUtils.DecreaseFloatByPercentage(PlayerStats.Speed, NewStat.Speed);
        PlayerStats.JumpHeight = MathsUtils.DecreaseFloatByPercentage(PlayerStats.JumpHeight, NewStat.JumpHeight);
        PlayerStats.GrappleCooldown = MathsUtils.DecreaseFloatByPercentage(PlayerStats.GrappleCooldown, NewStat.GrappleCooldown);
        PlayerStats.AbilityCooldown = MathsUtils.DecreaseFloatByPercentage(PlayerStats.AbilityCooldown, NewStat.AbilityCooldown);
        PlayerStats.Strength = MathsUtils.DecreaseFloatByPercentage(PlayerStats.Strength, NewStat.Strength);
        PlayerStats.Stamina = MathsUtils.DecreaseFloatByPercentage(PlayerStats.Stamina, NewStat.Stamina);
    }
}


[System.Serializable]
public class Stats
{
    public float Health;
    public float Speed;
    public float JumpHeight;
    public float GrappleCooldown;
    public float AbilityCooldown;
    public float Strength;
    public float Stamina;
    public bool NO;
}


