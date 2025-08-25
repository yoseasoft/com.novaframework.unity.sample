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

namespace GameSample.StateTransition
{
    /// <summary>
    /// 主场景输入逻辑类
    /// </summary>
    static class MainSceneInputSystem
    {
        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha1, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForCreateSoldiers(this MainScene self)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            mainDataComponent.CreatePlayer();
            mainDataComponent.CreateMonster(2);
            Debugger.Info("创建角色对象实例完成！");

            self.PrintUsage();
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha2, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForSoldierMoved(this MainScene self)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            Soldier soldier = mainDataComponent.GetRandomSoldier();
            if (null == soldier)
            {
                Debugger.Warn("当前对象数据不完整，请重新构建完整数据后执行该操作！");
                return;
            }
            
            Debugger.Info("【{%s}】开始移动……", soldier.GetComponent<AttributeComponent>().name);
            soldier.StateTransition("Move");

            // self.PrintUsage();
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha3, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForSoldierAttacked(this MainScene self)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            Player player = mainDataComponent.player;
            if (null == player)
            {
                Debugger.Warn("当前对象数据不完整，请重新构建完整数据后执行该操作！");
                return;
            }

            Debugger.Info("【{%s}】开始攻击……", player.GetComponent<AttributeComponent>().name);
            player.StateTransition("Attack");

            // self.PrintUsage();
        }

        [GameEngine.InputResponseBindingOfTarget((int) UnityEngine.KeyCode.Alpha4, GameEngine.InputOperationType.Released)]
        static void OnInputOperationForRemoveSoldiers(this MainScene self)
        {
            Debugger.Warn("当前对象数据不完整，请重新构建完整数据后执行该操作！");
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            mainDataComponent.RemovePlayer();
            mainDataComponent.RemoveAllMonsters();
            Debugger.Info("销毁角色对象实例完成！");

            self.PrintUsage();
        }
    }
}
