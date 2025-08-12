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

using System.Collections.Generic;

namespace GameEngine.Sample.DependencyInject
{
    /// <summary>
    /// Bean配置数据管理类
    /// </summary>
    internal static class BeanConfig
    {
        static IDictionary<string, string> _dataConfigures = new Dictionary<string, string>()
        {
            { "main", MainFile },
        };

        const string MainFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                <bean name=""game_actor"" class_type=""GameEngine.Sample.DependencyInject.Actor"" singleton=""false"" inherited=""true"">
                    <component reference_name=""identityComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""soldier"" class_type=""GameEngine.Sample.DependencyInject.Soldier"" singleton=""false"">
                    <component reference_name=""attributeComponent"" activation_on=""Initialize""/>
                    <component reference_type=""GameEngine.Sample.DependencyInject.TransformComponent"" activation_on=""Initialize""/>
                    <component reference_type=""GameEngine.Sample.DependencyInject.MoveComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""local_player"" class_type=""GameEngine.Sample.DependencyInject.Player"" singleton=""false"">
                    <field name=""buff"" reference_type=""GameEngine.Sample.DependencyInject.Buff""/>
                    <component reference_type=""GameEngine.Sample.DependencyInject.SkillComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""sword_monster"" class_type=""GameEngine.Sample.DependencyInject.Monster"" singleton=""false"">
                    <component reference_type=""GameEngine.Sample.DependencyInject.GoblinAiWithSwordComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""bow_monster"" class_type=""GameEngine.Sample.DependencyInject.Monster"" singleton=""false"">
                    <component reference_type=""GameEngine.Sample.DependencyInject.GoblinAiWithBowComponent"" activation_on=""Initialize""/>
                </bean>
                <bean name=""identityComponent"" class_type=""GameEngine.Sample.DependencyInject.IdentityComponent"" singleton=""false"">
                </bean>
                <bean name=""attributeComponent"" class_type=""GameEngine.Sample.DependencyInject.AttributeComponent"" singleton=""false"">
                </bean>
                <bean name=""attackComponent"" class_type=""GameEngine.Sample.DependencyInject.AttackComponent"" singleton=""false"">
                    <field name=""weapon"" reference_type=""GameEngine.Sample.DependencyInject.Weapon""/>
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
