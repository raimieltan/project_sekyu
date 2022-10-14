using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeExample : MonoBehaviour
{
    public List<GameObject> ListCharacters = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     private void OnTriggerEnter(Collider other)
    {
        // CharacterController control = other.gameObject.transform.root.GetComponent<CharacterController>();

        // if(control != null)
        // {
        //     if (!ListCharacters.Contains(control))
        //     {
        //         ListCharacters.Add(control);
        //     }
        // }
        // if (other.gameObject.name == "PlayerArmature")
        // {
        //     ListCharacters.Add(other.gameObject);
		// }
    }

     private void OnTriggerExit(Collider other)
    {
        // CharacterController control = other.gameObject.transform.root.GetComponent<CharacterController>();

        // if(control != null)
        // {
        //     if (!ListCharacters.Contains(control))
        //     {
        //         ListCharacters.Add(control);
        //     }
        // }
        // if (other.gameObject.name == "PlayerArmature")
        // {
        //     ListCharacters.Remove(other.gameObject);
		// }
    }
}
