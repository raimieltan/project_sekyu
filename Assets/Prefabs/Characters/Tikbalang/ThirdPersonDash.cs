using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ThirdPersonDash : MonoBehaviour
{
    //public ThirdPersonController moveScript;
    public CharacterController cc;
    public float dashSpeed;
    public float dashTime;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.secondAbility)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            cc.Move(transform.forward * dashSpeed * Time.deltaTime);

            yield return null;
        }

    }
}