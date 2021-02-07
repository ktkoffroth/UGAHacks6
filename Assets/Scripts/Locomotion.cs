using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// ReSharper disable All

public class Locomotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public SteamVR_Action_Boolean snapLeft;
    public SteamVR_Action_Boolean snapRight;
    public SteamVR_Action_Boolean move;
    public SteamVR_Action_Vector2 movement;
    public GameObject cameraGameObject;
    public float speedMod;
    public float gravity;
    private CharacterController characterController;
    public SteamVR_Action_Single magTrigger;
    public GameObject magButton;
    public float buttonTravel;


    public bool getSnapLeftDown()
    {
        return snapLeft.GetStateDown(SteamVR_Input_Sources.LeftHand);
    }

    public bool getSnapRightDown()
    {
        return snapRight.GetStateDown(SteamVR_Input_Sources.LeftHand);
    }

    // public bool getSnapLeftUp()
    // {
    //     return snapLeft.GetStateUp(SteamVR_Input_Sources.LeftHand);
    // }
    //
    // public bool getSnapRightUp()
    // {
    //     return snapRight.GetStateUp(SteamVR_Input_Sources.LeftHand);
    // }

    public Vector3 getMovement()
    {
        var pos = movement.GetAxis(SteamVR_Input_Sources.RightHand);
        var mover = new Vector3(pos.x * speedMod, 0, pos.y * speedMod);

        var lookAngle = cameraGameObject.transform.rotation.eulerAngles;
        var directionAngle = Quaternion.Euler(0, lookAngle.y, 0);

       // Debug.Log(directionAngle);

        return move.GetState(SteamVR_Input_Sources.RightHand)
            ? directionAngle * mover
            : Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // gameObject.transform.position += getMovement();

        magButton.transform.localPosition =
            new Vector3(buttonTravel * magTrigger.GetAxis(SteamVR_Input_Sources.LeftHand), 0, 0);

        var camPos = cameraGameObject.transform.localPosition;
        characterController.center = new Vector3(camPos.x, camPos.y / 2, camPos.z);
        characterController.height = camPos.y;

        characterController.Move(getMovement() + Vector3.down * gravity * Time.deltaTime);
        // if (getSnapLeftDown())
        // {
        //     transform.RotateAround(transform.position, Vector3.up, -30);
        // }
        // else if (getSnapRightDown())
        // {
        //     transform.RotateAround(transform.position, Vector3.up, 30);
        // }
    }
}