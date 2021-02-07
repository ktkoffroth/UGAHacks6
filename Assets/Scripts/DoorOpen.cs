using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public bool front;
    private HingeJoint hingeJoint;
    
    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = transform.parent.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        var motor = hingeJoint.motor;
        
        motor.force = 100;
        motor.targetVelocity = front ? 50 : -50;
        motor.freeSpin = false;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }
}
