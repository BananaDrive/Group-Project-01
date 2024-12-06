using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{
    public Rigidbody Rib;
    public float walk, jump;

    private Vector2 input;

    public LayerMask Terrain;
    public Transform GroundPoint;
    public bool OnGround;
    public bool Hidden = false;
    public GameObject[] HideObject;
    public float HideingRange = 10f;
    public float HideWait = 1f;


    private Collider currentHideObject = null;
    private int OGLayer;
    private bool canHide = false;

    public bool sprintMode = false;
    public float sprintMultiplier = 1.5f;

    public GameObject Key;
    public bool HasKey = false;

    public GameObject DropFloor;

    public GameObject FlashLight;
    public bool FlashPickedup = false;
    public float FlashRadius = 3f;
    public Transform Flashy;
    public TextMeshProUGUI InteractText;


    GameManager Gm;


    void Start()
    {
        Gm = gameObject.GetComponent<GameManager>();
        OGLayer = gameObject.layer;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (!Hidden)
        {
            // Handle input

            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            input.Normalize();

            // Determine current speed

            float currentSpeed = sprintMode ? walk * sprintMultiplier : walk;

            // Apply movement

            Vector3 moveDirection = new Vector3(input.x * currentSpeed, Rib.linearVelocity.y, input.y * currentSpeed);
            Rib.linearVelocity = moveDirection;
        }

        if (Input.GetKeyDown(KeyCode.E) && canHide && currentHideObject != null)
        {
            ToggleHide();
        }

        // Sprint Mechanics

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMode = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintMode = false;
        }

        //FlashlightPickup
        if (Vector3.Distance(Flashy.position, transform.position) <= FlashRadius)
        {
            InteractText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                FlashLight.transform.SetParent(transform);
                FlashLight.transform.localPosition = new Vector3(0.5f, 0, 0.5f);
                FlashLight.transform.localRotation = Quaternion.identity;

                FlashPickedup = true;

                InteractText.gameObject.SetActive(false);
            }
        }
        else
        {
            InteractText.gameObject.SetActive(false);
        }

        //FlashLight Toggle

        if (Input.GetKey(KeyCode.F) && FlashPickedup == true)
        {
            if (FlashLight != null)
            {
                Light FlashLightLight = FlashLight.GetComponentInChildren<Light>();
                if (FlashLightLight != null)
                {
                    FlashLightLight.enabled = !FlashLightLight.enabled;
                }
            }
        }

    }



    private void ToggleHide()
    {
        Hidden = !Hidden;

        if (Hidden)
        {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
            GetComponent<Renderer>().enabled = false;

            if (currentHideObject != null)
            {
                transform.position = currentHideObject.bounds.center;
            }

            Rib.linearVelocity = Vector3.zero;
            Rib.angularVelocity = Vector3.zero;
        }
        else
        {
            gameObject.layer = OGLayer;
            GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DropFloor != null)
            {
                Destroy(DropFloor);
            }
        }
        foreach(GameObject hideObj in HideObject)
        {
            if (other.gameObject == hideObj)
            {
                currentHideObject = other;

                if (Vector3.Distance(transform.position, hideObj.transform.position) <= HideingRange)
                {
                    canHide = true;
  
                }
                return;
            }

            
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentHideObject)
        {
            currentHideObject = null;
            canHide = false;

        }
    }

    



}
