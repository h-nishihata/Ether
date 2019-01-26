using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;

	private Color normalColor = Color.white;
	public Color highlightColor = Color.blue;
    public int latestSpriteNum;
	
	public void OnDrop(PointerEventData data)
	{
		if (receivingImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
        if (dropSprite != null)
        {
            receivingImage.overrideSprite = dropSprite;
            RecordLatestSprite(dropSprite);
        }
        receivingImage.color = normalColor;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		Sprite dropSprite = GetDropSprite (data);
        if (dropSprite != null) receivingImage.color = highlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}

    public void RecordLatestSprite(Sprite currentSprite)
    {
        switch (currentSprite.name)
        {
            case  "Icon1":
                latestSpriteNum = 1;
                break;
            case "Icon2":
                latestSpriteNum = 2;
                break;
            case "Icon3":
                latestSpriteNum = 3;
                break;
        }
    }
}
