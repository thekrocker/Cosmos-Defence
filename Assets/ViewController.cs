using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    // == CONFIG PARAMS ==
    //
    // Set the desired aspect ratio
    float targetAspect = 9.0f / 16.0f;
    // Set scale size (1 = full scale)
    float scaleSize = 1.0f;
    // Set the value being used to divide
    float scaleCalc = 2.0f;
    // Declare variables to track scale/aspect
    float windowAspect;
    float scaleHeight;


    // == CACHED REFERENCES ==
    //
    // Declare variable for camera reference
    Camera cameraView;
    // Declare variable for rect reference
    Rect rect;


    // == AWAKE==
    //
    // Use this for initialization BEFORE Start
    void Awake()
    {
        // Obtain camera component so we can modify its viewport
        cameraView = GetComponent<Camera>();

        // Set the rect to be the camera view
        rect = cameraView.rect;

        // Adjust the camera view
        AdjustView();
    }


    // == UPDATE ==
    //
    // Update is called once per frame
    void Update()
    {
        // Adjust the camera view
        AdjustView();
    }


    // == METHODS ==
    //
    // Force view to match 9:16 aspect ratio
    private void AdjustView()
    {
        // Determine the game window's current aspect ratio
        windowAspect = (float)Screen.width / (float)Screen.height;

        // Current viewport height should be scaled by this amount
        scaleHeight = windowAspect / targetAspect;

        // Detect whether it's necessary to change view
        if (scaleHeight < scaleSize)
        {
            AddLetterbox();
        }
        if (scaleHeight > scaleSize)
        {
            AddPillarbox();
        }
        cameraView.rect = rect;
    }

    // Add letterbox (horizontal)
    private void AddLetterbox()
    {
        rect.width = scaleSize;
        rect.height = scaleHeight;
        rect.x = 0;
        rect.y = (scaleSize - scaleHeight) / scaleCalc;
    }

    // Add pillarbox (vertical)
    public void AddPillarbox()
    {
        float scaleWidth = scaleSize / scaleHeight;

        rect.width = scaleWidth;
        rect.height = scaleSize;
        rect.x = (scaleSize - scaleWidth) / scaleCalc;
        rect.y = 0;
    }
}
