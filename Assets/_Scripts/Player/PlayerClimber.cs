using StarterAssets;
using UnityEngine;

public class PlayerClimber : MonoBehaviour
{
    [SerializeField] private Hand leftHand;
    [SerializeField] private Hand rightHand;
    
    [SerializeField] private Camera playerCamera;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private StarterAssetsInputs input;
    
    [SerializeField] private float climbSpeed = 3.5f;
    [SerializeField] private float maxReach = 2.2f;
    [SerializeField] private float reachCorrectionStrength = 16f;
    [SerializeField] private float anchoredGravity = -0.75f;

    private CharacterController characterController;
    private float defaultMoveSpeed;
    private float defaultSprintSpeed;
    private float defaultGravity;

    private bool IsClimbing => (leftHand && leftHand.IsGrabbed) || (rightHand && rightHand.IsGrabbed);

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (!playerCamera)
            playerCamera = Camera.main;

        if (!firstPersonController)
            firstPersonController = GetComponent<FirstPersonController>();

        if (!input)
            input = GetComponent<StarterAssetsInputs>();

        if (firstPersonController)
        {
            defaultMoveSpeed = firstPersonController.MoveSpeed;
            defaultSprintSpeed = firstPersonController.SprintSpeed;
            defaultGravity = firstPersonController.Gravity;
        }
    }

    private void LateUpdate()
    {
        if (!IsClimbing)
        {
            RestoreWalkingSettings();
            return;
        }

        ApplyClimbingSettings();
        characterController.Move(BuildClimbMovement());
    }

    private Vector3 BuildClimbMovement()
    {
        Vector3 movement = Vector3.zero;

        if (input && playerCamera)
        {
            Vector2 move = input.move;
            Vector3 climbDirection =
                playerCamera.transform.right * move.x +
                playerCamera.transform.up * move.y;

            movement += climbDirection * (climbSpeed * Time.deltaTime);
        }

        movement += BuildReachCorrection() * Time.deltaTime;
        movement += Vector3.up * (anchoredGravity * Time.deltaTime);

        return movement;
    }

    private Vector3 BuildReachCorrection()
    {
        if (!playerCamera)
            return Vector3.zero;

        Vector3 correction = Vector3.zero;
        int grabbedHands = 0;

        AddReachCorrection(leftHand, ref correction, ref grabbedHands);
        AddReachCorrection(rightHand, ref correction, ref grabbedHands);

        if (grabbedHands == 0)
            return Vector3.zero;

        return correction / grabbedHands;
    }

    private void AddReachCorrection(Hand hand, ref Vector3 correction, ref int grabbedHands)
    {
        if (!hand || !hand.IsGrabbed)
            return;

        grabbedHands++;

        Vector3 cameraToHand = hand.GrabPosition - playerCamera.transform.position;
        float distance = cameraToHand.magnitude;

        if (distance <= maxReach)
            return;

        float overReach = distance - maxReach;
        correction += cameraToHand.normalized * (overReach * reachCorrectionStrength);
    }

    private void ApplyClimbingSettings()
    {
        if (!firstPersonController)
            return;

        firstPersonController.ExternalMovementActive = true;
        firstPersonController.MoveSpeed = 0f;
        firstPersonController.SprintSpeed = 0f;
        firstPersonController.Gravity = 0f;
    }

    private void RestoreWalkingSettings()
    {
        if (!firstPersonController)
            return;

        firstPersonController.ExternalMovementActive = false;
        firstPersonController.MoveSpeed = defaultMoveSpeed;
        firstPersonController.SprintSpeed = defaultSprintSpeed;
        firstPersonController.Gravity = defaultGravity;
    }
}
