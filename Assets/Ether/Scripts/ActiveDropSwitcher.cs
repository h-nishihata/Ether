using UnityEngine;

/// <summary>
/// 各Boxに付いている，使用する3Dモデルのみを有効化するためのスクリプト.
/// </summary>
public class ActiveDropSwitcher : MonoBehaviour
{
    public DropModelSetter modelSetter;
    public Transform[] drops;
    private Vector3 offset = new Vector3();


    public void Trigger(int id)
    {
        for (int i = 0; i < drops.Length; i++)
        {
            if (drops[i] == null)
                continue;
            drops[i].gameObject.SetActive(false); // リセット.
        }
        if (drops[id] == null)
            return;

        drops[id].gameObject.SetActive(true);

        offset = modelSetter.offsetPositions[id];
        drops[id].transform.localPosition = offset;
        offset = Vector3.zero;
    }
}
