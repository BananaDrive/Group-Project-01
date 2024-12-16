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

    public GameObject DeathScreen;
    private bool Dead = false;
    public GameObject Inventory;


    GameManager Gm;


    void Start()
    {
        Gm = gameObject.GetComponent<GameManager>();
        OGLayer = gameObject.layer;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (DeathScreen != null)
        {
            DeathScreen.SetActive(false);
        }
    }


    void Update()
    {
        if (Dead)
            return;

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

        if (other.CompareTag("Enemy"))
        {
            Die();
        }

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

    void Die()
    {
        Dead = true;
        Rib.linearVelocity = Vector3.zero;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (DeathScreen != null)
        {
            DeathScreen.SetActive(true);
            Inventory.SetActive(false);
        }

        Time.timeScale = 0f;
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
