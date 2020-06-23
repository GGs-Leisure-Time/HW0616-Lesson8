using UnityEngine;
using System.Linq;
//引用 系統.查詢語言 Lin Query

public class PeopleTrack : People
{
    /// <summary>
    /// 目標
    /// </summary>
    protected Transform target;

    public People[] people;

    /// <summary>
    /// 距離
    /// </summary>
    public float[] distance;

    protected virtual void Start()
    {
        //陣列 = 透過類型尋找物件<泛型>();
        people = FindObjectsOfType<People>();
        //距離陣列的數量 = 人類陣列的數量
        distance = new float[people.Length];
        //設定目的地(原點)，避免初期導覽錯亂
        agent.SetDestination(Vector3.zero);
    }

    private void Update()
    {
        Track();
    }

    protected virtual void Track()
    {
        //儲存所有人跟此物件的距離
        for (int i = 0; i < people.Length; i++) 
        {
            if(people[i]==null || people[i].transform.name== "Zombie" || people[i].transform.name == "Police")
            {
                //如果人類死亡，距離改為1000
                if (people[i] == null) distance[i] = 1000;
                //與殭屍的距離改為999
                else distance[i] = 999;
                //跳過並執行下一個迴圈
                continue;
            }

            distance[i] = Vector3.Distance(transform.position, people[i].transform.position);
        }
        //最小值 = 距離.最小值
        float min = distance.Min();
        //索引值 = 距離.轉清單.取得索引值(最小值)
        int index = distance.ToList().IndexOf(min);
        //目標 = 人類[索引值].變形
        target = people[index].transform;
        //追蹤最近目標
        agent.SetDestination(target.position);

        if (agent.remainingDistance <= 1f && min!=999) HitPeople();
    }

    private float timerhit;

    private void HitPeople()
    {
        if (timerhit >= 1f)
        {
            //計時器歸零
            timerhit = 0;
            //代理器停止
            agent.isStopped = true;
            ani.SetTrigger("ATK");
            target.GetComponent<People>().Dead();
        }
        else
        {
            agent.isStopped = false;
            //計時器累加
            timerhit += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall")
        {
            Dead();
        }
    }
}
