
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class boidsScript : MonoBehaviour
{
    public new Rigidbody rigidbody;

    public float forcePower;                        //ピュンって加速する時の力の大きさ
    private float startSpeed = 2.0f;
    private Vector3 position, velocity;             //現在位置と速度
    private Vector3 acceleration = Vector3.zero;    //加速度
    public float attenulationRate = 0.95f;          //速度の減衰率
    public float FOV { get; set; } = 2.0f;          //視野
    public bool isStop = true;                     //停止Algorithmを使うかどうか

    private const float speedMin = 0.2f;
    private const float speedMax = 100.0f;
    private const float overSpeed = 0.1f;

    private const float reflectPower = 0.2f;
    private const float avoidancePower = 1f;
    public bool ProcessSwitch { get; set; } = true;

    /*最適化*/
    private int triggerStayCount = 0;           //onTriggerStayの処理回数を減らすためのカウント
    private bool moveFlag;               //trueのときだけ回避行動
    private float oldTime;                      //停止開始時刻
    private float randomTime;                   //停止時間
    private readonly float RAND_MIN = 1.0f;
    private readonly float RAND_MAX = 10.0f;
    private readonly float moveTime = 2.0f;     //移動時間


    //moveFlag切り替え
    private bool MoveFlagSwitch()
    {
        if (!isStop) return true;   //停止Algorithm使わないときは常に泳ぎ続ける

        if(oldTime + randomTime <= Time.time)
        {
            moveFlag = !moveFlag;
            oldTime = Time.time;
            randomTime = Random.Range(RAND_MIN, RAND_MAX);
            if(moveFlag)
            {
                rigidbody.velocity += transform.forward * forcePower;
            }
        }
        return moveFlag;
    }

    //テスト
    void OnTriggerEnter(Collider other)
    {
        if (!MoveFlagSwitch()) return;

        if (other.gameObject.tag == "Wall")
        {
            Vector3 normal;     //衝突相手の法線ベクトル
            Vector3 thisPosition = transform.position;      //自身の座標
            Vector3 otherPosition = other.ClosestPointOnBounds(this.transform.position);    //衝突相手の座標
            float sqrDistance = Vector3.SqrMagnitude(thisPosition - otherPosition);         //距離
            Ray ray = new Ray(thisPosition, otherPosition - thisPosition);                  //自分から衝突相手へのRay
            RaycastHit hit;     //Ray衝突情報

            if (other.Raycast(ray, out hit, 10))
            {
                if (hit.collider != null)
                {
                    normal = hit.normal;
                    acceleration += Vector3.Reflect(hit.point - thisPosition, normal).normalized * reflectPower / sqrDistance;
                }
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        Profiler.BeginSample("All");
        Profiler.EndSample();

        if (!MoveFlagSwitch()) return;

        Profiler.BeginSample("partial");
        Profiler.EndSample();

        if (other.gameObject.tag == "Wall")
        {
            Vector3 normal;                                                //衝突相手の法線ベクトル
            Vector3 thisPosition = transform.position;                     //自身の座標
            Vector3 otherPosition = other.ClosestPointOnBounds(this.transform.position);    //衝突相手の座標
            float sqrDistance = Vector3.SqrMagnitude(thisPosition - otherPosition);         //距離
            Ray ray = new Ray(thisPosition, otherPosition - thisPosition);      //自分から衝突相手へのRay
            RaycastHit hit;                                                     //Ray衝突情報

            if (other.Raycast(ray, out hit, 10))
            {
                if (hit.collider != null)
                {
                    normal = hit.normal;
                    acceleration += Vector3.Reflect(hit.point - thisPosition, normal).normalized * reflectPower / sqrDistance;
                }
            }
        }
    }

    //他個体回避
    public void OtherAvoidance(Collider other)
    {
        if (!(MoveFlagSwitch() && isStop)) return;

        if (other.gameObject.tag == "boid")
        {
            Vector3 normal;                                                             //衝突相手の法線ベクトル
            Vector3 thisPosition = transform.position;                                  //自身の座標
            Vector3 otherPosition = other.transform.position;                           //衝突相手の座標
            float sqrDistance = Vector3.SqrMagnitude(thisPosition - otherPosition);     //距離
            Ray ray = new Ray(thisPosition, otherPosition - thisPosition);              //自分から衝突相手へのRay
            RaycastHit hit;                                                             //Ray衝突情報

            if (other.Raycast(ray, out hit, 10))
            {
                normal = hit.normal;
                acceleration += Vector3.Reflect(hit.point - thisPosition, normal).normalized * avoidancePower / sqrDistance;
            }
        }
    }

    //速度均一化
    private Vector3 FlockVelocityAveraging()
    {
        List<boidsScript> boids = main.Instance.GetOtherBoidsInFOV(this);

        Vector3 sum = new Vector3();

        if(boids.Count != 0)
        {
            foreach(boidsScript boid in boids)
            {
                sum += boid.velocity;
            }
            sum /= boids.Count;
        }

        return sum - velocity;
    }

    //近くの仲間の中心に集まろうとする
    private Vector3 FlockCentralization()
    {
        List<boidsScript> boids = main.Instance.GetOtherBoidsInFOV(this);

        Vector3 res = new Vector3();

        if(boids.Count != 0)
        {
            foreach (boidsScript boid in boids)
            {
                res += boid.transform.position;
            }

            res /= boids.Count;
        }
        else
        {
            return Vector3.zero;
        }

        return res - transform.position;
    }


    //初期化
    void Start()
    {
        randomTime = Random.Range(RAND_MIN, RAND_MAX);
        oldTime = Time.time;
        position = transform.position;
        startSpeed = Random.Range(0.1f, 2.0f);
        velocity = transform.forward * startSpeed;
        rigidbody.velocity = velocity;
        acceleration = Vector3.zero;

        if (Random.Range(0, 1.0f) >= 0.999)moveFlag = true;
        else moveFlag = false;
    }

    //等加速度直線運動の公式
    /*https://kou.benesse.co.jp/nigate/science/a13p01bb03.html*/
    void Update()
    {
        velocity = rigidbody.velocity;

        //金魚復活
        if(transform.position.y < main.Instance.SurfaceY - 0.5f && PoiAndFishJoint.Instance.JointNum == 0 && !ProcessSwitch)
        {
            ProcessSwitch = true;
            gameObject.layer = LayerMask.NameToLayer("BoidsTrigger");
            rigidbody.velocity = transform.forward * forcePower;
            velocity = rigidbody.velocity;
            rigidbody.useGravity = false;
        }

        //動的な重みづけ
        float[] weight = new float[3];
        for(int i = 0; i < 3; i++)
        {
            weight[i] = main.Instance.weight[i];
        }

        //processswitchがfalseの時は処理しない
        if (!ProcessSwitch)
        {
            velocity = Vector3.zero;
            return;
        }

        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);

        //減速
        if (velocity.magnitude >= overSpeed)
        {
            velocity = velocity * attenulationRate;
            rigidbody.velocity = velocity;
        }

        //高さのクランプ
        if (transform.position.y > main.Instance.tankMaxY)
        {
            //設定する座標
            Vector3 clamp = transform.position;
            clamp.y = main.Instance.tankMaxY - 0.1f;
            transform.position = clamp;
        }

        //moveFlagがfalseのときも処理しない
        if (!MoveFlagSwitch())
        {
            return;
        }
        else
        {
            float deltaT = Time.deltaTime;

            acceleration = (acceleration * weight[0] + FlockVelocityAveraging() * weight[1] + FlockCentralization() * weight[2]) / weight.Sum();
            velocity += acceleration * deltaT;
            velocity = velocity / velocity.magnitude * Mathf.Clamp(velocity.magnitude, speedMin, speedMax);     //速度の制限
        }



        rigidbody.velocity = velocity;

        transform.rotation = Quaternion.LookRotation(rigidbody.velocity);

        acceleration = Vector3.zero;
    }
}
