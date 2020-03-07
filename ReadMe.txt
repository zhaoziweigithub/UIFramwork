使用方法：
1.在UIPanel文件夹里的UIPanelType写入UI的名字
2.在Resources文件夹里的UIPanelType的Json文件写入UI窗口的预制体名和预制体所在的路径
3.让每个UI窗口预制体继承自BasePanel。
4.在每个窗口中，如果要显示下一个要打开的窗口就调用UIManager.Instance.PushPanel(UIPanelType.要打开的窗口名);
5.在每个窗口中，如果要关闭本窗口就调用 UIManager.Instance.PopPanel();
6.每个窗口中，可以根据需求重写下面方法：OnExit(本页面关闭的操作)，OnEnter(本页面初显示时的操作)，OnResume(上一个页面退出后本页面的操作)，OnPause(下一个页面弹出时本页面的操作)。这些操作一般是设置本页面不能交互，或者设置界面隐藏和显示等。