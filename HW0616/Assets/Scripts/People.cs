using UnityEngine;
using UnityEngine.AI;

public class People : MonoBehaviour
{
    [Header("移動速度"), Range(0,10)]
    public float speed = 1.5f;

    /// <summary>
    /// 導覽代理器
    /// </summary>
    protected NavMeshAgent agent;

    /// <summary>
    /// 動畫控制器
    /// </summary>
    protected Animator ani;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        agent.speed = speed;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead() 
    {
        //動畫控制器.設定觸發("死亡")
        ani.SetTrigger("Death");
        agent.isStopped = true;
        Destroy(gameObject, 1.5f);

    }
}
