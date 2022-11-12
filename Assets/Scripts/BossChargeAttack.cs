using UnityEngine;
using DuncanUtilities;
using Mirror;

public class BossChargeAttack : NetworkBehaviour
{
    public float BuildUpTime;
    public float ChargeTime;
    public float RestTime;

    public float ChargeForce;

    CharacterController characterController;
    GameObject GFX_GO;
    Boss bossScript;
    Timer timer;

    
    enum attackState { buildUp = 0, charge, rest }
    [SerializeField] attackState currentAttackState;
    float[] stateTimes;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        characterController = GetComponent<CharacterController>();
        GFX_GO = transform.Find("GFX").gameObject;
        bossScript = GetComponent<Boss>();

        stateTimes = new float[] {BuildUpTime, ChargeTime, RestTime };
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ServerCallback]
    private void FixedUpdate()
    {
        FixedUpdateAttack();
    }


    [Server]
    void FixedUpdateAttack()
    {
        switch (currentAttackState)
        {
            case attackState.buildUp:
                // Shaking animation
                if (GFX_GO.transform.localPosition == Vector3.zero)
                    GFX_GO.transform.localPosition += Tools.randomVector3(0.25f);
                else
                    GFX_GO.transform.localPosition = Vector3.zero;
                break;
            case attackState.charge:
                characterController.Move(bossScript.BossToplayerDir.normalized * ChargeForce);
                break;
            case attackState.rest:
                break;
            default:
                break;
        }

        if (timer.Time >= timer.Target)
        {
            if (currentAttackState == attackState.rest)            
                currentAttackState = attackState.buildUp;            
            else
                currentAttackState++;

            timer.Reset(stateTimes[(int)currentAttackState]);
        }
            
    }

    // TODO: Add to utilities class?


    //class Timer
    //{
    //    public float Target;
    //    public float Time { get; private set; } = 0f;
    //    public delegate void TimerReachedDelegate();
    //    TimerReachedDelegate onTimerReached;

    //    public Timer(float target)
    //    {
    //        Target = target;
    //    }

    //    public void Reset(float newTarget)
    //    {
    //        Time = 0f;
    //        Target = newTarget;
    //    }

    //    public void Update()
    //    {
    //        Time += UnityEngine.Time.deltaTime;

    //        if (Time >= Target)
    //        {
    //            onTimerReached.Invoke();
    //        }
    //    }
    //}
}
