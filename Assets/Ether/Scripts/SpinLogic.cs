using UnityEngine;
using DG.Tweening;
 
public class SpinLogic : MonoBehaviour { 
    float lastX = 0.0f;
    float difX = 0.5f;
    int direction = 1;
    private bool b;
    public TurnPage pageSwitcher;
    public SetParticleModels modelSetter;
    private int pageID;

    private void Start()
    {
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<TurnPage>();
        pageID = modelSetter.pageID;
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID)
            return;

        if (Input.GetMouseButton(0))
        {
            difX = Mathf.Abs(lastX - Input.GetAxis ("Mouse Y"));

            if (lastX < Input.GetAxis ("Mouse Y"))
            {
                direction = -1;
                transform.Rotate(Vector3.right, -difX);
            }

            if (lastX > Input.GetAxis ("Mouse Y"))
            {
                direction = 1;
                transform.Rotate(Vector3.right, difX);
            }

            lastX = -Input.GetAxis ("Mouse Y");

            b = true;
        }
        else
        {
            if (!b)
                return;

            if (difX > 0f) difX -= 0.01f;
            if (difX < 0f) difX += 0.01f;

            transform.Rotate(Vector3.right, difX * direction);
        }
    }
}