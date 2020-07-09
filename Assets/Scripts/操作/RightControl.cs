using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControl : MonoBehaviour
{
    private LineRenderer line;
    private Touch touch;
    private Vector3[] linePos = new Vector3[2];
    private int screenWidth;
    private int fingerId = 0;

    private float touchHeight;
    public float TouchHeight
    {
        get
        {
            Vector3 start = Camera.main.WorldToScreenPoint(linePos[0]);
            Vector3 end = Camera.main.WorldToScreenPoint(linePos[1]);

            touchHeight = end.y - start.y;

            return touchHeight;
        }
    }

    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        screenWidth = Screen.width;
    }

    void Update()
    {
        //エディター上ではマウスを取得できるようにする
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
        }
        else
        {
            if (Input.touchCount > 0)
            {
                //指が登録されてない場合
                if (fingerId == 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        touch = Input.touches[i];
                        if (touch.position.x > screenWidth / 2)
                        {
                            fingerId = Input.touches[i].fingerId;
                            break;
                        }
                    }
                }
                else
                {
                    //指が登録済みの時は検索
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        if (Input.touches[i].fingerId == fingerId)
                        {
                            touch = Input.touches[i];
                            break;
                        }
                    }
                }
            }
        }

        if(touch.tapCount > 0)
        {
            //画面右半分がタップされた場合のみ処理
            if ((touch.position.x > screenWidth / 2 && touch.phase == TouchPhase.Began) || linePos[0] != Vector3.zero)
            {

                Vector3 touchPos = touch.position;
                touchPos.z = 1f;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

                if (touch.phase == TouchPhase.Began)
                {
                    linePos[0] = worldPos;      //初期位置の保存
                    linePos[1] = worldPos;      //現在位置の更新
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    linePos[1] = worldPos;          //現在位置の更新
                    linePos[1].x = linePos[0].x;    //xは固定
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    linePos[0] = Vector3.zero;
                    linePos[1] = Vector3.zero;
                    fingerId = 0;
                }

                line.SetPositions(linePos);
            }
        }
    }
}
