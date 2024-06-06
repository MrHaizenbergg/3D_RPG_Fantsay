using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    [Header("GeneralSettings")]
    [Tooltip("Object with GameControll.cs script")]
    public GameControll gameControll;
    [HideInInspector]
    public Inventory inventory;

    [Header("PlayerSettings")]
    [Tooltip("Player walk speed")]
    public float walkSpeed;
    [Tooltip("Player crouch speed")]
    public float crouchSpeed;
    [HideInInspector]
    public bool locked;
    [HideInInspector]
    public bool lockedMovement;
    [HideInInspector]
    public bool canBeCatchen;
    public CharacterController characterController;
    private float moveSpeed;
    [HideInInspector]
    public bool playerMoving;

    [Header("CameraSettings")]
    [Tooltip("Mouse Sensetivity value")]
    public float mouseSensetivity;
    [Tooltip("Main camera transform")]
    public Transform cameraTransform;
    public Transform cameraRoot;
    private float clampX;
    private float clampY;

    [Header("Camera Animations")]
    [Tooltip("Camera animation gameobject")]
    public Animation cameraAnimation;
    [Tooltip("Camera hit animation name")]
    public string cameraHitAnimName;
    [Tooltip("Camera idle animation name")]
    public string cameraIdleAnimName;
    [Tooltip("Camera move animation name")]
    public string cameraMoveAnimName;

    public Transform playerBody;

    [Header("CrouchSettings")]

    private float lerpSpeed = 10f;
    [Tooltip("Player character controller normal height")]
    public float normalHeight;
    [Tooltip("Player character controller crouch height")]
    public float crouchHeight;
    [Tooltip("Player camera normal offset")]
    public float cameraNormalOffset;
    [Tooltip("Player camera crouch offset")]
    public float cameraCrouchOffset;
    [Tooltip("Player obstacle layers")]
    public LayerMask obstacleLayers;
    [Tooltip("Clamp camera by Y axis")]
    public bool clampByY;
    public Vector2 clampXaxis;
    public Vector2 clampYaxis;
    [Tooltip("Time takes to repair broken legs")]
    public float legsFixTime;
    [HideInInspector]
    public bool crouch = false;

    [Header("UI Settigns")]
    [Tooltip("UI stand icon for mobile only")]
    public Image imageStand;
    [Tooltip("UI crouch icon for mobile only")]
    public Image imageCrouch;
    [Tooltip("UI crouch icon for mobile only")]
    public Image imageExitHidePlace;

    [Header("Hide Place Settings")]
    public HidePlace hidePlace;

    private float mouseX;
    private float mouseY;

    private float inputX;
    private float inputY;

    private float newHeight;
    private float newCamPos;

    private Vector3 forwardMove;
    private Vector3 sideMove;

    private int footStepInt;

    private Vector3 camEuler;

    [Header("Sounds Settings")]
    private AudioSource AS;
    [Tooltip("Foot steps sounds")]
    public AudioClip[] footSteps;
    [Tooltip("Sound of breaking legs")]
    public AudioClip legBreakSound;
    private bool legBreak;

    [SerializeField] private SplashWindow splashWindow;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
        inventory = GetComponent<Inventory>();
        characterController = GetComponent<CharacterController>();
        clampX = 0f;
        moveSpeed = walkSpeed;
        imageStand.enabled = true;
        imageCrouch.enabled = false;
        imageExitHidePlace.enabled = false;
    }

    private void Update()
    {
        canBeCatchen = characterController.isGrounded;

        if (!locked)
        {
            CameraRotation();
            HidePlaceExit();
            if (!lockedMovement)
            {
                Movement();
                Controll();
            }
        }
    }

    private void Controll()
    {
        if (CrossPlatformInputManager.GetButtonDown("Crouch"))
        {
            SetCrouch();
        }

        newHeight = crouch ? crouchHeight : normalHeight;
        characterController.height = Mathf.Lerp(characterController.height, newHeight, Time.deltaTime * lerpSpeed);

        characterController.center = Vector3.down * (normalHeight - characterController.height) / 2.0f;

        newCamPos = crouch ? cameraCrouchOffset : cameraNormalOffset;
        //Vector3 newPos = new Vector3(cameraTransform.localPosition.x, newCamPos, cameraTransform.localPosition.z);
        //cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPos, Time.deltaTime * lerpSpeed);

        if (CrossPlatformInputManager.GetButtonDown("Drop"))
        {
            gameControll.inventory.DropItem();
        }
    }

    public void PlayerLegsBreak()
    {
        gameControll.ScreenBlood(1);
        lockedMovement = true;
        crouch = true;
        moveSpeed = crouchSpeed;
        imageStand.enabled = false;
        imageCrouch.enabled = true;
        characterController.height = crouchHeight;
        cameraTransform.localPosition = new Vector3(0f, cameraCrouchOffset, 0f);
        AS.PlayOneShot(legBreakSound);
        StartCoroutine(WaitLegsFix());
    }

    private void Movement()
    {
        inputX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
        inputY = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;

        forwardMove = playerBody.forward * inputY;
        //Vector3 forvardMove = Camera.main.transform.TransformDirection(Vector3.forward) * inputY*1.3f;
        sideMove = transform.right * inputX;
        characterController.SimpleMove(forwardMove + sideMove);

        if (characterController.velocity.magnitude > 0.5f)
        {
            playerMoving = true;
            cameraAnimation.Play(cameraMoveAnimName);
            cameraAnimation[cameraMoveAnimName].speed = moveSpeed / 3f;
            //Debug.Log("MoveAnim");

        }
        else
        {
            playerMoving = false;
            //cameraAnimation.Play(cameraIdleAnimName);
        }
    }

    private void CameraRotation()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

        mouseX = CrossPlatformInputManager.GetAxis("Mouse X") * (mouseSensetivity * 2) * Time.deltaTime;
        mouseY = CrossPlatformInputManager.GetAxis("Mouse Y") * (mouseSensetivity * 2) * Time.deltaTime;

        clampX += mouseY;
        clampY += mouseX;

        if (clampX > clampXaxis.y)
        {
            clampX = clampXaxis.y;
            mouseY = 0.0f;
            ClampXAxis(clampXaxis.x);
        }
        else if (clampX < clampXaxis.x)
        {
            clampX = clampXaxis.x;
            mouseY = 0.0f;
            ClampXAxis(clampXaxis.y);
        }

        if (clampByY)
        {

            if (clampY > clampYaxis.y)
            {
                clampY = clampYaxis.y;
                mouseX = 0.0f;
                ClampYAxis(clampYaxis.y);
            }
            else if (clampY < clampYaxis.x)
            {
                clampY = clampYaxis.x;
                mouseX = 0.0f;
                ClampYAxis(clampYaxis.x);
            }
        }

        cameraTransform.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxis(float value)
    {
        camEuler = cameraTransform.eulerAngles;
        camEuler.x = value;
        cameraTransform.eulerAngles = camEuler;
    }

    private void ClampYAxis(float value)
    {
        camEuler = transform.eulerAngles;
        camEuler.y = value;
        transform.eulerAngles = camEuler;
    }

    private void SetCrouch()
    {
        if (!crouch)
        {
            crouch = true;
            moveSpeed = crouchSpeed;
            imageStand.enabled = false;
            imageCrouch.enabled = true;
        }
        else
        {

            //if (CheckDistance() > normalHeight)
            //{
            crouch = false;
            moveSpeed = walkSpeed;
            imageStand.enabled = true;
            imageCrouch.enabled = false;
            //}
        }
    }

    private void HidePlaceExit()
    {
        if (hidePlace)
        {
            if (CrossPlatformInputManager.GetButtonDown("Unhide"))
            {
                hidePlace.ExitHidePlace();
            }
        }
    }

    public void FootStepPlay()
    {
        footStepInt = Random.Range(1, footSteps.Length);
        AS.volume = moveSpeed / 6;
        AS.PlayOneShot(footSteps[footStepInt]);
    }

    private IEnumerator WaitLegsFix()
    {
        yield return new WaitForSeconds(legsFixTime);
        lockedMovement = false;
        gameControll.ScreenBlood(0);

    }
}