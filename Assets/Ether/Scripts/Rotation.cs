using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オブジェクトの回転用スクリプト.X軸方向はMainCamera，Y軸方向はEtherにアタッチされている.
/// </summary>
public class Rotation : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public bool isCamera;
    private float velocity = 1f;

    public Button resetButton;
    private Vector3 camPosition = new Vector3(-2f, 0f, -13f);
    private DropNumSwitcher dropNumSwitcher;
    public Slider numDropsSlider;
    public Slider materialSlider;
    public Slider pedestalWidthSlider;
    public Slider pedestalHeightSlider;
    public Slider etherSizeSlider;
    public Toggle bodyToggle;


    private void Start()
    {
        dropNumSwitcher = this.gameObject.GetComponent<DropNumSwitcher>();
    }

    void Update ()
	{
        if (isCamera)
        {
            // X軸.
            if (variableJoystick.Direction.y > 0f && (transform.rotation.x < 0.5f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, velocity);
            }
            else if (variableJoystick.Direction.y < 0f && (transform.rotation.x > -0.2f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, -velocity);
            }
        }
        else
        {
            // Y軸.
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

    public void Reset()
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = isCamera ? camPosition : Vector3.zero;

        if (!isCamera)
        {
            dropNumSwitcher.SetPedestal(0);
            numDropsSlider.value = 1f;
            etherSizeSlider.value = 350f;
            materialSlider.value = 0f;
            pedestalWidthSlider.value = 300f;
            pedestalHeightSlider.value = 100f;
            bodyToggle.isOn = false;
        }
    }
}
