using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Update内のDecreasePoiHp内にポイのHpを減少させる処理を記述していく*/
public class BreakPoi : MonoBehaviour
{
    //ポイの耐久値
    private readonly float MAX_POI_HP = 1000;
    private float poiHp;

    //耐久を削るスピードパラメータ(1秒*倍率)
    private readonly float DECREASE_HP_SPEED_INWATER = 0.01f;
    private readonly float DECREASE_HP_SPEED_LIFT = 0.05f;
    private readonly float DECREASE_HP_SPEED_ONFISH = 0.4f;

    private readonly int MAX_JOINT_NUM = 2;             //捕まえれる金魚の最大値
    public bool PoiIsDefeat { get; set; } = false;      //ポイの生死をつかさどる
    
    private BoxCollider collider;
    private PoiAndFishJoint joint;
    private UpDownButton upDown;

    private static BreakPoi instance;
    public static BreakPoi Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BreakPoi>();
            }
            return instance;
        }
    }

    void Start()
    {
        poiHp = MAX_POI_HP;
        collider = GetComponentInChildren<BoxCollider>();
        joint = GetComponent<PoiAndFishJoint>();
        upDown = GetComponent<UpDownButton>();
    }

    void Update()
    {
        DecreasePoiHp();    //ポイのHPを減少させる

        CheckBreakPoi();
    }

    //ポイのHPを減少させる
    private void DecreasePoiHp()
    {
        InWater();          //水に浸かっているとき
        FishOnPoiNum();     //ポイに乗ってる金魚の数に応じたHP減算
        Lift();             //ポイ昇降
    }

    //水中に浸かった時間でHPを削る
    private void InWater()
    {

        //水中に浸かっているとき
        if (transform.position.y < main.Instance.tankMaxY - 0.1f)
        {
            poiHp -= Time.deltaTime * DECREASE_HP_SPEED_INWATER * MAX_POI_HP;
        }
    }

    //ポイに乗ってる金魚の数に応じたHP減算(二次関数)
    private void FishOnPoiNum()
    {
        poiHp -=    Time.deltaTime * MAX_POI_HP * Mathf.Pow(joint.JointNum, 2) / 
                    Mathf.Pow(MAX_JOINT_NUM + 1, 2) * 
                    DECREASE_HP_SPEED_ONFISH;

        if(MAX_JOINT_NUM < joint.JointNum)
        {
            poiHp -= MAX_POI_HP;
        }
    }

    private void Lift()
    {
        if (upDown.MoveFlag)
            poiHp -= Time.deltaTime * MAX_POI_HP * DECREASE_HP_SPEED_LIFT;
    }

    //ポイが破れるかのチェック
    private void CheckBreakPoi()
    {
        //ポイのHpが切れたら
        if(poiHp < 0 && !PoiIsDefeat)
        {
            PoiBreak();
            PoiAndFishJoint.Instance.Release();
        }
    }

    //ポイ死亡
    public void PoiBreak()
    {
        Cloth[] cloths = GetComponentsInChildren<Cloth>();      //全てのCloth取得

        //全部破る
        foreach(Cloth cloth in cloths)
        {
            cloth.enabled = true;
        }

        PoiIsDefeat = true;
        collider.enabled = false;
    }

    //ポイ復活
    public void PoiResurrection()
    {
        Cloth[] cloths = GetComponentsInChildren<Cloth>();      //全てのCloth取得
        

        //全部復活
        foreach (Cloth cloth in cloths)
        {
            cloth.enabled = false;
        }

        PoiIsDefeat = false;
        collider.enabled = true;
        poiHp = MAX_POI_HP;
    }
}
