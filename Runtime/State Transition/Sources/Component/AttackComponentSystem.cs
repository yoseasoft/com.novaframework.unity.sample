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
    /// 攻击组件逻辑类
    /// </summary>
    static class AttackComponentSystem
    {
        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Awake)]
        static void Awake(this AttackComponent self)
        {
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Start)]
        static void Start(this AttackComponent self)
        {
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Update)]
        static void Update(this AttackComponent self)
        {
            if (self.is_attacking)
            {
                if (self.last_attack_time + self.attack_interval < NovaEngine.Timestamp.RealtimeSinceStartup)
                {
                    self.is_attacking = false;
                    Debugger.Info("【{%s}】攻击结束，当前总攻击次数：{%d}。", self.GetComponent<AttributeComponent>().name, self.attack_count);
                }
            }
        }

        [GameEngine.OnAspectAfterCall(GameEngine.AspectBehaviourType.Destroy)]
        static void Destroy(this AttackComponent self)
        {
        }

        public static void AttackTo(this AttackComponent self, Soldier target)
        {
            if (self.is_attacking)
            {
                Debugger.Info("【{%s}】正在攻击中，在此次攻击行为结束前不可再次发起新的攻击指令！", self.GetComponent<AttributeComponent>().name);
                return;
            }

            self.is_attacking = true;
            self.attack_count += 1;
            self.last_attack_time = NovaEngine.Timestamp.RealtimeSinceStartup;

            Debugger.Info("【{%s}】开始攻击目标【{%s}】。", self.GetComponent<AttributeComponent>().name, target.GetComponent<AttributeComponent>().name);

            self.Send(new HitForTarget() { invader = self.GetComponent<AttributeComponent>().name });
        }
    }
}
