using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textHints : MonoBehaviour
{
    public static bool textOn = false;
    public static int text = 0;
    public static string message;
    private float timer = 0.0f;
    Text Hint;

    // Start is called before the first frame update
    void Start()
    {
        Hint = GetComponent<Text>();
        timer = 0.0f;
        textOn = false;
        Hint.text = "The earth's environment has changed a lot after the meteorite impact.\n\n" +
                    "Resources have becoming rare and you need to collect them in order to survive on such earth. Try to find your way out when exploring the place.\n\n" +
                    "Beware of the wild, you might be attack by unknown creatures.";
    }

    // Update is called once per frame
    void Update()
    {
        if (textOn)
        {
            Hint.enabled = true;
            Hint.text = message;
            timer += Time.deltaTime;
        }

        if (timer >= 5)
        {
            textOn = false;
            Hint.enabled = false;
            timer = 0.0f;
        }

        if (text >= 1)
        {
            Hint.text = "     Arrow Keys = Move     Left Click = Shoot\n\n" +
                        "     Q = Throw Weapon      E = PickUp Weapon\n\n" +
                        "     Space = Jump            F = Open Door/Escape\n\n" +
                        "     Shift = Run                 Esc = Pause game\n\n" +
                        "     T = light up";
        }
        else 
        {
            Hint.text = "The earth's environment has changed a lot after the meteorite impact.\n\n" +
                   "Resources have becoming rare and you need to collect them in order to survive on such earth. Try to find your way out when exploring the place.\n\n" +
                   "Beware of the wild, you might be attack by unknown creatures.";
        }
    }
}
