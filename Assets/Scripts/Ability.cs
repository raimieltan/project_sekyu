using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Ability : MonoBehaviourPun
{
    public delegate void AbilityFired();
    public event AbilityFired TriggerAbilityFired;

    public float cooldownTime;
    public float nextFireTime;

    protected void TriggerFireEvent()
    {
        var eh = TriggerAbilityFired;
        if (eh != null)
            eh();
    }

}
