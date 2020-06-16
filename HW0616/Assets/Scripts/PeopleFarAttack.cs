using UnityEngine;

public class PeopleFarAttack : PeopleTrack
{
    [Header("停止距離"),Range (1,10)]
    public float stop = 5f;

    protected override void Start()
    {
        //代理器.停止距離 = 停止距離
        agent.stoppingDistance = stop;
        //目標 = 殭屍
        target = GameObject.Find("Zombie").transform;
    }

    protected override void Track()
    {
        agent.SetDestination(target.position);
    }

}
