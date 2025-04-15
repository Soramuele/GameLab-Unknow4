using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace Unknown.Ruslan
{
    public class MiniGameScript : MonoBehaviour
    {
        //public RectTransform customCursor;
        public RectTransform boundaryImage;
        public RectTransform startPositionImage;
        //public GameObject player;
        //public GameObject door;
        //public GameObject newcursor;
        public GameObject Startscreen;
        public GameObject Endscreen;
        public GameObject level1;
        public GameObject level2;
        public GameObject level3;
        public GameObject level4;
        
        private bool allowCursorMovement = false;
        //private Vector2 previousMousePosition;


        void Start()
        {

        


        }
        // Start is called before the first frame update


        // Update is called once per frame
        void Update()
        {


            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
            if (!allowCursorMovement) return;


            Vector2 mousePosition = Input.mousePosition;

            Rect boundaryRect = boundaryImage.rect;
            float minX = boundaryImage.position.x - boundaryRect.width / 2;
            float maxX = boundaryImage.position.x + boundaryRect.width / 2;
            float minY = boundaryImage.position.y - boundaryRect.height / 2;
            float maxY = boundaryImage.position.y + boundaryRect.height / 2;


            float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);


            //customCursor.position= new Vector2(clampedX, clampedY);
        
        //if(Endscreen.activeInHierarchy && Vector3.Distance(player.transform.position, door.transform.position) < 2f && Input.GetKeyDown(KeyCode.E))
        // {




        // }
        }




        void EnableCursorControl()
        {
            allowCursorMovement = true;
        }


        public void OnBreakEnter()  // walls touched
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3 .SetActive(false);
            Startscreen.SetActive(true);
            //newcursor.SetActive(false);
        }

        public void StartThegame()  
        {
            //newcursor.SetActive(true);
            level1.SetActive(true);
            Startscreen.SetActive(false);
            //customCursor.position = startPositionImage.position;
            //Invoke(nameof(EnableCursorControl), 1f);

        }

        public void Level2()
        {
            
            level1.SetActive(false);
            level2.SetActive(true);
        }


        public void Level3() 
        {
            level2.SetActive(false);
            level3.SetActive(true);


        }



        public void Level4()
        {

            level3.SetActive(false);
            level4.SetActive(true);




        }



        public void FinishThegame()
        {

            level3 .SetActive(false);
            Endscreen.SetActive(true);
            /// + UI


        }






    }
}
