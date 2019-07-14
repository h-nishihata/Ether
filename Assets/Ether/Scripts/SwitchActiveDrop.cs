using UnityEngine;

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
        tempOffset.y *= 4;
        drops[id].transform.localPosition = tempOffset;
        tempOffset = Vector3.zero;
    }
}
