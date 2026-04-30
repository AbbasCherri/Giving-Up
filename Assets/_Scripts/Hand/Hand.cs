using System;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class Hand : MonoBehaviour
{
    public enum WhichHand
    {
        Left = 0,
        Right = 1
    }
    
    [SerializeField] private WhichHand whichHand;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float grabDistance = 3f;
    [SerializeField] private float aimDistance = 25f;
    [SerializeField] private float surfaceOffset = 0.02f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private HandBob handBob;
    [SerializeField] private float maxGripTime = 5;
    [SerializeField] private Renderer HandImage;
    [SerializeField] private Bar GripLeft; 

    private static int IsHolding = Animator.StringToHash("isHolding"); //faster 
    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;
    private Vector3 startLocalScale;
    private Vector3 grabPosition;
    private Quaternion grabRotation;
    private int originalSiblingIndex;
    private bool isGrabbed;
    private Color originalColor;
    private Color handColor;
    private float currentGripTime = 0f;
    private float currentGripTimeModifier;
    private Material handMaterial;
    private float gripPercent = 0;
    
    public bool IsHoldingGrip => Input.GetMouseButton((int)whichHand);
    public bool IsGrabbed => isGrabbed;
    public Vector3 GrabPosition => grabPosition;
    private void Awake()
    {
        handMaterial =  HandImage.material;
        originalColor = handMaterial.color;
        if (!playerCamera)
            playerCamera = Camera.main;

        if (!animator)
            animator = GetComponent<Animator>();

        if (!handBob)
            handBob = GetComponent<HandBob>();

        originalSiblingIndex = transform.GetSiblingIndex();
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
        startLocalScale = transform.localScale;

        if (grabbableLayer.value == 0)
            grabbableLayer = LayerMask.GetMask("Grabbable");
        if (HandImage)
        {
            handColor = HandImage.material.color;
            originalColor = HandImage.material.color;
        }
        
    }

    private void Update()
    {
        bool isHolding = IsHoldingGrip;

        if (!isHolding && isGrabbed)
        {
            Release();
        }
        else if (isHolding && !isGrabbed)
        {
            SetHoldingAnimation(true);
            TryGrab();
        }
        else if (!isGrabbed)
        {
            SetHoldingAnimation(false);
        }

        if (!isGrabbed && currentGripTime > 0)
        {
            currentGripTime = Mathf.Max(0,currentGripTime - Time.deltaTime);
        }

        if (isGrabbed)
        {
            currentGripTime = Mathf.Min(maxGripTime+1,currentGripTime + Time.deltaTime);
            if (currentGripTime > maxGripTime)
            {
                Release();
            }
        }
        gripPercent = maxGripTime <= 0f ? 0f : currentGripTime / maxGripTime;
        handColor = Color.Lerp(originalColor, Color.red, gripPercent);
        HandImage.material.color = handColor;
        
        GripLeft.BarProgress(currentGripTime, maxGripTime);
    }

    private void LateUpdate()
    {
        if (isGrabbed)
            HoldAtGrabPoint();
    }

    private void TryGrab()
    {
        Ray ray = BuildGrabRay();

        if (!Physics.SphereCast(ray, 0.1f, out RaycastHit hit, grabDistance, grabbableLayer, QueryTriggerInteraction.Ignore))
            return;

        if (!hit.collider.GetComponentInParent<Grabbable>())
            return;
        float scaledOffset = surfaceOffset * hit.collider.bounds.size.magnitude;
        grabPosition = hit.point + hit.normal * scaledOffset;
        grabRotation = Quaternion.LookRotation(-hit.normal, playerCamera ? playerCamera.transform.up : Vector3.up);
        isGrabbed = true;

        HoldAtGrabPoint();

        if (animator)
        {
            animator.Update(0f);
            animator.enabled = false;
        }

        if (handBob)
            handBob.enabled = false;
    }

    private void HoldAtGrabPoint()
    {
        transform.SetPositionAndRotation(grabPosition, grabRotation);
    }

    private void Release()
    {
        isGrabbed = false;
        transform.SetSiblingIndex(originalSiblingIndex); 
        transform.localPosition = startLocalPosition;
        transform.localRotation = startLocalRotation;
        transform.localScale = startLocalScale;
        SetHoldingAnimation(false);
        if (handBob)
            handBob.enabled = true;
    }

    private Ray BuildGrabRay()
    {
        if (!playerCamera)
        {
            return new Ray(transform.position, transform.forward);
        }

        Vector3 aimPoint = playerCamera.transform.position + playerCamera.transform.forward * aimDistance;
        Vector3 direction = (aimPoint - transform.position).normalized;
        return new Ray(transform.position, direction);
    }

    
    private void SetHoldingAnimation(bool isHolding)
    {
        if (!animator)
            return;

        animator.enabled = true;
        animator.SetBool(IsHolding, isHolding);
    }
}
