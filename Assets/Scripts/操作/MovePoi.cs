using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchManager;

public class MovePoi : MonoBehaviour
{
    private LeftControl leftControl;
    private RightControl rightControl;
    public Transform Surface;   //水面
    public float XZSpeed;
    public float YSpeed;
    public float PoiSize = 1;

    Touch touch;
    int screenHeight, screenWidth;
    int fingerId;
    private Rigidbody GetRigidbody;

    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        leftControl = GetComponentInChildren<LeftControl>();
        rightControl = GetComponentInChildren<RightControl>();
        GetRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //左右移動
        //if (leftControl.TouchWidth != 0 || leftControl.TouchHeight != 0)
        //{
        //    float widthRatio = leftControl.TouchWidth / screenWidth;
        //    float heightRatio = leftControl.TouchHeight / screenHeight;

        //    GetRigidbody.velocity = new Vector3(3.3f * widthRatio * XZSpeed, GetRigidbody.velocity.y, 3.3f * heightRatio * XZSpeed);
        //}
        //else
        //{
        //    GetRigidbody.velocity = new Vector3(0, GetRigidbody.velocity.y, 0);
        //}

        //座標のクランプ
        Vector3 clampPos = GetRigidbody.position;
        GetRigidbody.position = clampPos;
    }

    public void OnDragPoi()
    {
        //エディター上ではマウスを取得できるようにする
        if (!Application.isMobilePlatform)
        {
            touch = TouchControl.Touch;
            //Vector2 oldPos = new Vector2();

            //if (Input.GetMouseButtonDown(0))
            //{
            //    touch.tapCount = 1;
            //    touch.position = Input.mousePosition;
            //    oldPos = touch.position;
            //    touch.phase = TouchPhase.Began;
            //}
            //else if (Input.GetMouseButton(0))
            //{
            //    touch.position = Input.mousePosition;
            //    if (oldPos != touch.position)
            //    {
            //        touch.phase = TouchPhase.Moved;
            //    }
            //    else
            //    {
            //        touch.phase = TouchPhase.Stationary;
            //    }
            //}
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    touch.phase = TouchPhase.Ended;
            //}
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 1)
                {
                    fingerId = 1;
                    touch = Input.touches[0];
                }
                else
                {
                    if (fingerId == 0)
                    {
                        fingerId = Input.touchCount;
                    }
                    touch = Input.touches[fingerId];
                }
                //指が登録されてない場合
                //if (fingerId == 0)
                //{
                //    //タップしている指の数だけループ
                //    for (int i = 0; i < Input.touchCount; i++)
                //    {
                //        touch = Input.touches[i];
                //        if (touch.position.x < screenWidth / 2)
                //        {
                //            fingerId = Input.touches[i].fingerId;
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                //    //指が登録済みの時は検索
                //    for (int i = 0; i < Input.touchCount; i++)
                //    {
                //        if (Input.touches[i].fingerId == fingerId)
                //        {
                //            touch = Input.touches[i];
                //            break;
                //        }
                //    }
                //}
            }
            else
            {
                fingerId = 0;
            }
        }

        Vector3 touchPos = touch.position;
        touchPos.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

        worldPos.y = transform.position.y;
        worldPos.x -= PoiSize / 2f;     //取っ手を持っているように見せるための位置調整
        worldPos.z += PoiSize / 4f;

        //壁に埋まらないように．
        worldPos.x = Mathf.Clamp(worldPos.x, main.Instance.tankMinX + PoiSize, main.Instance.tankMaxX - PoiSize);
        worldPos.z = Mathf.Clamp(worldPos.z, main.Instance.tankMinZ + PoiSize, main.Instance.tankMaxZ - PoiSize);

        transform.position = worldPos;      //ターンエンド
       
    }


}
