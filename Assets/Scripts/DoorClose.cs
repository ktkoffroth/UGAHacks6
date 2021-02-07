using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //load next room
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.GetType());
        if (other.GetType() == typeof(CharacterController))
        {
            transform.parent.GetComponent<HingeJoint>().useMotor = false;
            //unload room
        }
    }
}
