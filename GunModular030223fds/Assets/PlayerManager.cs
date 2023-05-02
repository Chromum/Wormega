using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public MapSpriteSelector currentRoom;
    public GameObject ItemPickupUI;
    public Damageable playerDamagable;

    // Start is called before the first frame update
    void Start()
    {
        playerDamagable.Death += PlayerDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied(GameObject g)
    {

    }
}
