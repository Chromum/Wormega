using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using NaughtyAttributes;

public class GunRecoil : MonoBehaviour
{
    [SerializeField] private float recoilDuration = 0.2f;
    [SerializeField] private float recoilDistance = 0.1f;
    [SerializeField] private float recoilRotationAmount = 10f;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private bool isRecoiling;
    private float recoilTimer;

    private void Start()
    {
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
    }

    private void Update()
    {
        if (isRecoiling)
        {
            Recoil();
        }
    }

    public void Fire()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            recoilTimer = 0f;
        }
    }

    private void Recoil()
    {
        recoilTimer += Time.deltaTime;

        if (recoilTimer < recoilDuration)
        {
            float recoilProgress = recoilTimer / recoilDuration;

            // Interpolate the gun's position from its starting position to its recoil position
            Vector3 recoilPosition = Vector3.Lerp(startingPosition, startingPosition - transform.up * recoilDistance, recoilProgress);
            transform.localPosition = recoilPosition;

            // Interpolate the gun's rotation from its starting rotation to its rotated recoil rotation
            Quaternion recoilRotation = Quaternion.Euler(-recoilRotationAmount, 0f, 0f);
            Quaternion recoilRotationTarget = startingRotation * recoilRotation;
            Quaternion finalRotation = Quaternion.Slerp(startingRotation, recoilRotationTarget, recoilProgress);
            transform.localRotation = finalRotation;
        }
        else
        {
            // Return the gun to its starting position and rotation
            transform.localPosition = startingPosition;
            transform.localRotation = startingRotation;

            isRecoiling = false;
        }
    }
}
