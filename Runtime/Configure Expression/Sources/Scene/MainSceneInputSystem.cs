/// -------------------------------------------------------------------------------
/// Sample Module for GameEngine Framework
///
/// Copyright (C) 2024 - 2025, Hurley, Independent Studio.
/// Copyright (C) 2025, Hainan Yuanyou Information Tecdhnology Co., Ltd. Guangzhou Branch
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

using System.Collections.Generic;

namespace GameSample.ConfigureExpression
{
    /// <summary>
    /// 主场景输入逻辑类
    /// </summary>
    static class MainSceneInputSystem
    {
        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha1, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForNormalFunctionCall(this MainScene self)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();

            GameEngine.GameApi.CallFunction(null, "震屏", "上下模式", 3, 1.5f);

            GameEngine.GameApi.CallFunction(mainDataComponent.player, "钝帧", 0.2f, 0.5f);

            self.PrintUsage();
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha2, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForBeanFunctionCall(this MainScene self)
        {
            string text = @"震屏(上下模式,3,1.5)
                    钝帧(0.2,0.5)
                    闪烁()
                    冲刺(3,1.5)";

            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();

            GameEngine.GameApi.Exec(mainDataComponent.player, 1001, text);

            self.PrintUsage();
        }
    }
}
