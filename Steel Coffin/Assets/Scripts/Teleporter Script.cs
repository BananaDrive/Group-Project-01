using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TeleporterScript : MonoBehaviour
{
    public Vector3 teleportPos = new Vector3(0, 0, 0);
    public float activationRange = 1f;
    public Transform player;
    public TextMeshProUGUI activateText;
    private PlayerInventory playerInventory;
    public bool islvl2 = false;

    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {

        playerInventory = player.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory component not found on player.");
        }


        StartCoroutine(FadeIn());

    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= activationRange)
        {
            activateText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (islvl2)
                {
                    Collider objectCollider = GetComponent<Collider>();

                    if (objectCollider != null)
                    {
                        Destroy(objectCollider);
                    }
                }

                StartCoroutine(FadeOut());

                StartCoroutine(TP());
            }
        }
        else
        {
            activateText.gameObject.SetActive(false);
        }
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
        yield return new WaitForSeconds(2f);

        StartCoroutine(FadeIn());
    }

    public IEnumerator TP()
    {
        yield return new WaitForSeconds(1f);

        player.transform.position = teleportPos;
    }

    public void TriggerFadeOut()
    {
        StartCoroutine(FadeOut());
    }


}
