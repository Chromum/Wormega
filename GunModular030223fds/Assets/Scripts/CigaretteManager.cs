using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class CigaretteManager : AbilityManager
{
    public bool Active;


    public float Contrast;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color ColorFilter;
    public float Saturation;

    public float Intensity;
    public float x;
    public float y;
    public float scale;

    public float Speed;

    public Camera cam;
    public Volume m_Volume;
    private Color og;


    public Animator Anima;

    public Countdown Countdown;

    public float desiredTimeScale;
    private float fixedDeltaTime;



    LensDistortion lens;
    ColorAdjustments colorAdj;
    private bool hasSet;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        m_Volume = cam.GetComponent<Volume>();
        m_Volume.sharedProfile.TryGet<LensDistortion>(out lens);
        m_Volume.sharedProfile.TryGet<ColorAdjustments>(out colorAdj);

        lens.intensity.value = 0f;
        lens.xMultiplier.value = x;
        lens.yMultiplier.value = y;
        lens.scale.value = scale;
        colorAdj.contrast.value = 0f;
        colorAdj.colorFilter.value = new Color(1f, 1f, 1f);
        colorAdj.saturation.value = 0f;
        this.fixedDeltaTime = Time.fixedDeltaTime;


    }

    // Update is called once per frame
    void Update()
    {
        if(Active)
        {
            lens.intensity.value = Mathf.Lerp(lens.intensity.value, Intensity, Time.deltaTime);
            lens.xMultiplier.value = Mathf.Lerp(lens.xMultiplier.value, x, Time.deltaTime);
            lens.yMultiplier.value = Mathf.Lerp(lens.yMultiplier.value, y, Time.deltaTime);
            lens.scale.value = Mathf.Lerp(lens.scale.value, scale, Time.deltaTime);

            colorAdj.contrast.value = Mathf.Lerp(colorAdj.contrast.value, Contrast, Time.deltaTime);
            colorAdj.colorFilter.value = Color.Lerp(colorAdj.colorFilter.value, ColorFilter, Time.deltaTime);
            colorAdj.saturation.value = Mathf.Lerp(colorAdj.saturation.value, Saturation, Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeScale, Time.deltaTime);
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        else
        {
            lens.intensity.value = Mathf.Lerp(lens.intensity.value, 0, Time.deltaTime);
            lens.xMultiplier.value = Mathf.Lerp(lens.xMultiplier.value, x, Time.deltaTime);
            lens.yMultiplier.value = Mathf.Lerp(lens.yMultiplier.value, y, Time.deltaTime);
            lens.scale.value = Mathf.Lerp(lens.scale.value, scale, Time.deltaTime);

            colorAdj.contrast.value = Mathf.Lerp(colorAdj.contrast.value, 0, Time.deltaTime);
            colorAdj.colorFilter.value = Color.Lerp(colorAdj.colorFilter.value, new Color(1f, 1f, 1f), Time.deltaTime);
            colorAdj.saturation.value = Mathf.Lerp(colorAdj.saturation.value, 0, Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1 , Time.deltaTime);
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }

        if(Active && Countdown.HasFinished() && !hasSet)
        {
           StopAbility();
        }


        Countdown.CountdownUpdate();
    }

    public override void EnableEffect()
    {
        base.EnableEffect();
        Countdown.StartCountdown();
        Active = true;
        lens.active = true;
        colorAdj.active = true;
        og = new Color(1f, 1f, 1f);
        gun.SetActive(true);
    }

    public override void StartAbility()
    {
        base.StartAbility();
        Anima.SetTrigger("ZaZa");
        hasSet = false;
        gun.SetActive(false);
    }

    public override void StopAbility()
    {
        Active = false;
        abOver?.Invoke();
        hasSet = true;
    }

}

public static class AudioUtils
{
    public static void PlaySoundWithPitch(AudioSource audioSource, AudioClip audioClip, float desiredPitch) {
        float pitch = desiredPitch * Time.timeScale; 
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip);
    }
}