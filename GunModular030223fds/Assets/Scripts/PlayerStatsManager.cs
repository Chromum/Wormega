using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.LowLevel;

public class PlayerStatsManager : MonoBehaviour
{
    public Stats PlayerStats;
    public Damageable Player;
    public Melee melee;
    public CigaretteManager cigarette;
    public TextMeshPro textMeshPro;
    public void Start()
    {
        GameManager.instance.GrappleForce = PlayerStats.GrappleCooldown * 100;
    }

    public void AddStats(Stats NewStat)
    {
        PlayerStats.Health = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Health, NewStat.Health);
        Player.MaxHealth = Player.Health * PlayerStats.Health;

        float multiplicationDifference = PlayerStats.Health - 1f;
        multiplicationDifference *= 10;
        Player.HealthTextValue = multiplicationDifference;
        Player.healthBar.HealthBarSlider.maxValue = Player.startingHealth + (multiplicationDifference * 10);

        // DEBUG LOG PROCESS
        // Display Health/Max Health
        // Display bonus amount of health as a percentage
        // Display bonus amount of health as a value
        // Add the value - display new Max Health, etc.
        // Test text update - check it works in text object




        PlayerStats.Speed = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Speed, NewStat.Speed);
        PlayerStats.JumpHeight = MathsUtils.IncreaseFloatByPercentage(PlayerStats.JumpHeight, NewStat.JumpHeight);
        PlayerStats.GrappleCooldown = MathsUtils.IncreaseFloatByPercentage(PlayerStats.GrappleCooldown, NewStat.GrappleCooldown);
        GameManager.instance.GrappleForce = PlayerStats.GrappleCooldown * 100;
        
        PlayerStats.AbilityCooldown = MathsUtils.IncreaseFloatByPercentage(PlayerStats.AbilityCooldown, NewStat.AbilityCooldown);
        cigarette.Countdown.Count = MathsUtils.DecreaseFloatByPercentage(cigarette.Countdown.Count, PlayerStats.AbilityCooldown);
        PlayerStats.Strength = MathsUtils.IncreaseFloatByPercentage(PlayerStats.Strength, NewStat.Strength);
        melee.Damage = melee.BaseDamage * PlayerStats.Strength;
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


