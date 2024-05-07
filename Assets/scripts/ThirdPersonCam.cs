using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;


public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public GameObject crosshair;
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float rotationSpeed;
    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject AimCam;



    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat
    }


    public LayerMask aimCollaiderLayerMask;
    public Transform debugTransform;


    bool shoot;
    public Transform bulletProjektilePrefab;
    public Transform spawnBulletPosition;

    public float moveSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStyle = CameraStyle.Basic;
    }

    void Update()
    {
           thirdPersonCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.6f;
        Vector3 mouseWorldPosition = Vector3.zero;


        if (Input.GetMouseButton(1)) SwitchCameraStyle(CameraStyle.Combat);
        else SwitchCameraStyle(CameraStyle.Basic);


        // switch Style




        //rotation orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);

        // roate player objekt
        if (currentStyle == CameraStyle.Basic)
        {
            moveSpeed = 10f;


            float horizontalInout = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInout;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
            combatCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.62f;
            combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = thirdPersonCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;


            crosshair.SetActive(false);
        }

        else if (currentStyle == CameraStyle.Combat)
        {
        moveSpeed = 5;

            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;

            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.75f;
            thirdPersonCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = combatCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value;

            // if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);

            crosshair.SetActive(true);

            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimCollaiderLayerMask))
            {
                // debugTransform.position=raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }


            if (shoot)
            {
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(bulletProjektilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                shoot = false;
            }
        }



    }


    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        // thirdPersonCam.SetActive(false);
        // combatCam.SetActive(false);

        combatCam.GetComponent<CinemachineFreeLook>().Priority = 0;
        thirdPersonCam.GetComponent<CinemachineFreeLook>().Priority = 0;



        // if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Basic) thirdPersonCam.GetComponent<CinemachineFreeLook>().Priority = 1;

        // if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.GetComponent<CinemachineFreeLook>().Priority = 1;


        currentStyle = newStyle;
    }

    // void OnAim(InputValue value)
    // {
    //     aiming = value.Get<float>();
    //     Debug.Log("jalla");
    // }

    void OnShoot(InputValue value)
    {
        shootInput(value.isPressed);
    }
    void shootInput(bool newShootState)
    {
        shoot = newShootState;
    }

}




