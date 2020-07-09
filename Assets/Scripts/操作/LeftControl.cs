using MyDebug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchManager;

public class LeftControl : MonoBehaviour
{
    private int screenWidth;
    private LineRenderer line;
    private Vector3[] linePos = new Vector3[2];     //0:初期値   1:現在値
    private Touch touch = new Touch();
    private int fingerId = 0;     //何本目の指か覚えておく

    private float distance;
    public float Distance
    {
        get
        {
            float dist = Vector3.Distance(Camera.main.WorldToScreenPoint(linePos[0]), Camera.main.WorldToScreenPoint(linePos[1]));
            return dist;
        }
    }

    private float touchWidth;
    public float TouchWidth
    {
        get
        {
            Vector3 start = Camera.main.WorldToScreenPoint(linePos[0]);
            Vector3 end = Camera.main.WorldToScreenPoint(linePos[1]);
            float width = end.x - start.x;
            return width;
        }
    }

    private float touchHeight;
    public float TouchHeight
    {
        get
        {
            Vector3 start = Camera.main.WorldToScreenPoint(linePos[0]);
            Vector3 end = Camera.main.WorldToScreenPoint(linePos[1]);
            float height = end.y - start.y;
            return height;
        }
    }

    void Start()
    {
        screenWidth = Screen.width;
        line = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //エディター上ではマウスを取得できるようにする
        if(Application.isEditor)
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
                        if (touch.position.x < screenWidth / 2)
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
                        if(Input.touches[i].fingerId == fingerId)
                        {
                            touch = Input.touches[i];
                            break;
                        }
                    }
                }
            }
        }

        LineRendering();
    }

    
    void LineRendering()
    {
        //画面がタップされたら
        if (touch.tapCount > 0)
        {
            if (touch.phase == TouchPhase.Began || linePos[0] != Vector3.zero)
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
                    linePos[1] = worldPos;      //現在位置の更新
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