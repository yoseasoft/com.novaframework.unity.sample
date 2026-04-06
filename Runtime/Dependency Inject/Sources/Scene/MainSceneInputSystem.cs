/// -------------------------------------------------------------------------------
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
using System.IO;
using System.Text;

namespace GameFramework.Sample.DependencyInject
{
    /// <summary>
    /// 主场景输入逻辑类
    /// </summary>
    static class MainSceneInputSystem
    {
        [OnInput(GameEngine.VirtualKeyCode.Alpha1, GameEngine.InputOperationType.Released)]
        static void OnConfigureFileLoadNotify(this MainScene self, GameEngine.VirtualKeyCode keycode, GameEngine.InputOperationType operationType)
        {
            /*
            GameEngine.ApplicationContext.LoadBeanConfigure((path, ms) =>
            {
                if (string.IsNullOrEmpty(path))
                    path = @"bean";

                using (FileStream fs = new FileStream(
                        $"{NovaEngine.Utility.Resource.ApplicationDataPath}/Resources/bean/{path}.xml", FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();

                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);

                    bytes = null;
                }

                return true;
            });
            */

            // 卸载之前的Bean配置
            GameEngine.ApplicationContext.Configure.UnloadAllBeanConfigureInfos();

            GameEngine.ApplicationContext.Configure.LoadBeanConfigure(@"main", (path, ms) =>
            {
                string text = BeanConfig.GetConfigByName(path);
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                ms.Write(buffer, 0, buffer.Length);
                ms.Seek(0, SeekOrigin.Begin);

                return true;
            });

            // 重新绑定Bean配置
            GameEngine.ApplicationContext.Configure.RebindingBeanConfigureOfSymbols();
        }

        [OnInput(GameEngine.VirtualKeyCode.Alpha2, GameEngine.InputOperationType.Released)]
        static void OnBeanObjectGenerateNotify(this MainScene self, GameEngine.VirtualKeyCode keycode, GameEngine.InputOperationType operationType)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            if (null != mainDataComponent.targetObject)
            {
                Debugger.Info($"成功销毁名为{mainDataComponent.targetObject.BeanName}的Bean对象实例！");
                GameEngine.ApplicationContext.ReleaseBean(mainDataComponent.targetObject);
                mainDataComponent.targetObject = null;
                return;
            }

            IList<string> beanNames = new List<string>()
            {
                @"local_player",
                @"sword_monster",
                @"bow_monster",
            };

            int r = NovaEngine.Utility.Random.Next(beanNames.Count);
            string beanName = beanNames[r];

            mainDataComponent.targetObject = GameEngine.ApplicationContext.CreateBean(beanName) as GActorWrapper;
            Debugger.Info($"成功创建名为{beanName}的Bean对象实例！");
        }

        [OnInput(GameEngine.VirtualKeyCode.Alpha3, GameEngine.InputOperationType.Released)]
        static void OnBeanObjectPrintNotify(this MainScene self, GameEngine.VirtualKeyCode keycode, GameEngine.InputOperationType operationType)
        {
            MainDataComponent mainDataComponent = self.GetComponent<MainDataComponent>();
            if (null == mainDataComponent.targetObject)
            {
                Debugger.Info($"当前没有任何有效的Bean对象实例，您需要先创建一个合法实例后再执行该操作！");
                return;
            }

            GActorWrapper actor = mainDataComponent.targetObject;

            StringBuilder sb = new StringBuilder();
            sb.Append($"类型={NovaEngine.Utility.Text.GetFullName(actor.GetType())}，");
            sb.Append($"名称={actor.BeanName}，");

            IReadOnlyList<GameEngine.CComponent> components = actor.GetAllComponents();
            sb.Append("组件列表={");
            for (int n = 0; null != components && n < components.Count; ++n)
            {
                GameEngine.CComponent component = components[n];
                if (n > 0) sb.Append('，');
                sb.Append($"{n}={NovaEngine.Utility.Text.GetFullName(component.GetType())}");
            }
            sb.Append("}");

            Debugger.Info($"当前Bean对象信息：\n{sb.ToString()}");
        }
    }
}
