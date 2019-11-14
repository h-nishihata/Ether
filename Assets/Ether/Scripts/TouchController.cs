using UnityEngine;

/// <summary>
/// モデルの回転.
/// </summary>
public class TouchController : MonoBehaviour
{
    //public UIController UI;
    private bool userHasTouched;
    private float countToRotation;
    private float countToDefault;
    private float transitionTime = 1f;

    private float lastX, lastY;
    private float diffX, diffY = 0.5f;
    private int directionX, directionY = 1;
    private float decayLevel = 0.03f;


    void Update()
    {
        //transform.rotation = Quaternion.identity; // 回転をリセット.

        if (Input.GetMouseButton(0))
        {
            if (countToRotation < transitionTime)
            {
                countToRotation += Time.deltaTime;
            }
            else
            {
                countToDefault = 0f;
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
        if (countToDefault < transitionTime)
        {
            countToDefault += Time.deltaTime;
        }
        else
        {
            countToRotation = 0f;
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
}