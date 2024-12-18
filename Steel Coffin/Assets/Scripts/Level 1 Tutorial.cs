using UnityEngine;
using TMPro;
public class Level1Tutorial : MonoBehaviour
{
    public TextMeshProUGUI TutText;
    public GameObject tutText;
    public float Tut = 5;

    void Start()
    {
        Tut = 0;
        tutText.SetActive(true);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tut++;
        }

        if (Tut == 0)
        {
            TutText.text = "Press E for next Tip";
        }

        if (Tut == 1)
        {
            TutText.text = "WASD to walk, Shift to sprint!";
        }

        if (Tut == 2)
        {
            TutText.text = "throw objects with your mouse, the buttons corralate to direction!";
        }

        if (Tut == 3)
        {
            TutText.text = "there is a shack over there";
        }

        if (Tut == 4)
        {
            TutText.text = "press E to hide in the dumpster (you can also hide in cabinets";
        }

        if (Tut == 5)
        {
            TutText.text ="This is the end of the tutorial, happy Exploring";
        }

        if (Tut == 6)
        {
            TutText.text = "";
        }
    }


}
