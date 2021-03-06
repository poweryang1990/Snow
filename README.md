# 项目说明
当前解决方案（Snow.sln）为基础部分的公共库，和具体业务无关。

1. `Snow` 为最基础的部分，除了依赖.net的核心库之外，不存在任何的第三方依赖。
2. `Snow.xxx` 为通用部分的库，均依赖于Snow，彼此直接也会存在单项依赖。
3. `Snow.Web` 为web层的基础部分。

**注意代码中的作用域修饰符，和对外公开的接口行为无关的成员，均不得设置为`public`。**

**所有项目中的扩展方法所在的命名空间都统一在`Snow.Extensions`下，以方便使用。**

**由于是和业务无关的基础库，故而代码中不要去做和业务行为有任何关联的决策；也不要假设外部会如何使用这些公开的成员，应采用默认即正确的方式来避免可能产生的误用。**

# 添加新项目注意事项
项目文件夹和物理路径文件夹要对应上去，build脚本只会关心`src和test`文件夹下的项目，这样做区分也是为了项目结构更清晰。

新项目的编译平台请使用`<TargetFrameworks>net452;net461;netstandard2.0</TargetFrameworks>`，目前优先支持net452，如果不兼容`netstandard2.0`，则移除这个即可。

如果要添加到`Snow.Basic`元包中，请更新`\build\Snow.Basic.nuspec`文件，添加对应的依赖，以及更新版本号。

# 代码注释
虽说好的代码本身就是注释，但是供外部使用的公开的成员都应该添加注释描述，以方便使用者理解用意。
1. src文件夹下的项目均对`CS0109;CS1572;CS1573;CS1591;CS1712`这几种编译警告做了配置，被视为编译错误。

# 单元测试
很难保证后续不会提供具有破坏性的更新，避免无法及时发现新增或修改的代码不会对旧版本带来的问题。所以对外公开的成员尽量都使用单元测试覆盖到。
1. 一个类型对应一个测试文件夹，一个函数对应一个测试文件，便于统一管理测试文件。
2. 目前是采用xunit2测试框架来进行单元测试，moq负责mock，FluentAssertions辅助测试断言。

# nuget包说明
这些库均会以nuget包的形式提供出去，请发布`*.symbols.nupkg`版本的nuget包（symbols有pdb文件以及项目源码，可以方便使用的时候进行调试）。

nuget包除了`src`文件夹中的每个项目会有一个独立的包之外，还会有一下两个元包：
1. Snow.Basic：包含Snow.Web除外所有的包。
2. Snow.All：包含Sonw所有的包。

# build.ps1
此poweshell脚本只是引导脚本(切勿修改)，我们编写的脚本位于`\build\build.cake`中（可以修改）。
1. `.\build.ps1` ：编译并且运行单元测试。
2. `.\build.ps1 -target build`：编译项目。
3. `.\build.ps1 -target test` ：运行所有的单元测试。
4. `.\build.ps1 -target pack` ：打包nuget包到nupkgs文件夹中。