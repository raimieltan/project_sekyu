using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Copycat : MonoBehaviour
{
    public GameObject panel;
    public List<Transform> possibleTransformations;
    public float cooldownTime = 10;
    public bool isSwapped = false;

    private Invisibility invisibility;
    private List<Transform> _characters;
    private int selectedCharacter = 0;
    private float nextActiveAbilityTime = 0;

    void Start()
    {
        invisibility = gameObject.GetComponent<Invisibility>();
        _characters = possibleTransformations;
        panel.gameObject.SetActive(false); 
        Swap(0);        
    }

    void Update()
    {
        if (Time.time > nextActiveAbilityTime)
        {
            // TODO: Add conditions for if attacked or attacking
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("C is pressed");

                if (invisibility.isVisible) {
                    ShowPanel();
                }
            }


            if (isSwapped) {
                Swap(0);
            }  
        }
    }

    public void Swap(int index)
    {
        selectedCharacter = index;
        EnableCharacter(index);

        nextActiveAbilityTime = Time.time + cooldownTime;
        isSwapped = index != 0;
    }

    void EnableCharacter(int index)
    {
        for (int characterPosition = 0; characterPosition < possibleTransformations.Count; characterPosition++)
        {
            Debug.Log("CHARACTER POSITION: " + characterPosition);
            Debug.Log(_characters[characterPosition].gameObject.name);

            Transform currentCharacter = _characters[characterPosition];
            currentCharacter.gameObject.SetActive(characterPosition == index);
        }
    }

    void ShowPanel()
    {
        panel.gameObject.SetActive(true);        
    }

    public void SelectCharacter(int index)
    {
        panel.gameObject.SetActive(false);
        selectedCharacter = index;
        Swap(index);
    }
}
