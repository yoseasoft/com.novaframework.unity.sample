/// -------------------------------------------------------------------------------
/// Sample Module for GameEngine Framework
///
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

using SystemType = System.Type;
using SystemDelegate = System.Delegate;
using SystemStringBuilder = System.Text.StringBuilder;
using SystemMethodInfo = System.Reflection.MethodInfo;
using SystemBindingFlags = System.Reflection.BindingFlags;

namespace GameSample.PerformanceAnalysis
{
    /// <summary>
    /// 主场景逻辑类
    /// </summary>
    static class MainSceneTestSystem
    {
        static int _count = 0;

        static void AccumulateInvokeCount(this MainScene self)
        {
            ++_count;
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha6, GameEngine.InputOperationType.Released)]
        static void OnSceneTestingInputed(this MainScene self, int keycode, int operationType)
        {
            SystemType targetType = typeof(MainSceneTestSystem);
            SystemMethodInfo methodInfo = targetType.GetMethod("AccumulateInvokeCount", SystemBindingFlags.Public | SystemBindingFlags.NonPublic | SystemBindingFlags.Static);

            SystemDelegate callback = NovaEngine.Utility.Reflection.CreateGenericActionDelegate(null, methodInfo);

            System.Action<object> action = NovaEngine.Utility.Reflection.CreateGenericAction<object>(methodInfo, typeof(MainScene));

            int c = 1000000;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            _count = 0;
            for (int n = 0; n < c; ++n)
            {
                callback.DynamicInvoke(self);
            }
            stopwatch.Stop();
            Debugger.Info("成功调用代理函数{%d}次，计算结果为{%d}，耗时{%d}毫秒！", c, _count, stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            _count = 0;
            for (int n = 0; n < c; ++n)
            {
                action.Invoke(self);
            }
            stopwatch.Stop();
            Debugger.Info("成功调用编译函数{%d}次，计算结果为{%d}，耗时{%d}毫秒！", c, _count, stopwatch.ElapsedMilliseconds);
        }
    }
}
