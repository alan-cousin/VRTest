//-------------------------------------------------------Alan_cousin2100

using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class ContinuousMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public XRNode inputSource;
    public Vector2 inputAxis;
    public LayerMask groundLayer;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float heightOffset = 2;
    public GameObject deviceSimulator;

    private CharacterController character;
    private XROrigin xrOrigin;
    private float fallingSpeed = 0;
    private float gravity = -9.81f;
    //float delayTime = 0;
    void Start()
    {
        character = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        InputActionReference iar = deviceSimulator.GetComponent<XRDeviceSimulator>().axis2DAction;
        
#else
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
#endif
    }

    void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * speed * Time.deltaTime);

        bool isGround = CheckIfGround();
        if (isGround) 
        {
            fallingSpeed = 0;
        }
        else 
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

        CapsuleFollowHeadset();
    }
    //check if the gameObject is on ground.
    bool CheckIfGround() 
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y * 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    //set character's center following Headset
    void CapsuleFollowHeadset() 
    {
        character.height = xrOrigin.CameraInOriginSpaceHeight + heightOffset;
        Vector3 capsuleCenter = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
