using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveDrop : MonoBehaviour
{
    public SetParticleModels modelSetter;
    public Transform[] drops;
    private Vector3 tempOffset = new Vector3();


    public void Trigger(int num)
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

        tempOffset = modelSetter.offsetPositions[num];
        tempOffset.y *= 4;
        drops[num].transform.localPosition = tempOffset;
        tempOffset = Vector3.zero;
    }
}
