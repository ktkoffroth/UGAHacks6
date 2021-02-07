using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Magnify : MonoBehaviour
{
    public SteamVR_Action_Single triggerMagnify;

    public GameObject brokenLens;
    public GameObject goodLens;
    public GameObject fillBody;

    public ReflectionProbe reflectionProbe;
    
    public float cycleTime;
    public float brokenTimeScale;


    // Start is called before the first frame update
    void Start()
    {
        goodLens.SetActive(true);
        brokenLens.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        var currentScale = fillBody.transform.localScale;
        reflectionProbe.cullingMask &= ~(1 << 6);
        
        if (goodLens.activeSelf && triggerMagnify.GetAxis(SteamVR_Input_Sources.LeftHand) > 0.75f)
        {
            currentScale = new Vector3(currentScale.x, currentScale.y,
                currentScale.z + 90f * Time.deltaTime / cycleTime);

            reflectionProbe.cullingMask |= 1 << 6;
            
            if (currentScale.z >= 100)
            {
                goodLens.SetActive(false);
                brokenLens.SetActive(true);
                currentScale = new Vector3(currentScale.x, currentScale.y, 100);
            }

            fillBody.transform.localScale = currentScale;
        }

        else if (goodLens.activeSelf && triggerMagnify.GetAxis(SteamVR_Input_Sources.LeftHand) <= 0.75f &&
                 currentScale.x > 10)
        {
            currentScale = new Vector3(currentScale.x, currentScale.y,
                currentScale.z - 90f * Time.deltaTime / cycleTime);
            
            if (currentScale.z <= 10)
            {
                currentScale = new Vector3(currentScale.x, currentScale.y, 10);
            }

            fillBody.transform.localScale = currentScale;
        }
        
        else if (brokenLens.activeSelf)
        {
            currentScale = new Vector3(currentScale.x, currentScale.y,
                currentScale.z - 90f * Time.deltaTime / (cycleTime * brokenTimeScale ));
            
            if (currentScale.z <= 10)
            {
                currentScale = new Vector3(currentScale.x, currentScale.y, 10);
                goodLens.SetActive(true);
                brokenLens.SetActive(false);
            }

            fillBody.transform.localScale = currentScale;
        }
    }
}