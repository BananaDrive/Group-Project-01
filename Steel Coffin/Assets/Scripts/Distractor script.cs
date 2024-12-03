using System.Collections;
using UnityEngine;

public class Distractorscript : MonoBehaviour
{
    public float throwForce = 5f;
    public float fuseTime = 3f;
    public GameObject Distractprojectile;
    public Transform player;
    public Transform playerR;
    public Transform playerL;
    public int currentdistract = 3;
    public int maxdistract = 4;
    public bool isthrowing = false;
    public float throwrate = 2f;
    public bool canthrow = true;

    public bool distracttriggered = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && currentdistract > 0 && !isthrowing)
        {
            distracttriggered = true;
            ThrowGrenadeL();
            isthrowing = true;
        }

        if (Input.GetMouseButton(1) && currentdistract > 0 && !isthrowing)
        {
            distracttriggered = true;
            ThrowGrenadeR();
            isthrowing = true;
        }


    }

    private void ThrowGrenadeL()
    {

        GameObject projectile = Instantiate(Distractprojectile, playerL.position, playerL.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(-playerL.transform.right * throwForce);
        }
        else
        {

        }

        currentdistract--;

        StartCoroutine(CooldownThrow());
    }

    private void ThrowGrenadeR()
    {

        GameObject projectile = Instantiate(Distractprojectile, playerR.position, playerR.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(playerR.transform.right * throwForce);
        }
        else
        {

        }

        currentdistract--;

        StartCoroutine(CooldownThrow());
    }

    private IEnumerator CooldownThrow()
    {
        yield return new WaitForSeconds(throwrate);
        isthrowing = false;
    }

    public void Deactivateboomboom()
    {
        gameObject.SetActive(false);
    }
}
