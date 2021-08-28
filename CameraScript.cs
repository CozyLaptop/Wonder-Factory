using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    //GameObject camObj;
    CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer framingTransposer;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance = 40;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (framingTransposer.m_CameraDistance >= 10)
            {
                framingTransposer.m_CameraDistance--;
                Debug.Log("Zoom in");
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (framingTransposer.m_CameraDistance <= 100)
            {
                framingTransposer.m_CameraDistance++;
                Debug.Log("Zoom out");
            }
        }
    }

}
