using UnityEngine;

public class PeopleFarAttack : PeopleTrack
{
    [Header("停止距離"),Range (1,10)]
    public float stop = 5f;
    [Header("子彈")]
    public GameObject bullet;
    [Header("冷卻"),Range(0.1f,3f)]
    public float cd = 1.5f;
    private float timer;

    protected override void Start()
    {
        //代理器.停止距離 = 停止距離
        agent.stoppingDistance = stop;
        //目標 = 殭屍
        target = GameObject.Find("Zombie").transform;
    }

    protected override void Track()
    {
        if (target == null) return;

        agent.SetDestination(target.position);
        transform.LookAt(target);
        if (agent.remainingDistance <= stop) Attack();
    }

    private void Attack()
    {
        timer += Time.deltaTime;

        //如果計時器>=冷卻時間
        if (timer >= cd)
        {
            timer = 0;
            ani.SetTrigger("ATK");
            GameObject temp = Instantiate(bullet, transform.position + transform.forward + transform.up, transform.rotation);
            Rigidbody rig = temp.AddComponent<Rigidbody>();
            rig.AddForce(transform.forward * 1500);
        }
    }
}
