using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisibility : MonoBehaviour
{
    public float cooldownTime = 3;
    private float nextActiveAbilityTime = 0;
    private List<Material[]> _materials;

    public Material transparentMaterial;
    public Image abilityImage;

    void Start()
    {
        abilityImage.fillAmount = 0;
        _materials = GetAllObjectsWithMeshMaterial();
    }

    void Update()
    {
        if (Time.time > nextActiveAbilityTime)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("I is pressed");
                nextActiveAbilityTime = Time.time + cooldownTime;
                abilityImage.fillAmount = 1;
                GoInvisible();
            } else {
                RemoveInvisibility();
            }
        }
        else
        {
            abilityImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;

            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                RemoveInvisibility();
            }
        }
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
            } else {
                materials.Add(null);
            }
        }

        return materials;
    }

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
    }

    private void GoInvisible()
    {
        Transform[] limbs = GetComponentsInChildren<Transform>();
        Material[] _replacement = new Material[1]{ transparentMaterial };

        for (int child = 0; child < limbs.Length; child++)
        {
            Renderer renderer = limbs[child].GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.materials = _replacement;
            }
        }
    }
}
