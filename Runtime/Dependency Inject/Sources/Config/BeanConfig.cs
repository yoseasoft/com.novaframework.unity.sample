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

using System.Collections.Generic;

namespace GameSample.DependencyInject
{
    /// <summary>
    /// Bean配置数据管理类
    /// </summary>
    internal static class BeanConfig
    {
        static IDictionary<string, string> _dataConfigures = new Dictionary<string, string>()
        {
            { "main", MainFile },
            { "monster", MonsterFile },
            { "component", ComponentFile },
        };

        const string MainFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                <!--
                  基础游戏角色的实体定义，它是所有角色类实体的父类
                  -->
                <bean name=""game_actor"" class_type=""GameSample.DependencyInject.Actor"" singleton=""false"" inherited=""true"">
                    <component reference_name=""identityComponent"" activation_on=""Initialize""/>
                </bean>
                <!-- 士兵类型的实体定义 -->
                <bean name=""soldier"" class_type=""GameSample.DependencyInject.Soldier"" singleton=""false"">
                    <component reference_name=""attributeComponent"" activation_on=""Initialize""/>
                    <component reference_type=""GameSample.DependencyInject.TransformComponent"" activation_on=""Initialize""/>
                    <component reference_type=""GameSample.DependencyInject.MoveComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""local_player"" class_type=""GameSample.DependencyInject.Player"" singleton=""false"">
                    <field name=""buff"" reference_type=""GameSample.DependencyInject.Buff""/>
                    <component reference_type=""GameSample.DependencyInject.SkillComponent"" activation_on=""Initialize""/>
                </bean>
                <file include=""monster""/>
                <file include=""component""/>
                </root>";

        const string MonsterFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                <bean name=""sword_monster"" class_type=""GameSample.DependencyInject.Monster"" singleton=""false"">
                    <component reference_type=""GameSample.DependencyInject.GoblinAiWithSwordComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""bow_monster"" class_type=""GameSample.DependencyInject.Monster"" singleton=""false"">
                    <component reference_type=""GameSample.DependencyInject.GoblinAiWithBowComponent"" activation_on=""Initialize""/>
                </bean>
                </root>";

        const string ComponentFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                <bean name=""identityComponent"" class_type=""GameSample.DependencyInject.IdentityComponent"" singleton=""false"">
                </bean>
                <!-- 属性组件 -->
                <bean name=""attributeComponent"" class_type=""GameSample.DependencyInject.AttributeComponent"" singleton=""false"">
                </bean>
                <!-- 攻击组件 -->
                <bean name=""attackComponent"" class_type=""GameSample.DependencyInject.AttackComponent"" singleton=""false"">
                    <field name=""weapon"" reference_type=""GameSample.DependencyInject.Weapon""/>
                </bean>
                </root>";

        /// <summary>
        /// 通过名称获取配置数据
        /// </summary>
        /// <param name="name">映射名称</param>
        /// <returns>返回配置数据</returns>
        public static string GetConfigByName(string name)
        {
            if (_dataConfigures.TryGetValue(name, out string config))
            {
                return config;
            }

            return null;
        }
    }
}
