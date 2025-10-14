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
    /// 玩家根状态对象类
    /// </summary>
    internal class PlayerRoot : GameEngine.HFSM.State
    {
        public readonly Grounded grounded;
        public readonly Airborne airborne;

        readonly PlayerContext ctx;

        public PlayerRoot(GameEngine.HFSM.StateMachine machine, PlayerContext ctx) : base(machine, null)
        {
            this.ctx = ctx;
            grounded = new Grounded(machine, this, ctx);
            airborne = new Airborne(machine, this, ctx);
        }

        protected override GameEngine.HFSM.State GetInitialState() => grounded;
        protected override GameEngine.HFSM.State GetTransition() => null;
    }

    /// <summary>
    /// 地面行为状态类
    /// </summary>
    internal class Grounded : GameEngine.HFSM.State
    {
        public readonly Idle idle;
        public readonly Move move;

        readonly PlayerContext ctx;

        public Grounded(GameEngine.HFSM.StateMachine machine, GameEngine.HFSM.State parent, PlayerContext ctx) : base(machine, parent)
        {
            this.ctx = ctx;
            idle = new Idle(machine, this, ctx);
            move = new Move(machine, this, ctx);
            Add(new DelayActivationActivity { seconds = 0.5f });
            Add(new TalkAboutActivity { enterMessage = "我踩到地面了", exitMessage = "我离开地面了" });
        }

        protected override GameEngine.HFSM.State GetInitialState() => idle;

        protected override GameEngine.HFSM.State GetTransition()
        {
            if (ctx.jumpPressed)
            {
                ctx.jumpPressed = false;
                return ((PlayerRoot) Parent).airborne;
            }
            return ctx.grounded ? null : ((PlayerRoot) Parent).airborne;
        }

        protected override void OnEnter()
        {
            Debugger.Info("进入地面状态");
        }

        protected override void OnExit()
        {
            Debugger.Info("离开地面状态");
        }

        // protected override void OnUpdate(float deltaTime) { Debugger.Info("刷新地面状态"); }
    }

    /// <summary>
    /// 空中行为状态类
    /// </summary>
    internal class Airborne : GameEngine.HFSM.State
    {
        readonly PlayerContext ctx;

        public Airborne(GameEngine.HFSM.StateMachine machine, GameEngine.HFSM.State parent, PlayerContext ctx) : base(machine, parent)
        {
            this.ctx = ctx;
            Add(new TalkAboutActivity { enterMessage = "我飞起来了", exitMessage = "我掉下去了" });
        }

        protected override GameEngine.HFSM.State GetTransition() => ctx.grounded ? ((PlayerRoot) Parent).grounded : null;

        protected override void OnEnter()
        {
            Debugger.Info("进入空中状态");
        }

        protected override void OnExit()
        {
            Debugger.Info("离开空中状态");
        }

        protected override void OnUpdate() { Debugger.Info("刷新空中状态"); }
    }

    // <summary>
    /// 待机行为状态类
    /// </summary>
    internal class Idle : GameEngine.HFSM.State
    {
        readonly PlayerContext ctx;

        public Idle(GameEngine.HFSM.StateMachine machine, GameEngine.HFSM.State parent, PlayerContext ctx) : base(machine, parent)
        {
            this.ctx = ctx;
        }

        protected override GameEngine.HFSM.State GetTransition()
        {
            return UnityEngine.Mathf.Abs(ctx.move.x) > 0.01f ? ((Grounded) Parent).move : null;
        }

        protected override void OnEnter()
        {
            Debugger.Info("进入休闲状态");
        }

        protected override void OnExit()
        {
            Debugger.Info("离开休闲状态");
        }
    }

    /// <summary>
    /// 移动行为状态类
    /// </summary>
    internal class Move : GameEngine.HFSM.State
    {
        readonly PlayerContext ctx;

        public Move(GameEngine.HFSM.StateMachine machine, GameEngine.HFSM.State parent, PlayerContext ctx) : base(machine, parent)
        {
            this.ctx = ctx;
        }

        protected override GameEngine.HFSM.State GetTransition()
        {
            if (!ctx.grounded) return ((PlayerRoot) Parent).airborne;

            return UnityEngine.Mathf.Abs(ctx.move.x) <= 0.01f ? ((Grounded) Parent).idle : null;
        }

        protected override void OnEnter()
        {
            Debugger.Info("进入移动状态" + ctx.position.x);
        }

        protected override void OnExit()
        {
            Debugger.Info("离开移动状态" + ctx.position.x);
        }

        protected override void OnUpdate()
        {
            float target = ctx.move.x * 1f;
            ctx.position.x += target;

            Debugger.Info("刷新移动状态");
        }
    }
}
