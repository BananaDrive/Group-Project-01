using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;


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


    void Start()
    {
        OGLayer = gameObject.layer;
    }


    void Update()
    {
        if (!Hidden)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            input.Normalize();

            Rib.linearVelocity = new Vector3(input.x * walk, Rib.linearVelocity.y, input.y * walk);
        }

            if (Input.GetKeyDown(KeyCode.F) && canHide && currentHideObject != null)
            {
                ToggleHide();
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
;
        }
    }
}
