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

namespace GameSample.StateTransition
{
    /// <summary>
    /// 移动组件逻辑类
    /// </summary>
    static class MoveComponentSystem
    {
        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Awake)]
        static void Awake(this MoveComponent self)
        {
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Start)]
        static void Start(this MoveComponent self)
        {
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Update)]
        static void Update(this MoveComponent self)
        {
            if (self.is_moving)
            {
                self.move_length += self.move_speed;
                if (self.last_move_time + self.move_duration < NovaEngine.Timestamp.RealtimeSinceStartup)
                {
                    self.is_moving = false;
                    Debugger.Info("角色【{%s}】移动结束，当前总移动距离：{%f}。", self.GetComponent<AttributeComponent>().name, self.move_length);
                }
            }
        }

        [GameEngine.OnAspectAfterCall(GameEngine.AspectBehaviourType.Destroy)]
        static void Destroy(this MoveComponent self)
        {
        }

        public static void MoveTo(this MoveComponent self)
        {
            if (self.is_moving)
            {
                Debugger.Info("角色【{%s}】正在移动中，在此次移动行为结束前不可再次发起新的移动指令！", self.GetComponent<AttributeComponent>().name);
                return;
            }

            self.is_moving = true;
            self.last_move_time = NovaEngine.Timestamp.RealtimeSinceStartup;

            Debugger.Info("角色【{%s}】开始移动。", self.GetComponent<AttributeComponent>().name);
        }
    }
}
