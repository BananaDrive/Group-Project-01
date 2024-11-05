using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasicScript : MonoBehaviour
{
    public float WalkSpeed;
    public float Distance;

    public LayerMask terrain;
    public Rigidbody Rb;
    public SpriteRenderer Sr;





    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrain))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + Distance;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        Rb.angularVelocity = moveDir * WalkSpeed;

        if (x != 0 && x < 0)
        {
            Sr.flipX = true;
        }

        else if (x != 0 && x > 0)
        {
            Sr.flipX = false;
        }
    }
}