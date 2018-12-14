using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private GameObject[] buttons;

    public static ButtonController instance;

    Color selected;
    Color unselected;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        buttons = GameObject.FindGameObjectsWithTag("UIButton");
        selected = Color.cyan;
        unselected = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonAttack()
    {
        GameController.instance.playerAction = GameController.PlayerAction.Attack;
        UnselectAll();
        foreach (GameObject b in buttons)
        {
            if(b.name == "ButtonAttack")
                b.GetComponent<Image>().color = selected;
        }

    }

    public void ButtonMagic()
    {
        GameController.instance.playerAction = GameController.PlayerAction.Magic;
        UnselectAll();
        foreach (GameObject b in buttons)
        {
            if (b.name == "ButtonMagic")
                b.GetComponent<Image>().color = selected;
        }
    }

    public void ButtonDefend()
    {
        GameController.instance.playerAction = GameController.PlayerAction.Defend;
        UnselectAll();
        foreach (GameObject b in buttons)
        {
            if (b.name == "ButtonDefend")
                b.GetComponent<Image>().color = selected;
        }
    }

    public void ButtonEscape()
    {
        GameController.instance.playerAction = GameController.PlayerAction.Escape;
        UnselectAll();
        foreach (GameObject b in buttons)
        {
            if (b.name == "ButtonEscape")
                b.GetComponent<Image>().color = selected;
        }
    }

    public void UnselectAll()
    {
        foreach(GameObject b in buttons)
        {
            b.GetComponent<Image>().color = unselected;
        }
    }
}
