using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPlacement : MonoBehaviour
{
    [Header("Powerup Settings")]

    [Space]
    [SerializeField] GameObject[] powerupPrefabs;
    private int powerupDensity;

    [SerializeField] float powerupMinHeight;
    [SerializeField] float powerupMaxHeight;
    [SerializeField] Vector2 powerupXRange;
    [SerializeField] Vector2 powerupZRange;

    [Space]

    [Header("Tree Settings")]

    [Space]
    public GameObject[] treePrefab;
    [SerializeField] int treeDensity;

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

    [Header("Environment Settings")]

    [Space]
    [SerializeField] GameObject[] environmentPrefab;
    [SerializeField] int environmentDensity;

    [SerializeField] float environmentMinHeight;
    [SerializeField] float environmentMaxHeight;
    [SerializeField] Vector2 environmentXRange;
    [SerializeField] Vector2 environmentZRange;

    [Space]

    [Header("Grass Settings")]
    [Space]
    [SerializeField] GameObject grassPrefab;
    [SerializeField] int grassDensity;

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
        GenerateObjects();
    }

    public void GenerateObjects() {
        Clear();

        GenerateGrass();

        GeneratePowerUps();

        GenerateTrees();

        GenerateEnvironment();
    }

    public void GenerateTrees() {
        for (int i = 0; i < treePrefab.Length; i++) {
            for (int j = 0; j < treeDensity; j++) {
                float sampleX = Random.Range(treeXRange.x, treeXRange.y);
                float sampleY = Random.Range(treeZRange.x, treeZRange.y);
                Vector3 rayStart = new Vector3(sampleX, treeMaxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;
                
                if (hit.point.y < treeMinHeight)
                    continue;

                GameObject instantiatedPrefab = Instantiate(treePrefab[i], transform);
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
    }

    public void GenerateEnvironment() {
        for (int i = 0; i < environmentPrefab.Length; i++) {
            for (int j = 0; j < environmentDensity; j++) {
                float sampleX = Random.Range(environmentXRange.x, environmentXRange.y);
                float sampleY = Random.Range(environmentZRange.x, environmentZRange.y);
                Vector3 rayStart = new Vector3(sampleX, environmentMaxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;
                
                if (hit.point.y < environmentMinHeight)
                    continue;

                GameObject instantiatedPrefab = Instantiate(environmentPrefab[i], transform);
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
    }

    public void GenerateGrass() {
        for (int i = 0; i < grassDensity; i++) {
            float sampleX = Random.Range(grassXRange.x, grassXRange.y);
            float sampleY = Random.Range(grassZRange.x, grassZRange.y);
            Vector3 rayStart = new Vector3(sampleX, grassMaxHeight, sampleY);

            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;
            
            if (hit.point.y < grassMinHeight)
                continue;

            GameObject instantiatedPrefab = Instantiate(grassPrefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
    }

    public void GeneratePowerUps() {
        powerupDensity = 5;
    
        for (int i = 0; i < powerupPrefabs.Length; i++) {
            for (int j = 0; j < powerupDensity; j++) {
                float sampleX = Random.Range(powerupXRange.x, powerupXRange.y);
                float sampleY = Random.Range(powerupZRange.x, powerupZRange.y);
                Vector3 rayStart = new Vector3(sampleX, powerupMaxHeight, sampleY);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                    continue;
                
                if (hit.point.y < powerupMinHeight)
                    continue;

                GameObject instantiatedPrefab = Instantiate(powerupPrefabs[i], transform);
                instantiatedPrefab.transform.position = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                instantiatedPrefab.transform.localScale = powerupPrefabs[i].transform.localScale;
                // instantiatedPrefab.transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }
    }

    // public void SpawnPowerups(GameObject _object, Transform _transform, RaycastHit _hit, Vector3 _scale) {
    //      GameObject instantiatedPrefab = Instantiate(_object, _transform);
    //         instantiatedPrefab.transform.position = new Vector3(_hit.point.x, _hit.point.y + 1f, _hit.point.z);
    //         instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
    //         instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, _hit.normal), rotateTowardsNormal);
    //         instantiatedPrefab.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
    // }

    public void Clear() {
        while (transform.childCount != 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
