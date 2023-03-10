using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour
{
    public Color changeColor;
    public Color changeColor2;
    public Color normalColor;
    public Button teamBtn;
    public Button teamBtn2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBtnColor()
    {
        ColorBlock btn = teamBtn.colors;
        ColorBlock btn2 = teamBtn2.colors;
        btn.selectedColor = changeColor;
        btn2.selectedColor = normalColor;
        teamBtn.colors = btn;
        teamBtn2.colors = btn2;
    }

    public void ChangeBtnColor2()
    {
        ColorBlock btn2 = teamBtn2.colors;
        ColorBlock btn = teamBtn.colors;
        btn2.selectedColor = changeColor;
        btn.selectedColor = normalColor;
        teamBtn2.colors = btn2;
        teamBtn.colors = btn;
    }
}
