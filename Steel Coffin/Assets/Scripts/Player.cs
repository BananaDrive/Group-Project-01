using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rib;
    public float walk, jump;

    private Vector2 input;

    public LayerMask Ground;
    public Transform GroundPoint;
    public bool OnGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();

        Rib.linearVelocity = new Vector3(input.x * walk, Rib.linearVelocity.y, input.y * walk);

        RaycastHit hit;
        if (Physics.Raycast(GroundPoint.position, Vector3.down, out hit, .3f, Ground))
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
}
