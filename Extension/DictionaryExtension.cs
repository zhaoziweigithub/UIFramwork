using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public static class DictionaryExtension // 对Dictory的扩展
{
    // 尝试根据key得到value，得到了的话直接返回value，没有得到直接返回null
    // this Dictionary<Tkey,Tvalue> dict 这个字典表示我们要获取值的字典，字典类型未知要用泛型，表示要调用该方法的字典对象
    //TryGet方法是给字典对象调用的
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)//返回值的类型类Tvalue类型
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}
