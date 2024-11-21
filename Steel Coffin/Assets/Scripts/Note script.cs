using UnityEngine;

[CreateAssetMenu(fileName = "Notescript", menuName = "Scriptable Objects/Notescript")]
public class Notescript : MonoBehaviour
{
    
    public float activationRange = 1f;
    public Transform player;
    public GameObject Note1;
    public bool Note = false;


    public void Start()
    {
        Note1.SetActive(false);
        Note = false;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= activationRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Note)
                {
                    Note1.SetActive(false);
                    Note = false;
                }
                else
                {
                    Note1.SetActive(true);
                    Note = true;
                }

                
            }
        }
        else
        {
            if (Note)
            {
                Note1.SetActive(false);
                Note = false;
            }
        }

        

    }


}
