using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActiveDrops : MonoBehaviour
{
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
        //var yOffset = num == 0 ? 0 : -668;
        //drops[num].transform.localPosition = new Vector3(0, yOffset, 0);
    }
}
