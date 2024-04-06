using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float rotationSpeed;
    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject AimCam;

    float aiming;


    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Aim
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStyle = CameraStyle.Basic;
    }

    private void Update()
    {


        // switch Style




        //rotation orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);

        // roate player objekt
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInout = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInout;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
            combatCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.7f;
            combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = thirdPersonCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;

            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        }

        else if (currentStyle == CameraStyle.Combat)
        {
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;

            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.75f;
            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;

            AimCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = combatCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value;
            AimCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;

            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
            if (Input.GetKeyDown(KeyCode.Space)) SwitchCameraStyle(CameraStyle.Aim);
            
        }




        else if (currentStyle == CameraStyle.Aim)
        {
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;

            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.75f;
            combatCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = AimCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value;

            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = AimCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
            combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = AimCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;


            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
            if (Input.GetKeyUp(KeyCode.Space)) SwitchCameraStyle(CameraStyle.Combat);
        }



    }


    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        // thirdPersonCam.SetActive(false);
        // combatCam.SetActive(false);

        combatCam.GetComponent<CinemachineFreeLook>().Priority = 0;
        thirdPersonCam.GetComponent<CinemachineFreeLook>().Priority = 0;
        AimCam.GetComponent<CinemachineFreeLook>().Priority = 0;


        // if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Basic) thirdPersonCam.GetComponent<CinemachineFreeLook>().Priority = 1;

        // if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.GetComponent<CinemachineFreeLook>().Priority = 1;

        if (newStyle == CameraStyle.Aim) AimCam.GetComponent<CinemachineFreeLook>().Priority = 1;

        currentStyle = newStyle;
    }

    // void OnAim(InputValue value)
    // {
    //     aiming = value.Get<float>();
    //     Debug.Log("jalla");
    // }

}



