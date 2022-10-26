using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MultiplayerUI : MonoBehaviour
{
  [SerializeField] private Button serverBtn;
  [SerializeField] private Button hostBtn;
  [SerializeField] private Button clientBtn;

  // [SerializeField]
  // private TextMeshProGUI playersInGameText;
  private void Update()
  {
    // playersInGameText.text = $"Players in Game: {PlayersManager.Instance.PlayersInGame}";
  }

  // Start is called before the first frame update
  private void Awake()
  {
    Cursor.visible = true;

  }

  private void Start()
  {
    serverBtn.onClick.AddListener(() =>
      {
        NetworkManager.Singleton.StartServer();
      });

    hostBtn.onClick.AddListener(() =>
    {

      if (NetworkManager.Singleton.StartHost())
      {
        Debug.Log("Host");
      };
    });


    clientBtn.onClick.AddListener(() =>
    {
      NetworkManager.Singleton.StartClient();
    });
  }
}
