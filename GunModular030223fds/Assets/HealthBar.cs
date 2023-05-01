using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthBarSlider;
    
    public Transform player;

    public Damageable Damageable;
    public bool TwoD = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerManager>().transform;
        HealthBarSlider.maxValue = Damageable.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarSlider.value = Damageable.Health;
        if (!TwoD)
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
            Vector3 rotation = lookRotation.eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
       
    }
}
