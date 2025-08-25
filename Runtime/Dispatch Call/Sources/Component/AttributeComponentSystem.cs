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

namespace GameSample.DispatchCall
{
    /// <summary>
    /// 属性组件逻辑类
    /// </summary>
    static class AttributeComponentSystem
    {
        [GameEngine.EventSubscribeBindingOfTarget(EventNotify.DisplayAttribute)]
        static void OnDisplayInfo(this AttributeComponent self, int eventID, params object[] args)
        {
            Debugger.Info("基于带参扩展成员函数‘OnDisplayInfo’调用, 打印信息：{%s}", self.ToString());
        }

        [GameEngine.EventSubscribeBindingOfTarget(EventNotify.DisplayAttribute)]
        static void OnDisplayInfoWithNullParameter(this AttributeComponent self)
        {
            Debugger.Info("基于无参扩展成员函数‘OnDisplayInfoWithNullParameter’调用, 打印信息：{%s}", self.ToString());
        }
    }
}
