using UnityEngine;

/// <summary>
/// 各Boxに付いている，使用する3Dモデルのみを有効にするためのスクリプト.
/// </summary>
public class SwitchActiveDrop : MonoBehaviour
{
    public SetDropModels setDropModels;
    public Transform[] drops;
    private Vector3 tempOffset = new Vector3();


    public void Trigger(int id)
    {
        for (int i = 0; i < drops.Length; i++)
        {
            if (drops[i] == null)
                continue;
            drops[i].gameObject.SetActive(false);
        }
        if (drops[id] == null)
            return;
        drops[id].gameObject.SetActive(true);

        tempOffset = setDropModels.offsetPositions[id];
        drops[id].transform.localPosition = tempOffset;
        tempOffset = Vector3.zero;
    }
}
