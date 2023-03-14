using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public List<string> stateNames;

    public Animator animator; // Reference to Animator component

    public float minAnimationTime = 3f; // Minimum time before playing a new animation

    public float maxAnimationTime = 8f; // Maximum time before playing a new animation

    void Start()
    {
        StartCoroutine(PlayRandomAnimation());
    }

    IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            // Pick a random state name from the list
            int randomIndex = Random.Range(0, stateNames.Count-1);
            string stateName = stateNames[randomIndex];

            // Play the animation corresponding to the state name
            animator.Play (stateName,0);

            // Wait for a random time before playing the next animation
            float randomWaitTime = Random.Range(minAnimationTime, maxAnimationTime);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}
