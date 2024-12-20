using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI AmountHeld;

    public bool distracttriggered = false;
    GameManager Gm;

    public AudioSource Cracker;

    private void Start()
    {
#pragma warning disable CS0618 
        Gm = FindObjectOfType<GameManager>();
#pragma warning restore CS0618 

        
    }

    void Update()
    {
        if (Gm != null && Gm.IsPaused)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && currentdistract > 0 && !isthrowing)
        {
            distracttriggered = true;
            ThrowGrenadeL();
            isthrowing = true;
            Cracker.Play();
        }

        if (Input.GetMouseButtonDown(1) && currentdistract > 0 && !isthrowing)
        {
            distracttriggered = true;
            ThrowGrenadeR();
            isthrowing = true;
            Cracker.Play();
        }

        AmountHeld.text = "Cracklers: " + currentdistract + "/" + maxdistract;
    }

    private void ThrowGrenadeL()
    {
        GameObject projectile = Instantiate(Distractprojectile, playerL.position, Quaternion.Euler(0, 0, 0));

        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.mass = 0.01f;

        SphereCollider collider = projectile.AddComponent<SphereCollider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(projectile.GetComponent<Distractorscript>());
        rb.AddForce(-playerL.transform.right * throwForce);

        currentdistract--;
        

        StartCoroutine(DestroyTime(projectile));
        StartCoroutine(CooldownThrow());
    }

    private void ThrowGrenadeR()
    {
        GameObject projectile = Instantiate(Distractprojectile, playerR.position, Quaternion.Euler(0, 0, 0));

        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.mass = 0.01f;

        SphereCollider collider = projectile.AddComponent<SphereCollider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(projectile.GetComponent<Distractorscript>());
        rb.AddForce(playerR.transform.right * throwForce);

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