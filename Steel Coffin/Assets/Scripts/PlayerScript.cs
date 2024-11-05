using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasicScript : MonoBehaviour
{

    GameManager Gm;  //Reference to GameManager Script

    [Header("Speed")]
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float mouseSensitivity = 2f;
    public float CrouchSpeed = 1.1f;

    private CharacterController controller;
    private float verticalRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;
    public Camera camera;
    public Transform Camera;


    [Header("Aiming")]
    public bool isAiming = false;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public Transform gunTransform;
    public Vector3 gunADSPosition;
    public Vector3 gunNormalPosition;

    [Header("Crouching")]
    public float crouchHeight = 0.5f;
    private float originalHeight;
    private bool isCrouching = false;
    private Vector3 targetPosition;


    [Header("leaning")]
    public float leanAmount = 100f;
    public float leanAmount1 = 100f;
    public float leanSpeed = 5f;
    private float targetLean = 30f;
    private float targetLean1 = .5f;
    private float currentLean = 0f;
    public Transform cameraParent;

    [Header("Interaction")]
    public float interactionRange = 3f;
    public LayerMask Interactables;
    public TextMeshProUGUI interactionText;

    [Header("Ammo")]
    public int pistolammo;
    public int shotgunammo;



    [Header("Guns")]
    public int weaponIndexToUnlock;

    void Start()
    {

        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        Cursor.lockState = CursorLockMode.Locked;
        targetPosition = transform.localPosition;
    }

    void Update()
    {
        if (!Gm.IsPaused)
        {
            if (controller == null) return;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.Rotate(0, mouseX, 0);
            camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


            isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                StartADS();
            }

            if (Input.GetMouseButtonUp(1))
            {
                StopADS();
            }


            if (isAiming)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomFOV, 0.1f);
                gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunADSPosition, 0.1f);
            }
            else
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, normalFOV, 0.1f);
                gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunNormalPosition, 0.1f);
            }




            if (Input.GetKey(KeyCode.Q))
            {
                targetLean = leanAmount;
                targetLean1 = -leanAmount1;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                targetLean = -leanAmount;
                targetLean1 = leanAmount1;
            }
            else
            {
                targetLean = 0f;
                targetLean1 = 0f;
            }

            cameraParent.localPosition = new Vector3(targetLean1, cameraParent.localPosition.y, cameraParent.localPosition.z);


            currentLean = Mathf.Lerp(currentLean, targetLean, Time.deltaTime * leanSpeed);
            camera.transform.localRotation = Quaternion.Euler(camera.transform.localRotation.eulerAngles.x, camera.transform.localRotation.eulerAngles.y, currentLean);


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                float jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                velocity.y = jumpForce;
            }



            transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, Time.deltaTime * CrouchSpeed);

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, interactionRange, Interactables))
                {
                    InteractableObject interactableObject = hitinfo.collider.GetComponent<InteractableObject>();
                    if (interactableObject != null)
                    {
                        interactableObject.Interact();
                        interactionText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    interactionText.gameObject.SetActive(false);
                }
            }
            else
            {
                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, interactionRange, Interactables))
                {
                    InteractableObject interactableObject = hitinfo.collider.GetComponent<InteractableObject>();
                    if (interactableObject != null)
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "F";
                    }
                }
                else
                {
                    interactionText.gameObject.SetActive(false);
                }

            }
        }
    }

    public int GetCurrentAmmo(int weaponID)
    {
        return weaponID switch
        {
            0 => pistolammo,      // M1911
            1 => shotgunammo,      // M4
            _ => 0
        };
    }

    public void DecreaseAmmo(int weaponID, int amount)
    {
        switch (weaponID)
        {
            case 0:
                pistolammo = Mathf.Max(0, pistolammo - amount);
                break;
            case 1:
                shotgunammo = Mathf.Max(0, shotgunammo - amount);
                break;
        }
    }

    

   

    private void StartADS()
    {
        isAiming = true;
    }

    private void StopADS()
    {
        isAiming = false;
    }



}
