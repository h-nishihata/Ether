using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    public bool isCamera;
    public float velocity = 3f;
    public Button resetButton;


    void Update ()
	{
        if (isCamera)
        {
            if (Input.GetKey(KeyCode.W) && (transform.rotation.x < 0.5f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, velocity);
                resetButton.interactable = true;
            }
            else if (Input.GetKey(KeyCode.S) && (transform.rotation.x > -0f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, -velocity);
                resetButton.interactable = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -velocity);
                resetButton.interactable = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, velocity);
                resetButton.interactable = true;
            }
        }
    }

    /// <summary>
    /// 回転をリセット.
    /// </summary>
    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        resetButton.interactable = false;
    }
}
