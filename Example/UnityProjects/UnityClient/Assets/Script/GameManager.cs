﻿using Protocol.CommonData;

public enum ServetType
{
    Local = 1,
    Tencent = 2,
}

public class GameManager : SingleBase<GameManager>
{
    public ServetType servetType = ServetType.Tencent;

    public PushTextDlg PushTextDlg;
    public LoginDlg LoginDlg;
    public RegisterAccountDlg registerAccountDlg;
    public GameStart GameStart;
    public MainDlg MainDlg;
    public GmDlg GmDlg;
    public HGmDlg HGmDlg;
    public GmAccountSetBorrowDlg gmAccountSetBorrowDlg;
    public GMSetRepaymentDlg gMSetRepaymentDlg;

    public CommonAccountData userData;
    public AppData appData;

    void Start()
    {
        DontDestroyOnLoad(this);
        PushTextDlg.gameObject.SetActive(true);
        PushTextDlg.gameObject.SetActive(false);
        gMSetRepaymentDlg.gameObject.SetActive(false);
        gmAccountSetBorrowDlg.gameObject.SetActive(false);
        //registerAccountDlg.gameObject.SetActive(false);
        //GameStart.gameObject.SetActive(false);
        //MainDlg.gameObject.SetActive(false);
        //GmDlg.gameObject.SetActive(false);
        //HGmDlg.gameObject.SetActive(false);
        LoginDlg.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        NotifyManager.Update();
    }
}
