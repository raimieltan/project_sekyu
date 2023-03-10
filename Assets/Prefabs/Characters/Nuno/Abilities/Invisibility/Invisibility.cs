using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;

public class Invisibility : Ability
{    
    private float invisibilityDuration = 5;

    private Copycat copycat;
    public bool isVisible;
    public Material transparentMaterial;
    private StarterAssetsInputs starterAssetsInputs;
    // public Image abilityImage;
    // private Copycat copycat;
    private List<Material[]> _materials;
    public PhotonView view;

    void Start()
    {
        cooldownTime = 8;
        nextFireTime = 0;
        isVisible = true;
        // copycat = gameObject.GetComponent<Copycat>();
        // abilityImage.fillAmount = 0;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        copycat = GetComponent<Copycat>();
        _materials = GetAllObjectsWithMeshMaterial();
    }

    void Update()
    {

        if (Time.time > nextFireTime)
        {
            if (copycat.characterIndex == 0 && starterAssetsInputs.secondAbility)
            {
                view.RPC("emitInvi",RpcTarget.All);

                // starterAssetsInputs.secondAbility = false;           
                // if (copycat.isSwapped)
                // {
                //     // TODO: Show panel? Ask Sean
                //     copycat.Swap(0);
                // }
                // nextInvisibilityTime = Time.time + cooldownTime;
                // abilityImage.fillAmount = 1;
                // GoInvisible();
            }
            //  else {
            // if (!copycat.isSwapped)
            // {
            //     RemoveInvisibility();
            // }
            // RemoveInvisibility();
            // starterAssetsInputs.secondAbility = false;
            // }
        }

        view.RPC("emitRemove",RpcTarget.All);

        // else
        // {
        //     abilityImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;

        //     if (abilityImage.fillAmount <= 0)
        //     {
        //         abilityImage.fillAmount = 0;
        //         RemoveInvisibility();
        //     }
        // }
    }

    [PunRPC]
    void emitRemove() {
        if (Time.time > (nextFireTime - (cooldownTime - invisibilityDuration)))
        {
           RemoveInvisibility();
        
        }
    }
        

    [PunRPC]
    void emitInvi(){
        Debug.Log(copycat.characterIndex);
        nextFireTime = Time.time + cooldownTime;
        GoInvisible();
           
        TriggerFireEvent();
    }

    private List<Material[]> GetAllObjectsWithMeshMaterial()
    {
        List<Material[]> materials = new List<Material[]>();

        Transform[] limbs = GetComponentsInChildren<Transform>();

        foreach (Transform child in limbs)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                materials.Add(renderer.materials);
            }
            else
            {
                materials.Add(null);
            }
        }

        return materials;
    }

    [PunRPC]
    private void RemoveInvisibility()
    {
        Transform[] limbs = GetComponentsInChildren<Transform>();

        for (int child = 0; child < limbs.Length; child++)
        {
            Renderer renderer = limbs[child].GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.materials = _materials[child];
            }
        }

        isVisible = true;
    }

    [PunRPC]
    private void GoInvisible()
    {
        Transform[] limbs = GetComponentsInChildren<Transform>();
        Material[] _replacement = new Material[1] { transparentMaterial };

        for (int child = 0; child < limbs.Length; child++)
        {
            Renderer renderer = limbs[child].GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.materials = _replacement;
            }
        }

        isVisible = false;
    }
}
