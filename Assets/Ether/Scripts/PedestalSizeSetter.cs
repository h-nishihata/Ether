using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各土台に付いている，サイズ変更用スクリプト.
/// </summary>
public class PedestalSizeSetter : MonoBehaviour
{
    public Slider sizeSlider;
    public Slider heightSlider;
    public Transform bottomDrop;
    public bool ignoreHeight;


    public void Rescale()
    {
        var size = sizeSlider.value;
        var height = ignoreHeight ? 1f : heightSlider.value;
        var offset = ignoreHeight ? 0f : height - 0.5f;

        gameObject.transform.localScale = new Vector3(size, height, size);
        gameObject.transform.localPosition = new Vector3(0f, bottomDrop.localPosition.y - 0.33f - offset, 0f);
    }
}
