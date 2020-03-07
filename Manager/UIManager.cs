using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIManager
{

    /// 
    /// 单例模式的核心
    /// 1，定义一个静态的对象 在外界访问 在内部构造
    /// 2，构造方法私有化，在外界无法构造，保证UIManager的单实例

    private static UIManager _instance;

    public static UIManager Instance //给单例_instance赋值，外界访问改静态的get方法就可以构造，同时调用其构造函数
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();//整个项目只会在这里构造它自身，为单实例
            }
            return _instance;
        }
    }

    private Transform canvasTransform;

    private Transform CanvasTransform//实例化出来的面板放在Canvas里，通过transform设置父子等级关系
    {
        get
        {
            if (canvasTransform == null)
            {
               canvasTransform= GameObject.Find("Canvas").transform;
            }

            return canvasTransform;
        }
    }

    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板Prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件，key为面板的type，值为游戏物体身上的组件(basepanel面板)，需要用面板时就把它实例化放在字典里，每次用时就可以在字典中取
    private Stack<BasePanel> panelStack;//每个页面为BasePanel类型的，故栈的类型为BasePanel
    
    //入栈
    public void PushPanel(UIPanelType panelType)//根据类型得到界面然后入栈
    {
        if (panelStack == null)//如果是空栈要构造出来
        {
            panelStack=new Stack<BasePanel>();
        }
        //判断一下栈里面是否有页面，有页面把该页面暂停，再添加新的页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();//取出栈顶页面元素，但不移除，让栈顶的页面暂停，Pop方法才移除。
            topPanel.OnPause();
        }

        BasePanel panel= GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    ///出栈 ，把页面从界面上移除
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();
        if (panelStack.Count <= 0) return;//栈顶无页面就不需要移除
        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();//取出栈顶panel并移除
        topPanel.OnExit();
        if (panelStack.Count <= 0) return;//判断移除后还有没有页面
        BasePanel topPanel2 = panelStack.Peek();//显示栈顶页面
        topPanel2.OnResume();
    }

    private BasePanel GetPanel(UIPanelType panelType)// 根据面板类型 得到实例化的面板，控制面板的显示隐藏跳转
    {
        if (panelDict == null)
        {
            panelDict=new Dictionary<UIPanelType, BasePanel>();//没有字典就创空字典
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//根据类型得到panel
        BasePanel panel = panelDict.TryGet(panelType);
        if (panel == null)//没有实例化panelType类型的面板
        {
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
           GameObject instPanel= GameObject.Instantiate(Resources.Load(path)) as GameObject;//通过路径实例化要加载面板的prefab
            instPanel.transform.SetParent(CanvasTransform,false);//实例化出来的面板放在Canvas里，第二个参数位是否保持世界坐标的位置，Canvas是有Scale(缩放比例)的，把外界实例化的panel放在Cannvas下，面板的Scale会不正常，设为false表示放在Canvas里保持panel的局部rotation和position
            panelDict.Add(panelType,instPanel.GetComponent<BasePanel>());//面板实例化出来(存其BasePanel组件)就放在字典里
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    private UIManager()//UIManager作为单例模式(单实例，只有一个对象)，构造方法要私有化，在外界不能调用其构造方法，该类不能在外界实例化，实例化对象只会实例化一次，在实例时进行初始化
    {
        ParseUIPanelTypeJson();//解析json
    }

    [Serializable]
    class UIPanelTypeJson//定义内部类
    {
        public List<UIPanelInfo>infoList;
    } 
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();//空字典，存到字典方便查找，方便根据paneltype(Key)取查找路径(value)

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");//通过TextAsset属性获取UIPanelType.json的所有文本信息，并进行解析

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//对象
         //读取json，<>表示要转化为什么类型(List或数组)，把json信息转递过来，再把信息转化到字典中。
        foreach (UIPanelInfo info in jsonObject.infoList)   
        {
            //Debug.Log(info.panelType);    
            panelPathDict.Add(info.panelType, info.path); //每个Info包含两个信息:Type(Key)和Path(Value)
        }
       
    }

    //public void Test()
    //{
    //    string path;
    //    panelPathDict.TryGetValue(UIPanelType.Knapsack, out path);
    //    Debug.Log(path);
    //}
}
