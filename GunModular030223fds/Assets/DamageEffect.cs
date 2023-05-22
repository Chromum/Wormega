using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{

    public Damageable damageable;
    public Image image;
    public float maxTransparency;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPrec = damageable.Health / damageable.MaxHealth;
        Color c = image.color;
        c.a = (1 - healthPrec) * maxTransparency;
        image.color = c;
    }
}
