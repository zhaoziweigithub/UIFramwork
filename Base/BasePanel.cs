using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // 界面被显示出来
    public virtual void OnEnter()
    {

    }

    //界面暂停，禁用页面交互
    public virtual void OnPause()
    {

    }

    // 界面继续，恢复页面交互
    public virtual void OnResume()
    {

    }

    // 界面不显示,点击叉号退出这个界面，界面被关闭
    public virtual void OnExit()
    {

    }
}
