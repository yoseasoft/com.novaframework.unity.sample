/// -------------------------------------------------------------------------------
/// NovaEngine Framework Samples
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

namespace GameEngine.Sample.DispatchCall
{
    /// <summary>
    /// 主场景输入逻辑类
    /// </summary>
    static class MainSceneInputSystem
    {
        [GameEngine.OnInputDispatchCall((int) UnityEngine.KeyCode.E, GameEngine.InputOperationType.Released)]
        static void OnEnterWorldMessageSend(int keycode, int operationType)
        {
            MainScene main = GameEngine.SceneHandler.Instance.GetCurrentScene() as MainScene;
            Debugger.Assert(null != main, "Invalid activated scene.");

            GameEngine.NetworkHandler.Instance.OnSimulationReceiveMessageComposedOfProtoBuf(new EnterWorldResp()
            {
                Code = 1,
                Player = MessageBuilder.CreatePlayerInfo(),
            });
        }

        [GameEngine.OnInputDispatchCall((int) UnityEngine.KeyCode.A, GameEngine.InputOperationType.Released)]
        static void OnLevelSpawnMessageSend(int keycode, int operationType)
        {
            List<MonsterInfo> monsters = new List<MonsterInfo>();
            int total = NovaEngine.Utility.Random.GetRandom(3) + 1;
            for (int n = 0; n < total; n++)
            {
                int r = NovaEngine.Utility.Random.GetRandom(2);
                MonsterInfo monster = null;
                if (r > 0)
                {
                    monster = MessageBuilder.CreateGoblinMonsterInfo();
                }
                else
                {
                    monster = MessageBuilder.CreateSlimeMonsterInfo();
                }

                monsters.Add(monster);
            }

            GameEngine.NetworkHandler.Instance.OnSimulationReceiveMessageComposedOfProtoBuf(new LevelSpawnResp()
            {
                Code = total,
                MonsterList = monsters,
            });
        }

        [GameEngine.OnInputDispatchCall((int) UnityEngine.KeyCode.Q, GameEngine.InputOperationType.Released)]
        static void OnLeaveWorldMessageSend(int keycode, int operationType)
        {
            GameEngine.NetworkHandler.Instance.OnSimulationReceiveMessageComposedOfProtoBuf(new LeaveWorldResp()
            {
                Code = 0,
            });
        }
    }
}
