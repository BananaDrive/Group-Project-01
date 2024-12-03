using System.Collections;
using UnityEngine;

public class Distractorscript : MonoBehaviour
{
    public float throwForce = .2f;
    public float fuseTime = 3f;
    public GameObject Distractprojectile;
    public Transform player;
    public Transform playerlookdirection;
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
            ThrowGrenade();
            isthrowing = true;
        }


        if (currentdistract <= 0)
        {
            Deactivateboomboom();
        }

    }

    private void ThrowGrenade()
    {

        GameObject projectile = Instantiate(Distractprojectile, playerlookdirection.position, playerlookdirection.rotation * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(playerlookdirection.transform.forward * throwForce, ForceMode.Impulse);
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
