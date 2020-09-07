//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年5月14日 22:44:27
//------------------------------------------------------------

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace ETModel
{
    [Title("Buff数据块", TitleAlignment = TitleAlignments.Centered)]
    [HideLabel]
    public class BuffDataBase
    {
        [LabelText("Buff的Id")]
        [BoxGroup("必填项")]
        public VTD_Id BuffId = new VTD_Id();
        
        [LabelText("归属的技能ID")]
        [BoxGroup("必填项")]
        public int BelongSkillId;

        [ReadOnly]
        public BuffSystemType BelongBuffSystemType;

        [LabelText("Buff的添加目标")]
        [BoxGroup("必填项")]
        public BuffTargetTypes BuffTargetTypes;

        [LabelText("Buff的基本特征")]
        [BoxGroup("必填项")]
        public BuffBaseType BuffBaseType;

        [ShowIf("Base_isVisualable")]
        [LabelText("Buff图标的名称")]
        [BoxGroup("选填项")]
        public string SpriteABInfo;

        [BoxGroup("选填项")]
        [LabelText("Buff是否状态栏可见")]
        public bool Base_isVisualable;

        [LabelText("要抛出的事件ID，如果有的话")]
        [BoxGroup("选填项")]
        public List<VTD_EventId> EventIDs = new List<VTD_EventId>();

        [LabelText("是否可以叠加(不能叠加就刷新，叠加满也刷新)")]
        [BoxGroup("选填项")]
        public bool CanOverlay;

        [ShowIf("CanOverlay")]
        [LabelText("叠加层数")]
        [MinValue(1)]
        [BoxGroup("选填项")]
        public int TargetOverlay = 1;

        [ShowIf("CanOverlay")]
        [LabelText("最大叠加数")]
        [BoxGroup("选填项")]
        public int MaxOverlay;

        [LabelText("Buff持续时间,-1代表永久,0代表此处设置无效")]
        [BoxGroup("选填项")]
        public long SustainTime;

        [LabelText("Buff效果为")]
        [BoxGroup("选填项")]
        public BuffWorkTypes BuffWorkType;

        [LabelText("Buff基础数值影响者")]
        [BoxGroup("选填项")]
        public BuffBaseDataEffectTypes BaseBuffBaseDataEffectTypes;

        [LabelText("将要被改变的基础数值")]
        [BoxGroup("选填项")]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, float> ValueToBeChanged = new Dictionary<int, float>();

        [LabelText("具体的加成(可能会一个效果多种加成方式)")]
        [BoxGroup("选填项")]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<BuffAdditionTypes, float> AdditionValue = new Dictionary<BuffAdditionTypes, float>();
    }
}