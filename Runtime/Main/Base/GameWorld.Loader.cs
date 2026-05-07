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

namespace GameFramework.Sample
{
    /// 演示案例总控
    static partial class GameWorld
    {
        static IDictionary<string, string> _dataConfigures = new Dictionary<string, string>()
        {
            { @"application", ApplicationConfigureFile },
            { @"module", ModuleConfigureFile },
        };

        const string ApplicationConfigureFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                    <!-- 配置模块参数 -->
                    <module-config url=""module""/>

                    <!-- 配置bean文件导入 -->
                    <!-- bean-import url=""bean""/ -->
                </root>";

        const string ModuleConfigureFile = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <root>
                    <!-- 配置热加载模块 -->
                    <!-- hot-load name=""GameFramework.Protocol.Protobuf""/ -->
                    <!-- hot-load name=""GameFramework.View.Fairygui""/ -->
                </root>";

        private static IList<string> _waitingLoadAssemblyNames = null;

        /// <summary>
        /// 通过名称获取配置数据
        /// </summary>
        /// <param name="name">映射名称</param>
        /// <returns>返回配置数据</returns>
        static string GetApplicationConfigByName(string name)
        {
            if (_dataConfigures.TryGetValue(name, out string config))
            {
                return config;
            }

            return null;
        }

        private static void LoadApplicationContexts()
        {
            GameEngine.ApplicationContext.Configure.LoadApplicationConfigure(@"application", (path, ms) =>
            {
                string text = GetApplicationConfigByName(path);
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                ms.Write(buffer, 0, buffer.Length);
                ms.Seek(0, SeekOrigin.Begin);

                return true;
            });
        }

        /// <summary>
        /// 注册待加载的程序集名称
        /// </summary>
        /// <param name="assemblyNames">程序集名称</param>
        private static void RegAssemblyNames(params string[] assemblyNames)
        {
            if (null == _waitingLoadAssemblyNames)
            {
                _waitingLoadAssemblyNames = new List<string>();
            }

            _waitingLoadAssemblyNames.Clear();

            for (int n = 0; n < assemblyNames.Length; ++n)
            {
                _waitingLoadAssemblyNames.Add(assemblyNames[n]);
            }
        }

        /// <summary>
        /// 加载所有程序集
        /// </summary>
        private static void LoadAllAssemblies(bool reload = false)
        {
            /**
             * System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
             * foreach (System.Reflection.Assembly assembly in assemblies)
             * {
             *     GameEngine.CodeLoader.LoadFromAssembly(assembly);
             * }
             */

            for (int n = 0; null != _waitingLoadAssemblyNames && n < _waitingLoadAssemblyNames.Count; ++n)
            {
                // System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(WaitingLoadAssemblyNames[n]);
                System.Reflection.Assembly assembly = NovaEngine.Utility.Assembly.GetAssembly(_waitingLoadAssemblyNames[n]);
                if (null == assembly)
                {
                    Debugger.Error("通过指定名称‘{%s}’获取当前上下文中已加载的程序集实例失败！", _waitingLoadAssemblyNames[n]);
                    continue;
                }

                GameEngine.GameLibrary.LoadFromAssembly(assembly, reload);
            }
        }
    }
}
