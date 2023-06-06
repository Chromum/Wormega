using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f; // Amount of weapon sway
    public float maxSwayAmount = 0.06f; // Maximum amount of weapon sway
    public float swaySmoothness = 2f; // Smoothness of weapon sway

    private Vector3 initialPosition; // Initial position of the weapon

    private void Start()
    {
        initialPosition = transform.localPosition; // Store the initial position of the weapon
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Get the horizontal mouse input
        float mouseY = Input.GetAxis("Mouse Y"); // Get the vertical mouse input

        // Calculate the target position based on the mouse input and sway amount
        float targetPosX = -mouseX * swayAmount;
        float targetPosY = -mouseY * swayAmount;

        // Clamp the target position to the maximum sway amount
        targetPosX = Mathf.Clamp(targetPosX, -maxSwayAmount, maxSwayAmount);
        targetPosY = Mathf.Clamp(targetPosY, -maxSwayAmount, maxSwayAmount);

        // Smoothly interpolate between the current position and the target position
        Vector3 targetPosition = new Vector3(targetPosX, targetPosY, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + targetPosition, Time.deltaTime * swaySmoothness);
    }
}
