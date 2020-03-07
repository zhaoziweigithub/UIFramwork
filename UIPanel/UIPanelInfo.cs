using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UIPanelInfo : ISerializationCallbackReceiver
{//该接口是与jsonUtility对应的
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString; 
    public string path;

    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()//每个类反序列化后调用该方法
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);//将反序列化后的字符串转为枚举类型，typeof取到UIPanelType的类型(枚举)，第二个参数为要转化的值
        panelType = type;
    }

    public void OnBeforeSerialize()//每次序列化之前调用该方法
    {

    }
}
