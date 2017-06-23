using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmyojiHelper.Models.Enums
{
    public enum StageCategory
    {
        /// <summary>
        /// 普通探索
        /// </summary>
        [Description("普通探索")]
        NormalDiscovery,

        /// <summary>
        /// 困難探索
        /// </summary>
        [Description("困難探索")]
        HardDiscovery,

        /// <summary>
        /// 御魂
        /// </summary>
        [Description("御魂")]
        RoyalSoul,

        /// <summary>
        /// 叢原火
        /// </summary>
        [Description("叢原火")]
        Sougenbi,

        /// <summary>
        /// 妖氣封印
        /// </summary>
        [Description("妖氣封印")]
        SealMonster,

        /// <summary>
        /// 挑戰券
        /// </summary>
        [Description("挑戰券")]
        Challenge,

        /// <summary>
        /// 祕聞副本
        /// </summary>
        [Description("祕聞副本")]
        Secret,
    }
}
