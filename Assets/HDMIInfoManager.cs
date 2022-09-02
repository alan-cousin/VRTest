//-------------------------------------------------------Alan_cousin2100

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//------------------------------Test class

public class HDMIInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Device Active " + XRSettings.isDeviceActive);
        Debug.Log("Device Name is " + XRSettings.loadedDeviceName);

        if (!XRSettings.isDeviceActive) 
        {
            Debug.Log("No Headset plugged");
        }

        if(XRSettings.isDeviceActive && XRSettings.loadedDeviceName != "Mock HMD" && XRSettings.loadedDeviceName == "MockHMDDisplay") 
        {
            Debug.Log("Using Mock HMD");
        }
        else 
        {
            Debug.Log("We have a headset " + XRSettings.loadedDeviceName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
