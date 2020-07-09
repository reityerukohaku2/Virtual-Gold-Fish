using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownButton : MonoBehaviour
{
    private ClickTimer clickTimer;
    private CGenerateRipple ripple;
    private PoiAudio poiAudio;              //ぽちゃん
    private Splash splash;                  //水しぶき
    private Rigidbody GetRigidbody;
    public Rigidbody ChildBody;
    private static bool upperFlag;          //上にいるか下にいるか
    private static bool moveFlag;           //移動中か否か
    public bool MoveFlag => moveFlag;
    private static bool goUp;               //上に向かってます
    public bool GoUp
    {
        get
        {
            return goUp;
        }
    }

    private bool inWater = false;    //水に浸かっているか

    private Vector3 poiPos;
    public float Vmax, Vmin, Ymax, Ymin, DestroyZoneY;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        clickTimer = FindObjectOfType<ClickTimer>();
        ripple = FindObjectOfType<CGenerateRipple>();
        splash = FindObjectOfType<Splash>().GetComponent<Splash>();
        poiAudio = GetComponent<PoiAudio>();
        GetRigidbody = GetComponent<Rigidbody>();
        upperFlag = true;
        moveFlag = false;
        goUp = false;
    }

    void Update()
    {
        if (moveFlag)
        {
            float numerator;    //分子
            float denominator;  //分母

            //子オブジェクトのRigidbodyを起動して衝突判定を可能にする
            ChildBody.WakeUp();

            poiPos = transform.position;

            //昇降の速度の式
            //上から下：Vd = (Y - Ymin)(Vmin - Vmax) / (Ymax - Ymin) - Vmin
            //下から上：Vu = (Y - Ymin)(Vmin - Vmax) / (Ymax - Ymin) + Vmax

            //上下運動の式(やだのび太さんのエッチ…///)
            velocity = GetRigidbody.velocity;
            if (upperFlag)
            {
                numerator = (poiPos.y - Ymin) * (Vmin - Vmax);
                denominator = Ymax - Ymin;
                velocity.y = numerator / denominator - Vmin;
            }
            else
            {
                numerator = (poiPos.y - Ymin) * (Vmin - Vmax);
                denominator = Ymax - Ymin;
                velocity.y = numerator / denominator + Vmax;
            }

            GetRigidbody.velocity = velocity;

            float clampNum;


            //降りるとき
            if (upperFlag)
            {
                //入水
                if(!inWater && poiPos.y <= main.Instance.tankMaxY)
                {
                    ripple.GenerateRipple(new Vector2(transform.position.x, transform.position.z));
                    poiAudio.PlayWaterEnter();
                    splash.PlaySplash(poiPos);
                    inWater = true;
                }

                clampNum = Mathf.Clamp(poiPos.y, Ymin, Ymax);
                PoiAndFishJoint.Instance.Release();
                if (clampNum == Ymin)
                {
                    upperFlag = false;
                    moveFlag = false;
                    transform.position = new Vector3(poiPos.x, clampNum, poiPos.z);
                    GetRigidbody.velocity = new Vector3(velocity.x, 0, velocity.z);
                    return;
                }
            }
            //上るとき
            else
            {
                //水から出るとき
                if(inWater && poiPos.y >= main.Instance.tankMaxY)
                {
                    ripple.GenerateRipple(new Vector2(transform.position.x, transform.position.z));
                    poiAudio.PlayWaterFlood();
                    splash.PlaySplash(poiPos);
                    inWater = false ;
                }

                clampNum = Mathf.Clamp(poiPos.y, Ymin, Ymax);
                if (clampNum == Ymax)
                {
                    upperFlag = true;
                    moveFlag = false;
                    transform.position = new Vector3(poiPos.x, clampNum, poiPos.z);
                    GetRigidbody.velocity = new Vector3(velocity.x, 0, velocity.z);
                    return;
                }
            }

            //上に向かってるか
            if (!upperFlag)
            {
                goUp = true;
            }
            else
            {
                goUp = false;
            }
        }
        else
        {
            goUp = false;
        }
    }

    public void UpDown()
    {
        if (clickTimer.MyTime >= 0.3f) return;  //もはやクリックと呼べなかったらreturn

        if (!moveFlag)
        {
            moveFlag = true;
        }
        else
        {
            upperFlag = !upperFlag;
        }
    }
}

