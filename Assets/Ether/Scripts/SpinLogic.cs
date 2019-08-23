using UnityEngine;
using UnityEngine.UI;

public class SpinLogic : MonoBehaviour
{
    public TurnPage pageSwitcher;
    private int pageID;
    public SetDropModels modelSetter;
    private SetMatTexure materialSetter;
    string lotNumber;

    private bool userHasTouched;

    private float lastX, lastY;
    private float diffX, diffY = 0.5f;
    private int directionX, directionY = 1;
    private float decayLevel = 0.03f;

    private Text patternInfo;

    private void Start()
    {
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<TurnPage>();
        pageID = modelSetter.pageID;
        materialSetter = Camera.main.GetComponent<SetMatTexure>();
        patternInfo = modelSetter.info;
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID) // 現在表示中でなければ...
        {
            transform.rotation = Quaternion.identity; // 回転をリセット.
        }
        else if (pageSwitcher.currentPage - 1 == pageID) // 自分のページが表示されたら...
        {
            // すでに制作されたことがある場合，自分のページが表示されたら(MainCameraに映ったら)カメラの背景色を徐々に変える. 
            if (modelSetter.isExistentInArchive)
            {
                var matType = modelSetter.fixedMat;
                materialSetter.ChangeBGToWhite(matType); // 背景色を白に変える.
            }
            else if (!modelSetter.isExistentInArchive)
            {
                if (!SetMatTexure.genButtonPressed)
                    materialSetter.ChangeBGToBlack(); // 通常は黒を使用する.
            }

            patternInfo.color = SetMatTexure.genButtonPressed ? Color.black : Color.white;

            // タッチで3Dモデルを回転.
            if (Input.GetMouseButton(0))
            {
                diffX = Mathf.Abs(lastX - Input.GetAxis("Mouse X"));
                diffY = Mathf.Abs(lastY - Input.GetAxis("Mouse Y"));
                // X軸方向.
                if (lastX < Input.GetAxis("Mouse X"))
                {
                    directionX = -1;
                    transform.Rotate(Vector3.up, -diffX * 0.8f);
                }
                else if (lastX > Input.GetAxis("Mouse X"))
                {
                    directionX = 1;
                    transform.Rotate(Vector3.up, diffX * 0.8f);
                }
                // Y軸方向.
                if (lastY < Input.GetAxis("Mouse Y"))
                {
                    directionY = -1;
                    transform.Rotate(Vector3.right, -diffY * 3f);
                }
                else if (lastY > Input.GetAxis("Mouse Y"))
                {
                    directionY = 1;
                    transform.Rotate(Vector3.right, diffY * 3f);
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

    void GenerateNewPattern()
    {
        lotNumber = modelSetter.lotNumber4CSV;
        Debug.Log(lotNumber);
    }
}