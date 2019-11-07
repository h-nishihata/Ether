using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// モデルの回転など，表示中のページの動作に関するスクリプト.
/// </summary>
public class ActivePageBehaviour : MonoBehaviour
{
    public PageSwitcher pageSwitcher;
    private int pageID;
    public DropModelSetter modelSetter;
    private MatTexSetter matTexSetter;

    private bool userHasTouched;
    private float timeToHapticMode;
    private float timeToNormalMode;

    private float lastX, lastY;
    private float diffX, diffY = 0.5f;
    private int directionX, directionY = 1;
    private float decayLevel = 0.03f;

    private Text patternInfo;
    private CSVWriter csvWriter;
    private Text genMessage;

    private void Start()
    {
        pageID = modelSetter.pageID;
        pageSwitcher = GameObject.FindWithTag("List").GetComponent<PageSwitcher>();
        matTexSetter = Camera.main.GetComponent<MatTexSetter>();
        patternInfo = modelSetter.patternInfo;
        csvWriter = GameObject.FindWithTag("CSVWriter").GetComponent<CSVWriter>();
        genMessage = GameObject.FindWithTag("GenMessage").GetComponent<Text>();
    }

    void Update ()
    {
        if (pageSwitcher.currentPage - 1 != pageID) // 現在表示中でなければ...
        {
            transform.rotation = Quaternion.identity; // 回転をリセット.
        }
        else if (pageSwitcher.currentPage - 1 == pageID) // 自分のページが表示されたら...
        {
            /* すでに制作されたことがある場合，自分のページが表示されたら(MainCameraに映ったら)カメラの背景色を徐々に変える. 
            if (modelSetter.isExistentInArchive)
            {
                var matType = modelSetter.fixedMat;
                matTexSetter.ChangeBGToWhite(matType); // 背景色を白に変える.
            }
            else if (!modelSetter.isExistentInArchive)
            {
                if (MatTexSetter.genButtonPressed)
                {
                    patternInfo.color = Color.black;
                }
                else
                {
                    patternInfo.color = Color.white;
                    matTexSetter.ChangeBGToBlack(); // 通常は黒を使用する.
                }
            }
            */

            if (Input.GetMouseButton(0))
            {
                if (timeToHapticMode < 2f)
                {
                    timeToHapticMode += Time.deltaTime;
                }
                else
                {
                    timeToNormalMode = 0f;
                    userHasTouched = true;
                }
                if (userHasTouched)
                    RotateSculpture();
            }
            else
            {
                InertialRotation();
            }
        }
    }

    /// <summary>
    /// タッチで3Dモデルを回転.
    /// </summary>
    private void RotateSculpture()
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
    }

    /// <summary>
    /// マウスボタンを離した後も惰性で回転を続け，徐々に減衰する.
    /// </summary>
    private void InertialRotation()
    {
        if (timeToNormalMode < 2f)
        {
            timeToNormalMode += Time.deltaTime;
        }
        else
        {
            timeToHapticMode = 0f;
            userHasTouched = false;
        }

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

    /*
    void GenerateNewPattern()
    {
        genMessage.text = "Generated !";
        genMessage.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));

        if (!isSaved)
        {
            lotNumber.Append("\n");
            lotNumber.Append(modelSetter.lotNumber4CSV);
            lotNumber.Append(",");
            lotNumber.Append(matTexSetter.lastMatName);
            lotNumber.Append(",#,,,");
            csvWriter.Save(lotNumber.ToString(), "archiveData");
            isSaved = true;
        }

        if (counterToGoBack < 5f)
        {
            counterToGoBack += Time.deltaTime;
        }
        else
        {
            counterToGoBack = 0;
            matTexSetter.Reset();
        }

    }*/
}