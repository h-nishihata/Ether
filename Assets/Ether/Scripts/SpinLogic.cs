using UnityEngine;
 
public class SpinLogic : MonoBehaviour { 
    float lastY = 0.0f;
    float difY = 0.5f;
    int direction = 1;
    private bool userHasTouched;
    public TurnPage pageSwitcher;
    public SetDropModels modelSetter;
    private int pageID;

    private void Start()
    {
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<TurnPage>();
        pageID = modelSetter.pageID;
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID)
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {

            if (Input.GetMouseButton(0))
            {
                difY = Mathf.Abs(lastY - Input.GetAxis("Mouse Y"));

                if (lastY < Input.GetAxis("Mouse Y"))
                {
                    direction = -1;
                    transform.Rotate(Vector3.right, -difY);
                }

                if (lastY > Input.GetAxis("Mouse Y"))
                {
                    direction = 1;
                    transform.Rotate(Vector3.right, difY);
                }

                lastY = -Input.GetAxis("Mouse Y");

                userHasTouched = true;
            }
            else
            {
                if (!userHasTouched)
                    return;

                if (difY > 0f) difY -= 0.01f;
                if (difY < 0f) difY += 0.01f;

                transform.Rotate(Vector3.right, difY * direction);
            }
        }
    }
}