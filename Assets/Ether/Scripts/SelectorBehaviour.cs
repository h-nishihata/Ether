using UnityEngine;

public class SelectorBehaviour : MonoBehaviour
{
    public RectTransform[] targets;
    public AudioManager audioManager;

    public void MoveTo(int targetNum)
    {
        transform.position = targets[targetNum].position;
        audioManager.Play(0);
    }
}
