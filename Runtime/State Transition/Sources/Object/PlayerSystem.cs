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
    /// 玩家对象逻辑类
    /// </summary>
    static class PlayerSystem
    {
        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Awake)]
        static void Awake(this Player self)
        {
            self.CreateStateBuilder()
                .Start("Attack").Transition("Idle");
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Start)]
        static void Start(this Player self)
        {
        }

        [GameEngine.OnAspectAfterCall(GameEngine.AspectBehaviourType.Destroy)]
        static void Destroy(this Player self)
        {
        }

        [GameEngine.StateTransitionBindingOfTarget("Attack", GameEngine.StateAccessType.Enter)]
        static void OnAttackBegan(this Player self)
        {
            Debugger.Info("【{%s}】开始攻击！", self.GetComponent<AttributeComponent>().name);

            MainDataComponent mainDataComponent = GameEngine.GameApi.GetCurrentScene().GetComponent<MainDataComponent>();
            Monster target = mainDataComponent.GetRandomMonster();

            AttackComponent attackComponent = self.GetComponent<AttackComponent>();
            attackComponent.AttackTo(target);
        }

        [GameEngine.StateTransitionBindingOfTarget("Attack", GameEngine.StateAccessType.Update)]
        static void OnAttackUpdate(this Player self)
        {
            AttackComponent attackComponent = self.GetComponent<AttackComponent>();
            if (attackComponent.is_attacking)
                return;

            self.StateFinished();
        }

        [GameEngine.StateTransitionBindingOfTarget("Attack", GameEngine.StateAccessType.Update)]
        static void OnAttackUpdateAndNotify(this Player self)
        {
            AttackComponent attackComponent = self.GetComponent<AttackComponent>();
            Debugger.Info("【{%s}】当前耗时：{%f}！", self.GetComponent<AttributeComponent>().name, NovaEngine.Timestamp.RealtimeSinceStartup - attackComponent.last_attack_time);
        }

        [GameEngine.StateTransitionBindingOfTarget("Attack", GameEngine.StateAccessType.Exit)]
        static void OnAttackEnded(this Player self)
        {
            Debugger.Info("【{%s}】攻击结束！", self.GetComponent<AttributeComponent>().name);
        }

        [GameEngine.StateTransitionBindingOfTarget("Idle", GameEngine.StateAccessType.Enter)]
        static void OnIdleBegan(this Player self)
        {
            Debugger.Info("【{%s}】进入休闲！", self.GetComponent<AttributeComponent>().name);
        }

        [GameEngine.StateTransitionBindingOfTarget("Idle", GameEngine.StateAccessType.Exit)]
        static void OnIdleEnded(this Player self)
        {
            Debugger.Info("【{%s}】休闲结束！", self.GetComponent<AttributeComponent>().name);
        }
    }
}
