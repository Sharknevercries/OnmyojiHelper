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
        /// 山兔大暴走
        /// </summary>
        [Description("山兔大暴走")]
        Yamausagi,

        /// <summary>
        /// 河畔童謠
        /// </summary>
        [Description("河畔童謠")]
        Kappa,

        /// <summary>
        /// 妖刀的秘笈 
        /// </summary>
        [Description("妖刀的秘笈")]
        Youtouhime,

        /// <summary>
        /// 紅葉的羈絆
        /// </summary>
        [Description("紅葉的羈絆")]
        Kijomomiji,

        /// <summary>
        /// 雨女的等候
        /// </summary>
        [Description("雨女的等候")]
        Ameonna,

        /// <summary>
        /// 荒川之怒
        /// </summary>
        [Description("荒川之怒")]
        Arakawanoaruji,

        /// <summary>
        /// 暴風之巔
        /// </summary>
        [Description("暴風之巔")]
        Ootengu,
    }
}
