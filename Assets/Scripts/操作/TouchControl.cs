using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDebug;

namespace TouchManager
{
    public class TouchControl : MonoBehaviour
    {
        private static Touch touch;
        public static Touch Touch
        {
            get
            {
                //タッチの初期化
                if (Application.isEditor)
                {
                    Vector2 oldPos = new Vector2();

                    if (Input.GetMouseButtonDown(0))
                    {
                        touch.tapCount = 1;
                        touch.position = Input.mousePosition;
                        oldPos = touch.position;
                        touch.phase = TouchPhase.Began;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        touch.position = Input.mousePosition;
                        if (oldPos != touch.position)
                        {
                            touch.phase = TouchPhase.Moved;
                        }
                        else
                        {
                            touch.phase = TouchPhase.Stationary;
                        }
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        touch.phase = TouchPhase.Ended;
                    }
                    /*
                    TouchDebug td = new TouchDebug();
                    td.ClickToTouch();
                    */
                }
                else
                {
                    touch = Input.GetTouch(0);
                }
                return touch;
            }
        }

        private static Vector2 startPos;
        public static Vector2 StartPos { get; }

        void Update()
        {
            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
        }  
    }

    
}