using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オブジェクトの回転.X軸方向転はMainCamera，Y軸方向はEtherにアタッチされている.
/// </summary>
public class Rotation : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public bool isCamera;
    private float velocity = 1f;

    public Button resetButton;
    private Vector3 camPosition = new Vector3(-1f, 2f, -10f);
    private DropNumSwitcher dropNumSwitcher;
    public Slider numDropsSlider;
    public Slider materialSlider;
    public Slider pedestalSizeSlider;
    public Slider pedestalHeightSlider;
    public Slider bodySizeSlider;
    public Toggle bodyToggle;


    private void Start()
    {
        dropNumSwitcher = this.gameObject.GetComponent<DropNumSwitcher>();
    }

    void Update ()
	{
        if (isCamera)
        {
            if (variableJoystick.Direction.y > 0f && (transform.rotation.x < 0.5f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, velocity);
                resetButton.interactable = true;
            }
            else if (variableJoystick.Direction.y < 0f && (transform.rotation.x > -0f))
            {
                transform.RotateAround(Vector3.zero, Vector3.right, -velocity);
                resetButton.interactable = true;
            }
        }
        else
        {
            if (variableJoystick.Direction.x < 0f)
            {
                transform.Rotate(Vector3.up, velocity);
                resetButton.interactable = true;
            }
            else if (variableJoystick.Direction.x > 0f)
            {
                transform.Rotate(Vector3.up, -velocity);
                resetButton.interactable = true;
            }
        }
    }

    public void Reset()
    {
        transform.localRotation = isCamera ? Quaternion.Euler(12, 0, 0) : Quaternion.identity;
        transform.localPosition = isCamera ? camPosition : Vector3.zero;

        if (!isCamera)
        {
            dropNumSwitcher.SetPedestal(0);
            materialSlider.value = 0f;
            numDropsSlider.value = pedestalSizeSlider.value = 1f;
            pedestalHeightSlider.value = 0.5f;
            bodySizeSlider.value = 1.75f;
            bodyToggle.isOn = false;

            resetButton.interactable = false;
        }
    }
}
