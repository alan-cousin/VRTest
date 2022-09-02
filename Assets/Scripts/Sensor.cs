//-------------------------------------------------------Alan_cousin2100

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sensor : MonoBehaviour
{
    //send haptic infos to device
    public void ActivateHaptic()
    {
        GameObject.Find("XR Origin").GetComponent<AudioSource>().enabled = true;
        ActionBasedController xRController = gameObject.GetComponent<ActionBasedController>();
        xRController?.SendHapticImpulse(0.7f, 2f);
    }
    //non activate haptic
    public void DeActivateHaptic() 
    {
        GameObject.Find("XR Origin").GetComponent<AudioSource>().enabled = false;
    }
}
