using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour

{
    public float bobSpd;
    public float bobHeight;
    public float midPoint;

    public bool isBobbing;

    private float timer = 0.0f;

    private CharacterController theCC;

    void Start()
    {
        theCC = GameObject.FindObjectOfType<CharacterController>();
    }

    void Update()
    {
        float waveSlice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (theCC.isGrounded)
        {
            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveSlice = Mathf.Sin(timer);
                timer = timer + bobSpd;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }

            if (waveSlice != 0)
            {
                float translateChange = waveSlice * bobHeight;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;

                if (isBobbing == true)
                    cSharpConversion.y = midPoint + translateChange;
                else if (isBobbing == false)
                    cSharpConversion.x = midPoint + translateChange;
            }
            else
            {
                if (isBobbing == true)
                    cSharpConversion.y = midPoint;
                else if (isBobbing == false)
                    cSharpConversion.x = 0;
            }

            transform.localPosition = cSharpConversion;
        }
    }
}

