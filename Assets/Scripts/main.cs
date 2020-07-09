using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    public int boidsNum;
    public GameObject boid;

    public GameObject Surface;
    public GameObject DownWall;
    public GameObject RightWall;
    public GameObject LeftWall;
    public GameObject ForwardWall;
    public GameObject BackWall;

    public float tankMinX ;
    public float tankMaxX ;
    public float tankMinY ;
    public float tankMaxY ;
    public float tankMinZ ;
    public float tankMaxZ ;

    public Text debug;
    private float startTime;        //開始時刻
    private float aveFPS = 0;       //平均FPS
    private int count = 0;          //計測回数 
    private bool check = false;             //計測終了スイッチ

    public float SurfaceY
    {
        get
        {
            return Surface.transform.position.y;
            
        }
    }

    private List<boidsScript> boids = new List<boidsScript>();

    public float[] weight = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        tankMaxX = RightWall.transform.position.x;
        tankMinX = LeftWall.transform.position.x;
        tankMaxY = Surface.transform.position.y;
        tankMinY = DownWall.transform.position.y;
        tankMaxZ = BackWall.transform.position.z;
        tankMinZ = ForwardWall.transform.position.z;

        //金魚召喚
        for (int i = 0; i < boidsNum; i++)
        {
            GameObject newBoid = Instantiate(boid,
                                             new Vector3(Random.Range(tankMinX, tankMaxX), Random.Range(tankMinY, tankMaxY), Random.Range(tankMinZ, tankMaxZ)),
                                             Random.rotation);
            boids.Add(newBoid.GetComponent<boidsScript>());
        }

        startTime = Time.time;      //開始時刻の保存
    }

    //フレームレート測定
    private void Update()
    {
        //30秒計測する
        if(Time.time - startTime < 30)
        {
            aveFPS += 1.0f / Time.deltaTime;
            count++;
        }
        else if(!check)
        {
            aveFPS /= count;
            check = true;
        }
        else
        {
            debug.text = "平均FPS：" + aveFPS;
        }
    }

    //シングルトン
    private static main instance;
    public static main Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<main>();
            }
            return instance;
        }
    }

    //boidとboidの正面方向にある壁との距離を求める
    public static float GetForwardWallDistance(boidsScript boid)
    {
        float dist = 0;

        //boidの前方方向にRayを飛ばして壁との交点を得る
        

        return dist;
    }

    //myself以外のboidをすべて含んだboidのリストを返す(生きてるやつのみ)
    public List<boidsScript> GetOtherBoids(boidsScript myself)
    {
        List<boidsScript> res = new List<boidsScript>();

        foreach(boidsScript boid in boids)
        {
            if (boid != myself && boid.ProcessSwitch)
            {
                res.Add(boid);
            }
        }

        return res;
    }

    //視界内の仲間を返す(生きてるやつのみ)
    public List<boidsScript> GetOtherBoidsInFOV(boidsScript myself)
    {
        List<boidsScript> res = new List<boidsScript>();

        foreach(boidsScript boid in boids)
        {
            if(boid != myself && boid.ProcessSwitch)
            {
                if(Vector3.Distance(boid.transform.position, myself.transform.position) <= myself.FOV)
                {
                    res.Add(boid);
                }
            }
        }

        return res;
    }

    public void RemoveBoid(boidsScript boid)
    {
        boids.Remove(boid);
        boid.enabled = false;
    }

    void Awake()
    {
        //FPS固定
        Application.targetFrameRate = 60;
    }


}
