using System.Collections;
using UnityEngine;

public class EndAcivation : MonoBehaviour
{
    public GameManager Gm;
    PlayerInventory playerInventory;
    public Transform player;
    public Animator EndCutScene;
    public GameObject Silence;
    public float activaterange = 3f;
    public GameObject ChoiceTime;
    public GameObject CutScene;
    public GameObject video;
    public GameObject Crap;

    void Start()
    {
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= activaterange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Silence.SetActive(false);
                Crap.SetActive(false);
                CutScene.SetActive(true);
                StartCoroutine(Choices());
            }
        }
    }


    private IEnumerator Choices()
    {
        yield return new WaitForSeconds(3.5f);
        ChoiceTime.SetActive(true);
        video.SetActive(false);
    }
}
