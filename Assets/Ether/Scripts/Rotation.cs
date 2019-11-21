using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オブジェクトの回転.X軸方向転はMainCamera，Y軸方向はEtherにアタッチされている.
/// </summary>
public class Rotation : MonoBehaviour
{
    public bool isCamera;
    public float velocity = 3f;

    public Button resetButton;
    public Slider numDropsSlider;
    private Vector3 camPosition = new Vector3(-1f, 2f, -10f);
    private DropNumSwitcher dropNumSwitcher;
    private MatTexSetter matTexSetter;


    private void Start()
    {
        dropNumSwitcher = this.gameObject.GetComponent<DropNumSwitcher>();
        matTexSetter = this.gameObject.GetComponent<MatTexSetter>();
    }

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

    public void Reset()
    {
        transform.localRotation = isCamera ? Quaternion.Euler(12, 0, 0) : Quaternion.identity;
        transform.localPosition = isCamera ? camPosition : Vector3.zero;

        if (!isCamera)
        {
            dropNumSwitcher.SetPedestal(0);
            matTexSetter.SetTexture(0);
            numDropsSlider.value = 1;
            resetButton.interactable = false;
        }
    }
}
