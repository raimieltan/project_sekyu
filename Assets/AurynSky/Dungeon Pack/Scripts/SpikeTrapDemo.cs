using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrapDemo : MonoBehaviour {

    //This script goes on the SpikeTrap prefab;

    public Animator spikeTrapAnim; //Animator for the SpikeTrap;
    public List<GameObject> ListCharacters = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
        ListCharacters.Clear();
    }

    void Awake()
    {
        //get the Animator component from the trap;
        spikeTrapAnim = GetComponent<Animator>();
        //start opening and closing the trap for demo purposes;
        // StartCoroutine(OpenCloseTrap());

        // spikeTrapAnim.SetTrigger("open");
    }

    void Update()
    {
        if(ListCharacters.Count == 1 && spikeTrapAnim.enabled)
        {
            spikeTrapAnim.SetTrigger("open");
            spikeTrapAnim.StopPlayback();
        } else
        {
            spikeTrapAnim.SetTrigger("close");
        }
    }


    // IEnumerator OpenCloseTrap()
    // {
    //     //play open animation;
    //     spikeTrapAnim.SetTrigger("open");
    //     //wait 2 seconds;
    //     yield return new WaitForSeconds(2);
    //     //play close animation;
    //     spikeTrapAnim.SetTrigger("close");
    //     //wait 2 seconds;
    //     yield return new WaitForSeconds(2);
    //     // Do it again;
    //     // if(ListCharacters.Count == 1)
    //     // {
    //     //     spikeTrapAnim.SetTrigger("open");
    //     // } else
    //     // {
    //     //     spikeTrapAnim.SetTrigger("close");
    //     // }
    //     StartCoroutine(OpenCloseTrap());
    // }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("OnCollisionEnter: " + col.ToString());
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
        if (other.gameObject.name == "PlayerArmature")
        {
            ListCharacters.Add(other.gameObject);
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerArmature")
        {
            ListCharacters.Remove(other.gameObject);
		}
    }

}