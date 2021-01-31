﻿using System.Collections.Generic;

namespace Protocol.CommonData
{
    [System.Serializable]
    public class BorrowInformatio
    {
        /// <summary>
        /// 借款id
        /// </summary>
        public int id;

        /// <summary>
        /// 账号
        /// </summary>
        public string account;

        /// <summary>
        /// 总借款金额
        /// </summary>
        public float allMoney;

        /// <summary>
        /// 利率
        /// </summary>
        public float rateInterest;

        /// <summary>
        /// 分期期数
        /// </summary>
        public int stagesNumber;

        /// <summary>
        /// 借款日期
        /// </summary>
        public int dateTime;

        /// <summary>
        /// 贷款状态
        /// </summary>
        public BorrowState borrowState;

        /// <summary>
        /// 还款记录
        /// </summary>
        public List<PaymentInfo> paymentInfos = new List<PaymentInfo>();

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    [System.Serializable]
    public enum BorrowState
    {
        /// <summary>
        /// 等待审批
        /// </summary>
        WaitApproval = 0,

        /// <summary>
        /// 已审批
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = 2,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 3,

        /// <summary>
        /// j结清
        /// </summary>
        Settle = 4,
    }

    [System.Serializable]
    public struct PaymentInfo
    {
        /// <summary>
        /// 还款id
        /// </summary>
        public int id;

        /// <summary>
        /// 还款日期
        /// </summary>
        public long dateTime;

        /// <summary>
        /// 还款金额
        /// </summary>
        public float allMoney;

        /// <summary>
        /// 利息
        /// </summary>
        public float interestMoney;

        /// <summary>
        /// 本金
        /// </summary>
        public float principal;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
