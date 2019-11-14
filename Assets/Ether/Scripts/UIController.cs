using UnityEngine;

public class UIController : MonoBehaviour
{
    public Transform UIPanel;
    public bool isActive = true;

    private void Update()
    {
        // UIパネル非表示時に，画面をタップすると再度表示する.
        if (!isActive && Input.GetMouseButtonDown(0))
            switchUIPanel(true);
    }

    /// <summary>
    /// UIパネルの表示/非表示の切替をする.
    /// </summary>
    public void switchUIPanel(bool status)
    {
        UIPanel.gameObject.SetActive(status);
        isActive = status;
    }
}