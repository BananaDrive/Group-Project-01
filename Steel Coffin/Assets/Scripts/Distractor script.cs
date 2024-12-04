using System.Collections;
using UnityEngine;

public class Distractorscript : MonoBehaviour
{
    public float throwForce = 5f;
    public float LifeTime = 15f;
    public GameObject Distractprojectile;
    public Transform player;
    public Transform playerR;
    public Transform playerL;
    public int currentdistract = 3;
    public int maxdistract = 4;
    public bool isthrowing = false;
    public float throwrate = 2;
    public bool canthrow = true;

    public bool distracttriggered = false;
    GameManager Gm;


    private void Start()
    {
        Gm = gameObject.GetComponent<GameManager>();
    }
    void Update()
    {
       
            if (Input.GetMouseButtonDown(0) && currentdistract > 0 && !isthrowing)
            {
                distracttriggered = true;
                ThrowGrenadeL();
                isthrowing = true;
            }

            if (Input.GetMouseButtonDown(1) && currentdistract > 0 && !isthrowing)
            {
                distracttriggered = true;
                ThrowGrenadeR();
                isthrowing = true;
            }

        
    }

    private void ThrowGrenadeL()
    {

        GameObject projectile = Instantiate(Distractprojectile, playerL.position, playerL.rotation * Quaternion.Euler(90, 0, 0));
        
        projectile.AddComponent<Rigidbody>();

        projectile.GetComponent<Rigidbody>().mass = 0.01f;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Destroy(projectile.GetComponent<Distractorscript>());

        if (rb != null)
        {
            rb.AddForce(-playerL.transform.right * throwForce);
        }
        else
        {

        }

        currentdistract--;

        StartCoroutine(DestroyTime(projectile));
        StartCoroutine(CooldownThrow());
    }

    private void ThrowGrenadeR()
    {

        GameObject projectile = Instantiate(Distractprojectile, playerR.position, playerR.rotation * Quaternion.Euler(90, 0, 0));
        
        projectile.AddComponent<Rigidbody>();

        projectile.GetComponent<Rigidbody>().mass = 0.01f;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Destroy(projectile.GetComponent<Distractorscript>());

        if (rb != null)
        {
            rb.AddForce(playerR.transform.right * throwForce);
        }
        else
        {

        }

        currentdistract--;

        StartCoroutine(DestroyTime(projectile));
        StartCoroutine(CooldownThrow());
    }

    private IEnumerator CooldownThrow()
    {
        yield return new WaitForSeconds(throwrate);
        isthrowing = false;
    }

    private IEnumerator DestroyTime(GameObject projectile)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(projectile);
    }

    public void Deactivateboomboom()
    {
        gameObject.SetActive(false);
    }

    
}
