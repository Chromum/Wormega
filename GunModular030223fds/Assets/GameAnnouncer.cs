using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnnouncer : MonoBehaviour
{
    public AudioSource Announcer;

    public KillTracker KillTracker;

    public AudioClip WaveOne;

    public AudioClip WaveTwo;

    public AudioClip WaveThree;

    public AudioClip FinalWave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Wave(int i,bool finalWave)
    {
        switch (i)
        {
            case 0:
                Announcer.PlayOneShot(WaveOne);
                break;
            case 1:
                Announcer.PlayOneShot(WaveTwo);
                break;
            case 2:
                Announcer.PlayOneShot(WaveThree);
                break;
        }

        if (finalWave)
        {
            StartCoroutine(PlayFinalWave());
        }
            
    }
    
    private IEnumerator PlayFinalWave()
    {
        while (Announcer.isPlaying)
        {
            yield return null;
        }
        Announcer.PlayOneShot(FinalWave);
    }

}
[System.Serializable]
public class KillTracker
{
    private int numKills = 0;
    private float timeOfLastKill = -1f;
    private const float KILL_TIME_WINDOW = 2f; // The time window in which kills count as a multi-kill

    public AudioClip doubleKill;
    public AudioClip trippleKill;
    public AudioClip quadrupileKill;
    public AudioClip RampageKill;

    
    public void EnemyDied(AudioSource Au)
    {
        float currentTime = Time.time;

        if (timeOfLastKill < 0f || currentTime - timeOfLastKill <= KILL_TIME_WINDOW)
        {
            numKills++;
            timeOfLastKill = currentTime;

            switch (numKills)
            {
                case 2:
                    Au.PlayOneShot(doubleKill);
                    break;
                case 3:
                    Au.PlayOneShot(trippleKill);
                    break;
                case 4:
                    Au.PlayOneShot(quadrupileKill);
                    break;
                case 5:
                    Au.PlayOneShot(RampageKill);
                    break;
            }
        }
        else
        {
            numKills = 1;
            timeOfLastKill = currentTime;
        }
    }
}