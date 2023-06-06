using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Gun PlayerGun;
    public Transform MainCam;
    public SecondAblityActivator Activator;
    public RayInteractor Interactor;
    public InventoryUIManager InventoryUIManager;
    public Melee melee;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(PlayerGun.Countdown.Finished)
        {
            if(PlayerGun.Barrel.barrelType == BarrelType.FullyAuto)
            {
                if (Input.GetMouseButton(0))
                    PlayerGun.TryShoot(MainCam.forward);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                    PlayerGun.TryShoot(MainCam.forward);
            }
            
        }
       
        if (Input.GetKeyDown(KeyCode.R))
            PlayerGun.Reload();
        if (Input.GetKeyDown(KeyCode.Q) && Activator.Countdown.HasFinished() && !Activator.abilityManager.Active)
        {
            Activator.Check = true;
            Activator.abilityManager.StartAbility();
            Activator.slid.value = 0;
        }
            if(Input.GetKeyDown(InventoryUIManager.ActivateButton))
            InventoryUIManager.TurnOnUI();
        else if (Input.GetKeyUp(InventoryUIManager.ActivateButton))
            InventoryUIManager.TurnOffUI();


        if (Input.GetKeyDown(KeyCode.V))
            melee.Attack();
            
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position,Camera.main.transform.forward * 10);
    }
}
