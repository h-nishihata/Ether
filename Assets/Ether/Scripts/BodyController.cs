﻿using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour
{
    public Transform body;
    private Toggle toggle;
    public Button resetButton;
    public Transform[] pedestals;
    public int activePedestalID;


    private void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    public void EnableBody()
    {
        body.gameObject.SetActive(toggle.isOn);
        resetButton.interactable = true;
    }

    public void AdjustGroundLevel()
    {
        var yPos = pedestals[activePedestalID].transform.position.y;
        body.transform.localPosition = new Vector3(-2f, yPos, 1.8f);
    }
}