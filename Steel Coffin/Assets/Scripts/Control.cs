using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        castPos.y += 2;
        if (Physics.Raycast(castPos, Vector3.down, out hit, Mathf.Infinity, terrain))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);

        Rb.linearVelocity = new Vector3(moveDir.x * WalkSpeed, Rb.linearVelocity.y, moveDir.z * WalkSpeed);

        if (x != 0)
        {
            Sr.flipX = x < 0;
        }
    }
}