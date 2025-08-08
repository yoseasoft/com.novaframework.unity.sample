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

using SystemStringBuilder = System.Text.StringBuilder;

namespace GameEngine.Sample.DispatchCall
{
    /// <summary>
    /// 怪物对象逻辑类
    /// </summary>
    public static class MonsterSystem
    {
        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Awake)]
        static void Awake(this Monster self)
        {
        }

        [GameEngine.OnAspectBeforeCall(GameEngine.AspectBehaviourType.Start)]
        static void Start(this Monster self)
        {
        }

        [GameEngine.OnAspectAfterCall(GameEngine.AspectBehaviourType.Destroy)]
        static void Destroy(this Monster self)
        {
        }

        [GameEngine.EventSubscribeBindingOfTarget(EventNotify.PlayerSearchAllEnemies)]
        private static void OnEnemyDisplayInfo(this Monster self, int eventID, params object[] args)
        {
            Debugger.Info("怪物对象成功接收事件[{%d}]，信息输出：{%s}！", eventID, self.ToMonsterString());
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.T, GameEngine.InputOperationType.Released)]
        private static void OnTalkInputObserve(this Monster self, int keycode, int operationType)
        {
            string[] infos = new string[5];
            infos[0] = "被调戏";
            infos[1] = "打球";
            infos[2] = "玩游戏";
            infos[3] = "徒步旅行";
            infos[4] = "骑行";

            int index = NovaEngine.Utility.Random.GetRandom(infos.Length);
            GameEngine.NetworkHandler.Instance.OnSimulationReceiveMessageComposedOfProtoBuf(new ActorChatResp()
            {
                ChatList = new List<ChatInfo>()
                {
                    new ChatInfo() { Uid = self.GetComponent<IdentityComponent>().objectID, Text = $"我{self.GetComponent<IdentityComponent>().objectName}可喜欢{infos[index]}了" }
                },
            });
        }

        public static string ToMonsterString(this Monster self)
        {
            SystemStringBuilder sb = new SystemStringBuilder();

            sb.AppendFormat("[怪物对象]:{0},", self.ToSoldierString());

            SpawnComponent spawnComponent = self.GetComponent<SpawnComponent>();
            sb.AppendFormat("出生点={{{0},{1},{2}}},", spawnComponent.born_position.x, spawnComponent.born_position.y, spawnComponent.born_position.z);

            return sb.ToString();
        }
    }
}
