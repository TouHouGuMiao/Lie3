﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaControl : CharacterPropBase {
    public Transform target;
    private Animator m_Animator;

    private GameObject prefab;
    private Transform point;
    private float m_HP;
    private float m_RunSpeed;
    private float time;

    private float disantce
    {
        get
        {
            return Vector2.Distance(transform.position, target.position);
        }
    }

  
    // Use this for initialization
    void Start () {
        m_Animator = this.GetComponent<Animator>();
        m_RunSpeed = speed+2;
        prefab = ResourcesManager.Instance.LoadBullet("StarBullet");
        point = transform.FindRecursively("point");
        MarisaSkillManager.Instance.InitMarisaSkills();
        m_HP = HP;

        AudioManager.Instance.PlayBg_Source("DemoBGM");
        StartCoroutine(DemoShow());
        //StartCoroutine(UseMarisaLockBullet());
        //StartCoroutine(UseHeiDongBianYuan_WanZheng());

        //StartCoroutine(UseHeiDongBianYuan());
        //StartCoroutine(UseHeiDongInBullet());
        //StartCoroutine(UseXiaoSanBullet());
        //StartCoroutine(UseDrawFiveStars());
        //StartCoroutine(UseRotateBullet());
        //StarBattleContinue();
    }

    void StarBattleContinue()
    {
        StartCoroutine(UseMairsaBoom());
        StartCoroutine(UseMarisaSphere());
        StartCoroutine(UseMarisaMix());
    }


    bool PlayerIsRun = false;
	// Update is called once per frame
	void Update () {


        if (target == null)
        {
            FindPlayerByTag();
        }


        if (target != null)
        {
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (disantce >= 30)
            {

                if (stateInfo.IsName("Base Layer.PuGong"))
                {
                    m_Animator.SetInteger("Skill", 0);
                }
                m_Animator.SetBool("Walk", true);
            }

            if (stateInfo.IsName("Base Layer.Walk"))
            {
                WalkPostiton();
            }

            if (disantce <= 10)
            {
                m_Animator.SetBool("Walk", false);
            }




        }
      
    }

 

    void WalkPostiton()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * m_RunSpeed, Space.Self);
        if (m_RunSpeed >= 40)
        {
            m_Animator.SetBool("Walk", false);
            InitRotationEuler();
        }
  
    }

    public void StartRun()
    {
        time = 0;
        StartCoroutine(UpdataSpeed());
    }

    IEnumerator UpdataSpeed()
    {
        bool isRun = m_Animator.GetBool("Walk");
        if (!isRun)
            time=0;
            yield return 0;

        yield return new WaitForSeconds(1);
        time += 1;
        m_RunSpeed = speed + 5.0f*time;
        StartCoroutine(UpdataSpeed());
       
    }

   

    private void FindPlayerByTag()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LockPlayer()
    {
        LookPlayer();

        Vector3 vecPos = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.root.eulerAngles.z+angle);
    }


    private void LookPlayer()
    {
        float index_x = transform.position.x - target.position.x;
        if (index_x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        else if (index_x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }

    }



    #region 帧事件方法调用
    public void UsePuGong()
    {
        MarisaSkillManager.Instance.ShowPuGong(target, point);
        //MarisaSkillManager.Instance.ShowSphereBullets(transform.position);
        //MarisaSkillManager.Instance.ShowNorStarBulltes(target, point);
        //MarisaSkillManager.Instance.ShowBoomBulltes(transform.position);
        //MarisaSkillManager.Instance.ShowMixBullet(transform);
    }

    public void UsePingBoomBullet()
    {
        LookPlayer();
        MarisaSkillManager.Instance.ShowPingBoomBullet(point.position, transform);
         
        //MarisaSkillManager.Instance.ShowDrawFiveStarBullet(transform);

    }
    #endregion


    

    private void OnTriggerEnter(Collider other)
    {
        BulletBase m_Base = other.GetComponent<BulletBase>();
        if (m_Base == null)
        {
            return;
        }

        if (m_Base.m_Type == BulletBase.BulletTpye.playerBullet)
        {
            float injured = m_Base.injured;
            injured = injured * defenseLV;
            m_HP = BattleCommoUIManager.Instance.UpdataHP_Boss("marisa", m_HP, injured, -1);
            GameObject.Destroy(m_Base.gameObject);
        }
    }


    IEnumerator DemoShow()
    {
      

        yield return new WaitForSeconds(15);
        MarisaSkillManager.Instance.ShowDemoSphere(target.position);
        MarisaSkillManager.Instance.ShowHeiDongBianYuanBullet(transform);
        yield return new WaitForSeconds(10);
        MarisaSkillManager.Instance.ShowHeiDongInBullet(transform.position);
        yield return new WaitForSeconds(6);
        MarisaSkillManager.Instance.ShowRotateBullet(transform,target);
        yield return new WaitForSeconds(5);
        MarisaSkillManager.Instance.ShowHeiDongBianYuanBullet(transform);
        yield return new WaitForSeconds(5);
        MarisaSkillManager.Instance.ShowRotateBullet(transform, target);
        yield return new WaitForSeconds(5);
        MarisaSkillManager.Instance.ShowHeiDongInBullet(transform.position);
        yield return new WaitForSeconds(9f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        Vector3 centerPos = target.position;
        MarisaSkillManager.Instance.ShowDrawFiveStarBullet(centerPos, 10);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        MarisaSkillManager.Instance.ShowDrawFiveStarBullet(centerPos, 12);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        MarisaSkillManager.Instance.ShowDrawFiveStarBullet(centerPos, 14);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(1.7f);
        MarisaSkillManager.Instance.ShowBoomInTarget(target.position);
        yield return new WaitForSeconds(4f);
        MarisaSkillManager.Instance.ShowTailLockBullet(transform, target);
        yield return new WaitForSeconds(6);
        MarisaSkillManager.Instance.ShowTailLockBullet(transform, target);
        yield return new WaitForSeconds(4);
        MarisaSkillManager.Instance.ShowTailLockBullet(transform, target);
        yield return new WaitForSeconds(23.5f);
        MarisaSkillManager.Instance.ShowRotateOutBullet(target.position);
        yield return new WaitForSeconds(5.1f);
        MarisaSkillManager.Instance.ShowBoomInTarget(new Vector3(target.position.x - 4f, target.position.y));
        yield return new WaitForSeconds(0.1f);
        MarisaSkillManager.Instance.ShowBoomInTarget(new Vector3(target.position.x , target.position.y-4));
        yield return new WaitForSeconds(0.1f);
        MarisaSkillManager.Instance.ShowBoomInTarget(new Vector3(target.position.x+4, target.position.y));
        yield return new WaitForSeconds(0.1f);
        MarisaSkillManager.Instance.ShowBoomInTarget(new Vector3(target.position.x , target.position.y+4));
        m_Animator.SetInteger("Skill", 3);
        yield return new WaitForSeconds(12);
        m_Animator.SetInteger("Skill", 0);

        yield return new WaitForSeconds(2);
        Vector3 targetPoint = target.position;
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 6, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 3, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 0, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 3, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 6, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        //yield return new WaitForSeconds(0.2f);
        //MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 6, targetPoint.y - 10, 0), new Vector3(0, 1, 0), target.position, true, true);
        yield return new WaitForSeconds(3f);
        targetPoint = target.position;
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x -10, targetPoint.y - 4, 0), new Vector3(1,0 , 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 10, targetPoint.y - 2, 0), new Vector3(1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 10, targetPoint.y - 0, 0), new Vector3(1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 10, targetPoint.y +2, 0), new Vector3(1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 10, targetPoint.y + 4, 0), new Vector3(1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(2.5f);
        targetPoint = target.position;
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 6, targetPoint.y +10, 0), new Vector3(0, -1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 3, targetPoint.y + 10, 0), new Vector3(0, -1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 0, targetPoint.y + 10, 0), new Vector3(0, -1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 3, targetPoint.y + 10, 0), new Vector3(0, -1, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 6, targetPoint.y + 10, 0), new Vector3(0, -1, 0), target.position, true, true);
        yield return new WaitForSeconds(2.5f);
        targetPoint = target.position;
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 10, targetPoint.y - 4, 0), new Vector3(-1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 10, targetPoint.y - 2, 0), new Vector3(-1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 10, targetPoint.y - 0, 0), new Vector3(-1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 10, targetPoint.y + 2, 0), new Vector3(-1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(0.2f);
        MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x + 10, targetPoint.y + 4, 0), new Vector3(-1, 0, 0), target.position, true, true);
        yield return new WaitForSeconds(3f);
        LockPlayer();
        MarisaSkillManager.Instance.ShowMoPao(point, target.position);
        //yield return new WaitForSeconds(0.2f);
        //MarisaSkillManager.Instance.ShowZhiXianInstateBullet(new Vector3(targetPoint.x - 10, targetPoint.y + 6, 0), new Vector3(1, 0, 0), target.position, true, true);

    }



    IEnumerator UseHeiDongBianYuan_WanZheng()
    {
        StartCoroutine(UseHeiDongBianYuan());
        yield return new WaitForSeconds(15);
        //StartCoroutine(UseHeiDongInBullet());
    }

    IEnumerator UseHeiDongInBullet()
    {
 
        MarisaSkillManager.Instance.ShowHeiDongInBullet(transform.position);
        yield return new WaitForSeconds(30);
        //StartCoroutine(UseHeiDongInBullet());
    }
    IEnumerator UseHeiDongBianYuan()
    { 
        MarisaSkillManager.Instance.ShowHeiDongBianYuanBullet(transform);
        yield return new WaitForSeconds(30);
        //StartCoroutine(UseHeiDongBianYuan());
    }

    IEnumerator UseDrawFiveStars()
    {
        yield return new WaitForSeconds(10);
        MarisaSkillManager.Instance.ShowDrawFiveStarBullet(target.position,12);
    
        //StartCoroutine(UseDrawFiveStars());
    }

    IEnumerator UseRotateBullet()
    {
       
        MarisaSkillManager.Instance.ShowRotateBullet(transform,target);
        yield return new WaitForSeconds(4);
    }

    public void InitRotationEuler()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    IEnumerator UseXiaoSanBullet()
    {
        yield return new WaitForSeconds(6);
        MarisaSkillManager.Instance.ShowXiaoSanBullet(transform.position);
        //StartCoroutine(UseXiaoSanBullet());
    }




    IEnumerator UseMarisaSphere()
    {
        MarisaSkillManager.Instance.ShowSphereBullets(transform.position);
        yield return new WaitForSeconds(3);
        StartCoroutine(UseMarisaSphere());
    }

    IEnumerator UseMairsaBoom()
    {
        MarisaSkillManager.Instance.ShowBoomBulltes(transform.position);
        yield return new WaitForSeconds(4);
        StartCoroutine(UseMairsaBoom());
    }

    IEnumerator UseMarisaMix()
    {
        MarisaSkillManager.Instance.ShowMixBullet(transform);
        yield return new WaitForSeconds(5);
        StartCoroutine(UseMarisaMix());
    }

    IEnumerator UseMarisaLockBullet()
    {
        yield return new WaitForSeconds(1);
        MarisaSkillManager.Instance.ShowLockBoomBullet(point, target);
        yield return new WaitForSeconds(15);
        StartCoroutine(UseMarisaLockBullet());
    }

  
}
