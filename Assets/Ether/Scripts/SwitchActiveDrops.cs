using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveDrops : MonoBehaviour
{
    public SetParticleModels modelSetter;
    public Transform[] drops;


    public void SetActiveDrops(int num)
    {
        for (int i = 0; i < drops.Length; i++)
        {
            if (drops[i] == null)
                continue;
            drops[i].gameObject.SetActive(false);
        }
        if (drops[num] == null)
            return;
        drops[num].gameObject.SetActive(true);
        modelSetter.offsetPositions[num].y *= 4;
        drops[num].transform.localPosition = modelSetter.offsetPositions[num];
    }
}
