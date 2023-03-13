using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;

public class Invisibility : Ability
{    
    private float invisibilityDuration = 5;

    private Copycat copycat;
    public bool isVisible;
    public Material transparentMaterial;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController player;

    // public Image abilityImage;
    // private Copycat copycat;
    private List<Material[]> _materials;
    public PhotonView view;

    void Start()
    {
        cooldownTime = 9;
        nextFireTime = 0;
        isVisible = true;
        player = GetComponent<ThirdPersonController>();
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
                view.RPC("GoInvisible",RpcTarget.All);

            }
        }
    }
        

    // [PunRPC]
    // void emitInvi(){
    //     Debug.Log(copycat.characterIndex);
        
    // }

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
    IEnumerator RemoveInvisibility()
    {
        yield return new WaitForSeconds(8f);

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
        nextFireTime = Time.time + cooldownTime;
        TriggerFireEvent();

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

        StartCoroutine(RemoveInvisibility());
    }
}
