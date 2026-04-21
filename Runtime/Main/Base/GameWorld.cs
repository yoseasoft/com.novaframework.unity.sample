/// -------------------------------------------------------------------------------
/// Copyright (C) 2024 - 2025, Hurley, Independent Studio.
/// Copyright (C) 2025, Hainan Yuanyou Information Technology Co., Ltd. Guangzhou Branch
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in
/// all copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
/// THE SOFTWARE.
/// -------------------------------------------------------------------------------

namespace GameFramework.Sample
{
    /// <summary>
    /// 演示案例总控
    /// </summary>
    static partial class GameWorld
    {
        /// <summary>
        /// 运行状态标识
        /// </summary>
        private static bool _isRunning = false;

        public static void Run()
        {
            TutorialSettings settings = TutorialSettings.Instance;
            if (TutorialSampleType.Unknown == settings.TutorialSampleType)
            {
                Debugger.Error("The current running tutorial sample type '{%v}' was invalid value, please selected correct sample type from 'TutorialSettings' assets file.", settings.TutorialSampleType);
                return;
            }

            string tutorialType = settings.TutorialSampleType.ToString();
            Debugger.Log("当前正在运行教程：{%s}", tutorialType);

            SampleFilteringProcessor.AddSampleFilter(tutorialType);

            // 自动注册全部热加载模块
            GameEngine.GameApi.RegisterAllHotModulesOfContextConfigure();

            // 加载应用上下文
            LoadApplicationContexts();

            RegAssemblyNames(GlobalMacros.AssemblyName);

            // 加载所有程序集
            LoadAllAssemblies();

            // 启动应用通知回调接口
            GameEngine.GameLibrary.OnApplicationStartup(OnApplicationResponseCallback);

            CallSampleGate(GameEngine.GameMacros.GAME_REMOTE_PROCESS_CALL_RUN_SERVICE_NAME);

            _isRunning = true;
        }

        public static void Stop()
        {
            if (!_isRunning)
            {
                Debugger.Error("The sample module was not running, stopped it failed.");
                return;
            }

            CallSampleGate(GameEngine.GameMacros.GAME_REMOTE_PROCESS_CALL_STOP_SERVICE_NAME);

            // 关闭应用通知回调接口
            GameEngine.GameLibrary.OnApplicationShutdown(OnApplicationResponseCallback);

            // 自动注销所有热加载模块
            GameEngine.GameApi.UnregisterAllHotModulesOfContextConfigure();

            SampleFilteringProcessor.RemoveSampleFilter();

            _isRunning = false;
        }

        /// <summary>
        /// 世界容器的重构运行函数
        /// </summary>
        public static void Rebuild()
        {
        }

        /// <summary>
        /// 世界容器的重载运行函数
        /// </summary>
        /// <param name="commandType">类型标识</param>
        public static void Reload(GameEngine.EngineCommandType commandType)
        {
            if (!_isRunning)
            {
                Debugger.Error("The sample module was not running, reloaded it failed.");
                return;
            }

            switch (commandType)
            {
                case GameEngine.EngineCommandType.Hotfix:
                    // 重载业务对象类
                    LoadAllAssemblies(true);
                    break;
                default:
                    Debugger.Throw<System.InvalidOperationException>($"Invalid reload type {commandType}.");
                    break;
            }
        }

        /// <summary>
        /// 调用游戏案例下的指定函数
        /// </summary>
        /// <param name="methodName">函数名称</param>
        private static void CallSampleGate(string methodName)
        {
            string targetName = SampleFilteringProcessor.GetFilterModuleName() + ".SampleGate";

            System.Type type = NovaEngine.Utility.Assembly.GetType(targetName);
            if (type == null)
            {
                Debugger.Error("Could not found '{%s}' class type with current assemblies list, call that function '{%s}' failed.", targetName, methodName);
                return;
            }

            Debugger.Info("Call remote service {%s} with target function name {%s}.", targetName, methodName);

            NovaEngine.Utility.Reflection.CallMethod(type, methodName);
        }

        /// <summary>
        /// 应用层相应通知回调函数
        /// </summary>
        /// <param name="protocolType">通知协议类型</param>
        private static void OnApplicationResponseCallback(NovaEngine.Application.ProtocolType protocolType)
        {
            switch (protocolType)
            {
                case NovaEngine.Application.ProtocolType.Startup:
                    Startup();
                    break;
                case NovaEngine.Application.ProtocolType.Shutdown:
                    Shutdown();
                    break;
                case NovaEngine.Application.ProtocolType.FixedExecute:
                    FixedExecute();
                    break;
                case NovaEngine.Application.ProtocolType.Execute:
                    Execute();
                    break;
                case NovaEngine.Application.ProtocolType.LateExecute:
                    LateExecute();
                    break;
                case NovaEngine.Application.ProtocolType.FixedUpdate:
                    FixedUpdate();
                    break;
                case NovaEngine.Application.ProtocolType.Update:
                    Update();
                    break;
                case NovaEngine.Application.ProtocolType.LateUpdate:
                    LateUpdate();
                    break;
                default:
                    break;
            }
        }
    }
}
