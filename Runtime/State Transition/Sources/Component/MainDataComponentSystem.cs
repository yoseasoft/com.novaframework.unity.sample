
using System.Collections.Generic;


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

            AttackComponent attackComponent = player.GetComponent<AttackComponent>();
            attackComponent.attack_count = 0;
            attackComponent.attack_interval = 1f;

            self.player = player;
        }

        public static void CreateMonster(this MainDataComponent self, int count)
        {
            if (null != self.monsters)
            {
                Debugger.Warn("当前已存在怪物对象实例，请先移除旧数据后再创建新的怪物数据！");
                return;
            }

            self.monsters = new List<Monster>();
            for (int n = 0; n < count; ++n)
            {
                int uid = UniqueGenerator.NextId();

                Monster monster = GameEngine.GameApi.CreateActor<Monster>();
                AttributeComponent attributeComponent = monster.GetComponent<AttributeComponent>();
                attributeComponent.name = $"小鬼_{uid}";

                MoveComponent moveComponent = monster.GetComponent<MoveComponent>();
                moveComponent.move_length = 0f;
                moveComponent.move_speed = 0.01f;
                moveComponent.move_duration = 0.5f;

                ShoutComponent shoutComponent = monster.GetComponent<ShoutComponent>();
                shoutComponent.shout_contents = GetRandomShoutList();

                self.monsters.Add(monster);
            }
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

        public static void RemoveAllMonsters(this MainDataComponent self)
        {
            if (null == self.monsters)
            {
                Debugger.Warn("当前不存在任何有效的怪物对象实例，移除怪物数据失败！");
                return;
            }

            for (int n = 0; n < self.monsters.Count; ++n)
            {
                Monster monster = self.monsters[n];
                GameEngine.GameApi.DestroyActor(monster);
            }

            self.monsters.Clear();
            self.monsters = null;
        }

        public static Soldier GetRandomSoldier(this MainDataComponent self)
        {
            if (null == self.player || null == self.monsters)
            {
                return null;
            }

            int r = NovaEngine.Utility.Random.GetRandom(2);
            if (r > 0)
            {
                return self.GetRandomMonster();
            }
            else
            {
                return self.player;
            }
        }

        public static Monster GetRandomMonster(this MainDataComponent self)
        {
            if (null == self.monsters)
            {
                return null;
            }

            int r = NovaEngine.Utility.Random.GetRandom(self.monsters.Count);
            return self.monsters[r];
        }

        const int ShoutContentSize = 5;
        static IList<string>[] ShoutContents = new IList<string>[ShoutContentSize]
            {
                new List<string>()
                {
                    @"嗷嗷嗷，疼死我啦！！！",
                    @"好疼啊，好疼啊，受不鸟啦！",
                    @"呜呜呜，人家疼死啦……",
                },
                new List<string>()
                {
                    @"啊啊啊，好痛，你这个混蛋！",
                    @"痛，痛，痛，我要杀了你！",
                    @"啊，混蛋，你弄疼我了！",
                },
                new List<string>()
                {
                    @"呜，呜，呜……",
                    @"啊……流血啦……",
                    @"呀呀呀，不要啊……",
                },
                new List<string>()
                {
                    @"就这？",
                    @"还敢动手，小子，吃我一刀。",
                    @"呵呵，现在轮到我出手了。",
                },
                new List<string>()
                {
                    @"啊啊啊，好吓人，快跑呀！",
                    @"好疼啊，妈妈，你在哪里呀！",
                    @"你等着，我找老大去。",
                },
            };

        static IList<string> GetRandomShoutList()
        {

            int r = NovaEngine.Utility.Random.GetRandom(ShoutContentSize);
            return ShoutContents[r];
        }
    }
}
