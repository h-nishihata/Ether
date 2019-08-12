using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBehaviour : MonoBehaviour
{
    public RectTransform[] targets;

    public void MoveTo(int targetNum)
    {
        transform.position = targets[targetNum].position;
    }
}
