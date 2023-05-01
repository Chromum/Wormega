using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Gun PlayerGun;
    public SecondAblityActivator Activator;
    public RayInteractor Interactor;
    public InventoryUIManager InventoryUIManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerGun.Countdown.Finished)
        {
            if(PlayerGun.Barrel.barrelType == BarrelType.FullyAuto)
            {
                if (Input.GetMouseButton(0))
                    PlayerGun.TryShoot(PlayerGun.fireTar.forward);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                    PlayerGun.TryShoot(PlayerGun.fireTar.forward);
            }
            
        }
       
        if (Input.GetKeyDown(KeyCode.R))
            PlayerGun.Reload();
        if (Input.GetKeyDown(KeyCode.Q) && Activator.Countdown.HasFinished() && !Activator.abilityManager.Active)
        {
            Activator.Check = true;
            Activator.abilityManager.StartAbility();
        }
            if(Input.GetKeyDown(InventoryUIManager.ActivateButton))
            InventoryUIManager.TurnOnUI();
        else if (Input.GetKeyUp(InventoryUIManager.ActivateButton))
            InventoryUIManager.TurnOffUI();
    }
}
