using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : IView
{
    public LoginPanel()
    {
        m_Layer = Layer.bottom;
    }  
 
    struct ValueBtn {
        private bool isRight;
    }
    

    private UIButton loginButton;
    private UIButton developerBtn;
    private UIButton closeGameBtn;
    private UIButton againGameBtn;   
    private UIButton seetingsBtn;

    private UISprite changeWordPic;

    private  GameObject m_go;
    public static GameObject GameChoice;
    public static  GameObject BackGround;//LoginPanel的背景图片
   

    private List<UIButton> choiceBtnList = new List<UIButton>();
    private List<bool> eggChangeList = new List<bool>();

    


    private Transform gameChoice;
    private ParticleSystem paritcleSystem_Normal;
    private ParticleSystem particleSystem_Normal_1;
    private ParticleSystem.Particle[] particleArray_Normal;
    private ParticleSystem sakuraParticle_1;
    private ParticleSystem sakuraParticle_2;

    private bool isParticleSetting_Up = false;
    //private GameObject go;
    private Vector3 ogPos = new Vector3(1300, -1, 0);
    private Vector3 changePos = new Vector3(200, -1, 0);
    private GameObject lieLogo;
    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        paritcleSystem_Normal.gameObject.SetActive(true);
        sakuraParticle_1.gameObject.SetActive(false);
        sakuraParticle_2.gameObject.SetActive(false);
        particleSystem_Normal_1.gameObject.SetActive(false);
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = paritcleSystem_Normal.velocityOverLifetime;
        velocityOverLifetimeModule.enabled = false;
        GameChoice.transform.localPosition = ogPos;
    }

    protected override void OnShow()
    {
        lieLogo.gameObject.SetActive(true);
        loginButton.isActive_Button = false;
        developerBtn.isActive_Button = false;
        closeGameBtn.isActive_Button = false;
        againGameBtn.isActive_Button = false;
        seetingsBtn.isActive_Button = false;
    }
    public override void Update() {
       // OnHoverChangeWord();  
    }


    protected override void OnStart()
    {       
        loginButton = this.GetChild("LoginButton").GetComponent<UIButton>();      
        developerBtn = this.GetChild("LoginButton (4)").GetComponent<UIButton>();
        closeGameBtn = this.GetChild("LoginButton (5)").GetComponent<UIButton>();
        lieLogo = this.GetChild("LieLogo").gameObject;
        againGameBtn = this.GetChild("LoginButton (1)").GetComponent<UIButton>();
        seetingsBtn = this.GetChild("LoginButton (2)").GetComponent<UIButton>();
        m_go = this.GetChild("ButtonGrid").gameObject;      
        BackGround = this.GetChild("bg").gameObject;
        GameChoice = this.GetChild("GameChoice").gameObject;
       

        //addElementInEggChangeList();                       
        paritcleSystem_Normal = this.GetChild("NormalParticle").GetComponent<ParticleSystem>();
        particleArray_Normal= new ParticleSystem.Particle[paritcleSystem_Normal.main.maxParticles];

        particleSystem_Normal_1 = this.GetChild("NormalParticle1").GetComponent<ParticleSystem>();

        sakuraParticle_1 = this.GetChild("SakuraParticle1").GetComponent<ParticleSystem>();
        sakuraParticle_2 = this.GetChild("SakuraParticle2").GetComponent<ParticleSystem>();

        addDelegate();      
        ColorEgg();
        OnLoginBtnHover();

        TweenAlpha ta = lieLogo.GetComponent<TweenAlpha>();
        ta.onFinished.Add(new EventDelegate(BGMSet));
    }


    void OnLoginBtnClick()
    {
        AudioManager.Instance.CloseBg_Source();

        //GameStateManager.LoadScene(2);//车人场景是2
        GameStateManager.LoadScene(4);//stage0
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "BattleUIPanel";
        //LoadingPanel.LoadingName = "SkillPanel";
        //LoadingPanel.LoadingName = "SurePropertyPanel";
        LoadingPanel.useCover = true;
    }


    private List<Transform> m_PicList=new List<Transform> ();
    void OnLoginBtnHover(){//鼠标悬浮选项放大的事件
      //  GameObject go = GameObject.Find("ButtonGrid");
        foreach (Transform child in m_go.transform) {
            m_PicList.Add(child);
        }
        
        BtnControl();
        
    }
    void BtnControl()
    {//悬停鼠标功能控制
        for (int i = 0; i < m_PicList.Count; i++) {
            m_PicList[i].gameObject.AddComponent<ShowChoiceMark>();
        }
    }

    void OnDeveloperBtnClick() {//开发人员界面显示       
        GUIManager.HideView("LoginPanel");
        GUIManager.ShowView("DeveloperPanel");
    }
    void OnCloseGameBtn() {
        Application.Quit();

    }
    void addDelegate() {
        EventDelegate OnLoginClick = new global::EventDelegate(OnLoginBtnClick);
        loginButton.onClick.Add(OnLoginClick);
       

        EventDelegate OnDeveloperBtn = new global::EventDelegate(OnDeveloperBtnClick);
        developerBtn.onClick.Add(OnDeveloperBtn);

        EventDelegate OnCloseBtn = new global::EventDelegate(OnCloseGameBtn);
        closeGameBtn.onClick.Add(OnCloseBtn);
        seetingsBtn.onClick.Add(new EventDelegate(OnSettingsBtnClick));
    }
    void OnSettingsBtnClick() {
        GUIManager.ShowView("CoverPanel");
        GUIManager.HideView("LoginPanel");       
        GUIManager.ShowView("SettingsPanel");
    }
    void ColorEgg()
    {//彩蛋事件
        foreach (Transform child in m_go.transform)
        {
            choiceBtnList.Add(child.GetComponent<UIButton>());
            // Debug.Log(child.name);
        }
        
    }
    //int num=0;
    void OnHoverChangeWord() {
        if (loginButton.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "one";
           // loginButton_b = true;
            loginButton.isActive_Button = true;
           
        }               
        else if (developerBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "five";
            // developerBtn_b = true;
            developerBtn.isActive_Button = true;
        }
        else if (closeGameBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "six";
            //   closeGameBtn_b = true;
            closeGameBtn.isActive_Button = true;
        }
       else if (seetingsBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "zero";
            // seetingsBtn_b = true;
            seetingsBtn.isActive_Button = true;
        }
        else if (againGameBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "two";
            // againGameBtn_b = true;
            againGameBtn.isActive_Button = true;
        }
        ChangeCover();
    }
    bool isAllboolVauletrue=false;
    void ChangeCover() {
        foreach (UIButton child in choiceBtnList) {
            if (child.isActive_Button == true)
            {
                isAllboolVauletrue = true;
            }
            else {
                isAllboolVauletrue = false;
                return;
            }
        }
        if (isAllboolVauletrue == true)
        {

            if (BackGround.GetComponent<UITexture>().mainTexture.name == "BinaryCover_Kong")
            {
                return;
            }
           
            foreach (UIButton child in choiceBtnList) {
                child.isActive_Button = false;
            }
            if (BackGround.GetComponent<UITexture>().mainTexture.name == "BinaryCover_Zi"){ // == ResourcesManager.Instance.LoadTexture2D("BinaryCover_Zi")) {
                BackGround.GetComponent<UITexture>().mainTexture = ResourcesManager.Instance.LoadTexture2D("BinaryCover_Kong");
                GUIManager.ShowView("CoverPanel");
                GameChoice.transform.localPosition = changePos;
            }


        }
        
        // Debug.Log("isAllboolVauletrue="+isAllboolVauletrue);
    }
   

    void BGMSet()
    {
        lieLogo.gameObject.SetActive(false);
        AudioManager.Instance.PlayBg_Source("LoginBGM",true);
        ParticleSystem.EmissionModule emission = paritcleSystem_Normal.emission;
        emission.rateOverTime = 4;
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Pasue), "LoginBGM", 50.4f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Up), "LoginBGM", 70.5f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Normal1Up), "LoginBGM", 84.5f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSakuRaShow), "LoginBGM", 101.5f);
    }



    void ParticleSetting_Pasue()
    {

        ParticleSystem.EmissionModule emission = paritcleSystem_Normal.emission;

        emission.rateOverTime = 0;
        paritcleSystem_Normal.GetParticles(particleArray_Normal);
        for (int i = 0; i < particleArray_Normal.Length; i++)
        {

            particleArray_Normal[i].velocity = new Vector3(particleArray_Normal[i].velocity.x * 0.4f, particleArray_Normal[i].velocity.y * 0.4f, particleArray_Normal[i].velocity.z * 0.4f);
        }
        paritcleSystem_Normal.SetParticles(particleArray_Normal, particleArray_Normal.Length);
    
     
        
    }

    void ParticleSetting_Up()
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = paritcleSystem_Normal.velocityOverLifetime;
        velocityOverLifetimeModule.enabled = true;
        velocityOverLifetimeModule.y = 0.7f;
    }

    void ParticleSetting_Normal1Up()
    {
        particleSystem_Normal_1.gameObject.SetActive(true);
    }

    void ParticleSakuRaShow()
    {
        GUIManager.ShowView("CoverPanel");
        paritcleSystem_Normal.gameObject.SetActive(false);
        particleSystem_Normal_1.gameObject.SetActive(false);
        sakuraParticle_1.gameObject.SetActive(true);
        sakuraParticle_2.gameObject.SetActive(true);
    }   
}
 