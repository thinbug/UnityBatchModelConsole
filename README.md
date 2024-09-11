# UnityBatchModelConsole
想通过Unity的batchmode来制作服务端程序，但是没有控制台程序，本测试2个用例，个人推荐第二个网络方式查看服务端日志。
网络模块是Kcp的，网络部分的代码参考我的另外一个kcp_demo库，可以用自己的UDP或者Tcp程序代替。


用例1:直接在Unity中显示Console
目录:
UnityConsole:
Unity的Batchmode模式运行的程序无窗口，这个项目是给他加上控制台窗口，并且显示gb2132中文。
这个模式不好用,参考用例2通过网络同步.
![image](https://github.com/thinbug/UnityBatchModelConsole/assets/25097168/a7dd823e-18fc-4294-b80c-e0a51093985a)


用例2:通过网络的控制台
目录:
UnityConsoleNetwork : Unity的服务端
ConsoleClient:Unity的客户端Console工具
控制台的DLL是x64的,运行时候选择x64

![1](https://github.com/thinbug/UnityBatchModelConsole/assets/25097168/95368f86-d39e-45b0-b890-08ab73d671b4)
