using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameScript : MonoBehaviour
{
    public RectTransform customCursor;
    public RectTransform boundaryImage;
    public RectTransform startPositionImage;




    void Start()
    {
        customCursor.position = startPositionImage.position;
        Cursor.lockState = CursorLockMode.Confined;





    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        

        Vector2 mousePosition = Input.mousePosition;
        Rect boundaryRect = boundaryImage.rect;
 
        // Calculate the minimum and maximum x and y positions the cursor can move to
        float minX = boundaryImage.position.x - boundaryRect.width / 2;
        float maxX = boundaryImage.position.x + boundaryRect.width / 2;
        float minY = boundaryImage.position.y - boundaryRect.height / 2;
        float maxY = boundaryImage.position.y + boundaryRect.height / 2;

        // Clamp the mouse position to stay inside the boundary
        float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);

        // Set the custom cursor's position to the clamped mouse position
        customCursor.position = new Vector2(clampedX, clampedY);
        
    }

}
