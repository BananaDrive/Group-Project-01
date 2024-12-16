using UnityEngine;
using TMPro;
using System.Collections;

public class Notes : MonoBehaviour
{
    //NOTES
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public Transform Player;
    public float NoteRange1 = 3;
    public float NoteRange2 = 3;
    public float NoteRange3 = 3;

    public float NoteTimer1 = 1;
    public float NoteTimer2 = 5;
    public float NoteTimer3 = 5;

    public GameObject NoteText1;
    public GameObject NoteText2;
    public GameObject NoteText3;

    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.position, transform.position) <= NoteRange1)
        {
            NoteText1.SetActive(true);

           // StartCoroutine(NoteTime1);
        }
    }






    //private IEnumerator NoteTime1()
   // {
       // yield return new WaitForSeconds(throwrate);
       // isthrowing = false;
    //}
}
