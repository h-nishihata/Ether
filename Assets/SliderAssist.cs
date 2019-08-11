using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SliderAssist : MonoBehaviour
{
    private RectTransform handlePosition;
    public  RectTransform fill;
    public float[] fixedPositions;
    private float currentPos;
    // Start is called before the first frame update
    void Start()
    {
        handlePosition = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentPos = handlePosition.anchorMin.x;
        }
        else
        {
            ToNearest();
        }

    }

    void ToNearest()
    {
        var nearest = fixedPositions.OrderBy(x => Mathf.Abs(x - currentPos)).First();

        handlePosition.anchorMin = new Vector2(nearest, 0f);
        handlePosition.anchorMax = fill.anchorMax = new Vector2(nearest, 1f);
    }
}
