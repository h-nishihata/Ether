using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisOffset : MonoBehaviour
{
    public Vector3 position;

    public void Trigger()
    {
        this.transform.position = position;
    }
}
