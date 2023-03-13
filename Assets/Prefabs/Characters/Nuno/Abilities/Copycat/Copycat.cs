using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using Photon.Pun;
using Photon.Realtime;

public class Copycat : Ability
{
// public GameObject panel;
// public bool isSwapped = false;

private Invisibility invisibility;
public GameObject[] _characters;
public int characterIndex;
private GameObject selectedCharacter;
public Avatar[] _avatars;
[SerializeField] public RuntimeAnimatorController animatorController;
private int avatarIndex;
private Animator _animator;
private Avatar selectedAvatar;
private float nextActiveAbilityTime = 0;
private StarterAssetsInputs starterAssetsInputs;
private IEnumerator coroutine;
public PhotonView view;
public ParticleSystem particleSystem;
public Color team1Color = Color.red;
public Color team2Color = Color.blue;
public Health health;
public TeamAura auraRef;

void Awake()
{
    cooldownTime = 3;
    nextFireTime = 0;
}

void Start()
{
    invisibility = GetComponent<Invisibility>();
    // if (_characters.Length != 0) {
    selectedCharacter = _characters[0];
    // }
    starterAssetsInputs = GetComponent<StarterAssetsInputs>();

    health = GetComponent<Health>();

    // _animator = this.gameObject.GetComponent<Animator>();

    for (int i = 0; i < _characters.Length; i++)
    {
        if (i != 0)
        {
            _characters[i].SetActive(false);
        }
    }

    _animator = this.gameObject.GetComponent<Animator>();

    // panel.gameObject.SetActive(false); 
    // Swap(0);        
}

void Update()
{

    // Debug.Log(_avatar);
    if (Time.time > nextFireTime)
    {
        // TODO: Add conditions for if attacked or attacking

        if (invisibility.isVisible)
        {
            if (starterAssetsInputs.firstAbility)
            {
                view.RPC("emitSwap",RpcTarget.All);
            }
        }
        // if (isSwapped) {
        //     Swap(0);
        // }  
    }
    else
    {
        // invisibility.enabled = true;
    }
}

[PunRPC]
void emitSwap() {
    Swap();
    coroutine = AddNewAnimator(0.01f);
    StartCoroutine(coroutine);
    nextFireTime = Time.time + cooldownTime;
    TriggerFireEvent();
}

public void Swap()
{
    if(characterIndex == 0) {
        auraRef.enabled = true;
    } else {
        auraRef.enabled = true;
    }

    if ((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "team1")
    {
        particleSystem.startColor = team1Color;
    }

    else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["team"] == "team2")
    {
        particleSystem.startColor = team2Color;
    }

    Destroy(_animator);

    characterIndex++;

    if (characterIndex == _characters.Length)
    {
        characterIndex = 0;
        auraRef.enabled = true;
        invisibility.enabled = true;
    }
    else
    {
        auraRef.enabled = false;
        invisibility.enabled = false;

    };

    selectedCharacter.SetActive(false);
    _characters[characterIndex].SetActive(true);

    selectedCharacter = _characters[characterIndex];
}


public void Revert() {
    coroutine = AddNewAnimator(0.01f);
    StartCoroutine(coroutine);
    view.RPC(nameof(emitRevert), RpcTarget.All);
    
    nextFireTime = Time.time + cooldownTime;
    TriggerFireEvent();
}

[PunRPC]
void emitRevert() 
{
    Destroy(_animator);
    auraRef.enabled = true;
    characterIndex = 0;
    selectedCharacter.SetActive(false);
    _characters[characterIndex].SetActive(true);

    selectedCharacter = _characters[characterIndex];
}

private IEnumerator AddNewAnimator(float delayTime)
{
    while (true)
    {
        yield return new WaitForSeconds(delayTime);

        Animator newAnim = this.gameObject.AddComponent<Animator>();
        newAnim.runtimeAnimatorController = animatorController;
        newAnim.avatar = selectedCharacter.GetComponent<Animator>().avatar;

        _animator = newAnim;
    }
}
}
