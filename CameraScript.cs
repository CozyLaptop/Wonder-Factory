using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour{
    CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer framingTransposer;
    // Start is called before the first frame update
    void Start(){
        //  Grabs camera and framing transposer
        vcam = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance = 40;
    }

    // Update is called once per frame
    void Update() {
        // Zoom in on mouse scrool
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (framingTransposer.m_CameraDistance >= 10) {
                framingTransposer.m_CameraDistance--;
            }
        }
        // Zoom out on mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if (framingTransposer.m_CameraDistance <= 80){
                framingTransposer.m_CameraDistance++;
            }
        }
    }
}
