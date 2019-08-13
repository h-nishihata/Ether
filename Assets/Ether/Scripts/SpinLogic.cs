using UnityEngine;
 
public class SpinLogic : MonoBehaviour
{
    public TurnPage pageSwitcher;
    public SetDropModels modelSetter;
    private int pageID;

    private float lastX, lastY;
    private float diffX, diffY = 0.5f;
    private int directionX, directionY = 1;
    private float decayLevel = 0.03f;

    private bool userHasTouched;


    private void Start()
    {
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<TurnPage>();
        pageID = modelSetter.pageID;
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID) // 現在表示中でなければ...
        {
            transform.rotation = Quaternion.identity; // 回転をリセット.
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                diffX = Mathf.Abs(lastX - Input.GetAxis("Mouse X"));
                diffY = Mathf.Abs(lastY - Input.GetAxis("Mouse Y"));
                // X軸方向回転
                if (lastX < Input.GetAxis("Mouse X"))
                {
                    directionX = -1;
                    transform.Rotate(Vector3.up, -diffX);
                }
                else if (lastX > Input.GetAxis("Mouse X"))
                {
                    directionX = 1;
                    transform.Rotate(Vector3.up, diffX);
                }
                // Y軸方向回転
                if (lastY < Input.GetAxis("Mouse Y"))
                {
                    directionY = -1;
                    transform.Rotate(Vector3.right, -diffY * 2.5f);
                }
                else if (lastY > Input.GetAxis("Mouse Y"))
                {
                    directionY = 1;
                    transform.Rotate(Vector3.right, diffY * 2.5f);
                }

                lastX = -Input.GetAxis("Mouse X");
                lastY = -Input.GetAxis("Mouse Y");

                userHasTouched = true;
            }
            else
            {
                if (!userHasTouched)
                    return;
                // マウスボタンを離した後も惰性で回転を続け，徐々に減衰する.
                if (diffX < 0f)
                    diffX += decayLevel;
                else if (diffX > 0f)
                    diffX -= decayLevel;

                if (diffY > 0f)
                    diffY -= decayLevel;
                else if (diffY < 0f)
                    diffY += decayLevel;

                transform.Rotate(Vector3.up, diffX * directionX);
                transform.Rotate(Vector3.right, diffY * directionY);
            }
        }
    }
}