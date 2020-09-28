using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オブジェクトの回転用スクリプト.X軸方向はMainCamera，Y軸方向はEtherにアタッチされている.
/// </summary>
public class Rotation : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public bool isCamera;
    private float velocity = 0.25f;

    private Vector3 camLocalPosition = new Vector3(-2f, 0f, -13f);
    private DropNumSwitcher dropNumSwitcher;
    public Slider numDropsSlider;
    public Slider materialSlider;
    public Slider pedestalWidthSlider;
    public Slider pedestalHeightSlider;
    public Slider etherSizeSlider;
    public Toggle bodyToggle;
    public Toggle archiveToggle;

    public static bool isEyeMode = false;
    public Transform eyeLevel;


    private void Start()
    {
        dropNumSwitcher = this.gameObject.GetComponent<DropNumSwitcher>();
    }

    void Update ()
	{
        if (isCamera) // アタッチされているオブジェクトがMain Cameraなら......
        {
            if (isEyeMode)
            {
                // 主観モードのときは，首の上下の運動に切り替える.
                if (variableJoystick.Direction.y != 0f)
                    transform.rotation = Quaternion.Euler(variableJoystick.Direction.y * 50f, 180, 0);
            }
            else
            {
                // X軸を中心としてカメラを上下に動かす.
                if (variableJoystick.Direction.y > 0f && (transform.rotation.x < 0.5f))
                {
                    transform.RotateAround(Vector3.zero, Vector3.right, velocity);
                }
                else if (variableJoystick.Direction.y < 0f && (transform.rotation.x > -0.2f))
                {
                    transform.RotateAround(Vector3.zero, Vector3.right, -velocity);
                }
            }
        }
        else // アタッチされているオブジェクトがEtherなら......
        {
            if (!isEyeMode)
            {
                // Y軸を中心として，親オブジェクトを回転させる.
                if (variableJoystick.Direction.x < 0f)
                {
                    transform.Rotate(Vector3.up, velocity);
                }
                else if (variableJoystick.Direction.x > 0f)
                {
                    transform.Rotate(Vector3.up, -velocity);
                }
            }
        }
    }

    public void ChangeCamMode(bool status)
    {
        if (!bodyToggle.isOn)
            return;

        isEyeMode = !isEyeMode;
        this.gameObject.transform.position = isEyeMode ? eyeLevel.transform.position : camLocalPosition + Vector3.right * -100; // 主観モードへの移行時，ワールド座標で指定しているので，戻るときにもそれを勘案する.
        this.gameObject.transform.rotation = isEyeMode ? eyeLevel.transform.rotation : Quaternion.identity;
    }

    public void Reset()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = isCamera ? camLocalPosition : Vector3.zero;

        if (!isCamera)
        {
            dropNumSwitcher.SetPedestal(0);
            numDropsSlider.value = 1f;
            etherSizeSlider.value = 350f;
            materialSlider.value = 0f;
            pedestalWidthSlider.value = 300f;
            pedestalHeightSlider.value = 100f;
            bodyToggle.isOn = false;
            archiveToggle.isOn = false;
        }
    }
}
