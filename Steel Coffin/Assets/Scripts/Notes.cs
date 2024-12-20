using UnityEngine;
using TMPro;
using System.Collections;

public class Notes : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public GameObject noteObject;
        public GameObject noteText;
        public float Range = 3f;
        public float showTime = 5f;
    }
    
    public List<Note> notes = new List<Note>();
    public Transform player;
    public AudioSource NoteAudio;
    

    void Update()
    {
        foreach (var note in notes)
        {
            if (Input.GetKeyDown(KeyCode.E) && IsPlayerInRange(note))
            {
                DisplayNoteMessage(note);
                break;
            }
        }
    }
    
    bool IsPlayerInRange(Note note)
    {
        return Vector3.Distance(player.position, note.noteObject.transform.position) <= note.Range;
    }
    
    void DisplayNoteMessage(Note note)
    {
        note.noteText.SetActive(true);
        NoteAudio.Play();
        StartCoroutine(HideNoteMessage(note));
    }
    
    private IEnumerator HideNoteMessage(Note note)
    {
        yield return new WaitForSeconds(note.showTime);
        note.noteText.SetActive(false);
    }
}
