using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  

public class ObjectPlacement : MonoBehaviour
{
    [Header("Trees")]
    public GameObject[] treePrefab;

    [Header("Tree Raycast Settings")]
    [SerializeField] int treeDensity;

    [Space]

    [SerializeField] float treeMinHeight;
    [SerializeField] float treeMaxHeight;
    [SerializeField] Vector2 treeXRange;
    [SerializeField] Vector2 treeZRange;

    [Space]

    // [Header("Buildings")]
    // [SerializeField] GameObject buildingPrefab;

    // [Header("Building Raycast Settings")]
    // [SerializeField] int buildingDensity;

    // [Space]

    // [SerializeField] float buildingMinHeight;
    // [SerializeField] float buildingMaxHeight;
    // [SerializeField] Vector2 buildingXRange;
    // [SerializeField] Vector2 buildingZRange;

    // [Space]

    [Header("Environment")]
    [SerializeField] GameObject[] environmentPrefab;

    [Header("Environment Raycast Settings")]
    [SerializeField] int environmentDensity;

    [Space]

    [SerializeField] float environmentMinHeight;
    [SerializeField] float environmentMaxHeight;
    [SerializeField] Vector2 environmentXRange;
    [SerializeField] Vector2 environmentZRange;

    [Space]

    [Header("Grass")]
    [SerializeField] GameObject grassPrefab;

    [Header("Environment Raycast Settings")]
    [SerializeField] int grassDensity;

    [Space]

    [SerializeField] float grassMinHeight;
    [SerializeField] float grassMaxHeight;
    [SerializeField] Vector2 grassXRange;
    [SerializeField] Vector2 grassZRange;

    [Space]

    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;

    public void Start() {
        Generate();
    }

    public void Generate() {
        Clear();

        //GENERATE TREES
        for (int i = 0; i < treePrefab.Length; i++) {
            for (int j = 0; j < treeDensity; j++) {
                float sampleX = Random.Range(treeXRange.x, treeXRange.y);
                float sampleY = Random.Range(treeZRange.x, treeZRange.y);
                Vector3 rayStart = new Vector3(sampleX, treeMaxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;
                
                if (hit.point.y < treeMinHeight)
                    continue;

                GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab[i], transform);
                instantiatedPrefab.transform.position = hit.point;
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                instantiatedPrefab.transform.localScale = new Vector3(
                    Random.Range(minScale.x, maxScale.x),
                    Random.Range(minScale.y, maxScale.y),
                    Random.Range(minScale.z, maxScale.z)
                );
            }
        }

        // GENERATE ENVIRONMENT
        for (int i = 0; i < environmentPrefab.Length; i++) {
            for (int j = 0; j < environmentDensity; j++) {
                float sampleX = Random.Range(environmentXRange.x, environmentXRange.y);
                float sampleY = Random.Range(environmentZRange.x, environmentZRange.y);
                Vector3 rayStart = new Vector3(sampleX, environmentMaxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;
                
                if (hit.point.y < environmentMinHeight)
                    continue;

                GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(environmentPrefab[i], transform);
                instantiatedPrefab.transform.position = hit.point;
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                instantiatedPrefab.transform.localScale = new Vector3(
                    0.3f,
                    0.3f,
                    0.3f
                );
            }
        }

        // GENERATE GRASS
        for (int i = 0; i < grassDensity; i++) {
            float sampleX = Random.Range(grassXRange.x, grassXRange.y);
            float sampleY = Random.Range(grassZRange.x, grassZRange.y);
            Vector3 rayStart = new Vector3(sampleX, grassMaxHeight, sampleY);

            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;
            
            if (hit.point.y < grassMinHeight)
                continue;

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(grassPrefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
    }

    public void Clear() {
        while (transform.childCount != 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
