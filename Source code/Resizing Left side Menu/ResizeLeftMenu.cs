using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeLeftMenu : MonoBehaviour, IDragHandler
{

    public Texture2D resizePointer;

    public GameObject scrollView;

    public Canvas canvas;







    public void pointerEnter()
    {
        Cursor.SetCursor(resizePointer, Vector2.zero, CursorMode.Auto);

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), 0);

        float menuWidth = scrollView.GetComponent<RectTransform>().rect.width; // width of the menu
        float menuHeight = scrollView.GetComponent<RectTransform>().rect.height;













    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

        scrollView.GetComponent<RectTransform>().offsetMax += new Vector2(eventData.delta.x / canvas.scaleFactor, 0);

    }



    public void pointerExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }



    public void Start()
    {

    }
}
