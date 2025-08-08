## NovaFramework - Unity 教程示例

NovaFramework的教程示例库，提供框架使用的各个模块教程示例。

## 使用文档

1. 在应用加载模块增加==Game.Sample==程序集加载流程；
2. 在==Main==目录下的==GameSample==文件中修改当前运行的案例类型；
3. 需要启动==GameImport==导入模块，并在==GameConfig==文件中开启演示案例模块的跳转标识；
4. 运行程序，将自动转入对应案例类型的==SampleGate==并进入该案例的演示流程；

目前已有的演示案例包括：  
- [Text Format](Runtime/Text%20Format/README.md)
- Symbol Parser
- Inversion Of Control
- [Object Lifecycle](Runtime/Object%20Lifecycle/README.md)
- [Dispatch Call](Runtime/Dispatch%20Call/README.md)
- Dependency Inject
- [Performance Analysis](Runtime/Performance%20Analysis/README.md)

## 注意事项

使用方式(任选其一)

1. 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容：
    ```json
        {"com.novaframework.unity.sample": "https://github.com/yoseasoft/com.novaframework.unity.sample.git"}
    ```

2. 在Unity 的`Packages Manager` 中使用`Git URL` 的方式添加库,地址为：
https://github.com/yoseasoft/com.novaframework.unity.sample.git

3. 直接下载仓库放置到Unity 项目的`Packages` 目录下，会自动加载识别。
