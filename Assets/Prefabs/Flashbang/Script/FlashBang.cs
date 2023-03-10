using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class FlashBang : MonoBehaviour
{
    public Team teamOrigin;

    [SerializeField]
    private AudioClip bangClip;

    [SerializeField]
    private AudioClip whiteNoiseClip;

    [SerializeField]
    private float throwSpeed = 10f;

    private Image[] whiteImages = new Image[0];

    private Rigidbody rigidbodyComponent;

    private ParticleSystem flashParticle;

    private MeshRenderer meshRenderer;

    private AudioSource whiteNoiseAudioSource;

    private AudioSource bangAudioSource;

    public PhotonView view;

    public void SetThrowDirection(Vector3 throwDirection)
    {
        // Raise an event to notify the server that the player has thrown the flashbang
        view.RPC("ThrowFlashbang", RpcTarget.All, throwDirection);
    }

    [PunRPC]
    void ThrowFlashbang(Vector3 throwDirection)
    {
        // Set the velocity of the rigidbody to throw the object in front of the player
        rigidbodyComponent.velocity = throwDirection * throwSpeed;
    }

    private void InitAudioSource()
    {
        // Add an AudioSource component for the "bang" clip and set its properties
        bangAudioSource = gameObject.AddComponent<AudioSource>();
        bangAudioSource.clip = bangClip;
        bangAudioSource.loop = false;
        bangAudioSource.playOnAwake = false;

        // Add an AudioSource component for the "whiteNoise" clip and set its properties
        whiteNoiseAudioSource = gameObject.AddComponent<AudioSource>();
        whiteNoiseAudioSource.clip = whiteNoiseClip;
        whiteNoiseAudioSource.loop = false;
        whiteNoiseAudioSource.playOnAwake = false;

        // Set the positions of the audio sources to match the game object
        bangAudioSource.transform.position = transform.position;
        whiteNoiseAudioSource.transform.position = transform.position;
    }

    private void InitWhiteFlashImage(Canvas canvas)
    {
        // Create a new GameObject for the image
        GameObject imageObject = new GameObject("White Flash Image");
        imageObject.transform.SetParent(canvas.transform); // Set the canvas as the parent

        // Add a RectTransform component to the image object
        RectTransform rectTransform = imageObject.AddComponent<RectTransform>();
        rectTransform.localScale = Vector3.one; // Set the scale to (1, 1, 1) to match the canvas

        // Add an Image component to the image object and set its color
        Image image = imageObject.AddComponent<Image>();
        image.color = Color.white;

        // Set the position and size of the image using the RectTransform
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;

        image.enabled = false;

        // Resize the array to add one more element
        Array.Resize(ref whiteImages, whiteImages.Length + 1);

        // Create a new Image object and add it to the last element of the array
        whiteImages[whiteImages.Length - 1] = image;
    }

    private void Awake()
    {
        InitAudioSource();
        rigidbodyComponent = gameObject.GetComponent<Rigidbody>();
        // view = gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        flashParticle = gameObject.GetComponent<ParticleSystem>();

        StartCoroutine(WhiteFade());
    }

    private IEnumerator WhiteFade()
    {
        yield return new WaitForSeconds(4f);

        flashParticle.Play();
        bangAudioSource.Play();
        whiteNoiseAudioSource.Play();
        meshRenderer.enabled = false;

        foreach (Image whiteImage in whiteImages)
        {
            whiteImage.enabled = true;
            whiteImage.color = new Vector4(1, 1, 1, 1);
            float FadeSpeed = 1f;
            float Modifier = 0.01f;
            float WaitTime = 0;

            for (int i = 0; whiteImage.color.a > 0; i++)
            {
                whiteImage.color = new Vector4(1, 1, 1, FadeSpeed);
                FadeSpeed = FadeSpeed - 0.025f;
                Modifier = Modifier * 1.5f;
                WaitTime = 0.5f - Modifier;
                if (WaitTime < 0.1f) WaitTime = 0.1f;
                whiteNoiseAudioSource.volume -= 0.05f;
                yield return new WaitForSeconds(WaitTime);
            }
            Destroy(whiteImage.gameObject);
        }

        whiteNoiseAudioSource.Stop();
        whiteNoiseAudioSource.volume = 1;

        // Remove after
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // view.RPC("RPC_InitOnFacingPlayers", RpcTarget.All, other);
            // view.RPC(nameof(RPC_InitOnFacingPlayers), RpcTarget.All, other);
            view.RPC(nameof(RPC_InitOnFacingPlayers), PhotonNetwork.LocalPlayer, other);
        }
    }

    [PunRPC]
    public void RPC_InitOnFacingPlayers(Collider other)
    {

        StartCoroutine(InitOnFacingPlayers(other));
    }

    private IEnumerator InitOnFacingPlayers(Collider other)
    {
        yield return new WaitForSeconds(3.5f);

        // Canvas playerCanvas = other.GetComponentInChildren<Canvas>();
        TeamTag teamTag = other.GetComponent<TeamTag>();

        // Get the GameObject associated with the collider
        GameObject playerObject = other.gameObject;

        // Get the PhotonView component attached to the GameObject
        PhotonView photonView = playerObject.GetComponent<PhotonView>();
        Debug.Log("Player will get blind: " + photonView.Owner.NickName);

        Canvas playerCanvas = other.GetComponentInChildren<Canvas>();
        // Get the "Geometry" child object's transform and calculate the direction vector
        Transform geometryTransform = playerObject.transform.Find("Geometry");
        Vector3 direction = transform.position - geometryTransform.position;
        float angle = Vector3.Angle(direction, geometryTransform.forward);
        Debug.Log("angle: " + angle);

        if (photonView.IsMine)
        {
            // Check if the angle is within the allowed angle range
            if (angle <= 60f)
            {
                InitWhiteFlashImage(playerCanvas);
            }
        }
        else
        {
            Debug.LogWarning("Could not find player canvas or team tag.");
        }
    }
}
