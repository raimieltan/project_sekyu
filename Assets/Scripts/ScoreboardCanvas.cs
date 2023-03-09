using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ScoreboardCanvas : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private GameObject scoreBoardTable;
    public Scoreboard scoreboard;

    void Start()
    {
        starterAssetsInputs = transform.root.GetComponent<StarterAssetsInputs>();
        scoreboard = scoreBoardTable.GetComponent<Scoreboard>();
    }

    void Update()
    {
        if(starterAssetsInputs.openScoreboard){
            scoreboard.UpdateScoreboard();
            scoreBoardTable.SetActive(true);
        } else {
            scoreboard.UpdateScoreboard();
            scoreBoardTable.SetActive(false);
        }
    }
}

