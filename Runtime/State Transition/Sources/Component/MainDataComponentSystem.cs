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

namespace GameSample.StateTransition
{
    /// <summary>
    /// 主场景数据组件逻辑类
    /// </summary>
    static class MainDataComponentSystem
    {

        public static void CreatePlayer(this MainDataComponent self)
        {
            if (null != self.player)
            {
                Debugger.Warn("当前已存在玩家对象实例，请先移除旧数据后再创建新的玩家数据！");
                return;
            }

            Player player = GameEngine.GameApi.CreateActor<Player>();
            AttributeComponent attributeComponent = player.GetComponent<AttributeComponent>();
            attributeComponent.name = @"忧郁的胖头鱼";

            MoveComponent moveComponent = player.GetComponent<MoveComponent>();
            moveComponent.move_length = 0f;
            moveComponent.move_speed = 0.03f;
            moveComponent.move_duration = 1f;

            self.player = player;
        }

        public static void RemovePlayer(this MainDataComponent self)
        {
            if (null == self.player)
            {
                Debugger.Warn("当前不存在任何有效的玩家对象实例，移除玩家数据失败！");
                return;
            }

            GameEngine.GameApi.DestroyActor(self.player);
            self.player = null;
        }
    }
}
