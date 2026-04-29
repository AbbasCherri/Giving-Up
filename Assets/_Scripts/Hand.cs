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

    private static int IsHolding = Animator.StringToHash("isHolding");
    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;
    private Vector3 startLocalScale;
    private Vector3 grabPosition;
    private Quaternion grabRotation;
    private int originalSiblingIndex;
    private bool isGrabbed;

    public bool IsHoldingGrip => Input.GetMouseButton((int)whichHand);
    public bool IsGrabbed => isGrabbed;
    public Vector3 GrabPosition => grabPosition;

    private void Awake()
    {
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

        if (!isGrabbed)
            DrawAimRay(Color.red);
    }

    private void LateUpdate()
    {
        if (isGrabbed)
            HoldAtGrabPoint();
    }

    private void TryGrab()
    {
        Ray ray = BuildGrabRay();

        if (!Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer, QueryTriggerInteraction.Ignore))
            return;

        if (!hit.collider.GetComponentInParent<Grabbable>())
            return;

        grabPosition = hit.point + hit.normal * surfaceOffset;
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
        DrawAimRay(Color.green);
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

    private void DrawAimRay(Color color)
    {
        Ray ray = BuildGrabRay();
        Debug.DrawRay(ray.origin, ray.direction * grabDistance, color);
    }

    private void SetHoldingAnimation(bool isHolding)
    {
        if (!animator)
            return;

        animator.enabled = true;
        animator.SetBool(IsHolding, isHolding);
    }
}
