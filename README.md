# 项目说明

当前解决方案（UOKOFramework.sln）为基础部分的公共库，和具体业务无关。

1. `UOKOFramework` 为最基础的部分，除了依赖.net的核心库之外，不存在任何的第三方依赖。
2. `UOKOFramework.xxx` 为通用部分的库，不存在互相依赖，但是都均依赖于UOKOFramework。
3. `UOKOFramework.Web` 为web层的基础部分。

# 添加新项目注意所属的文件夹

项目文件夹和物理路径文件夹要对应上去，build脚本只会关心`src和test`文件夹下的项目。这样做区分也是为了项目结构更清晰。

# 代码注释
虽说好的代码本身就是注释，但是供外部使用的成员都应该添加注释描述，以方便使用者理解用意。
> src文件夹下的项目均对`CS0109;CS1572;CS1573;CS1591;CS1712`这几种编译警告做了配置，被视为编译错误。

# 单元测试
很难保证后续不会提供具有破坏性的更新，避免无法及时发现新增或修改的代码不会对旧版本带来的问题。所以对外公开的成员尽量的都使用单元测试覆盖到。
1. 一个类型对应一个测试文件夹，一个函数对应一个测试文件，便于统一管理测试文件。
2. 目前是采用xunit2测试框架来进行单元测试。

# nuget包说明
由于这些库均会以nuget包的形式提供出去，所以请发布`*.symbols.nupkg`版本的nuget包（其中包含有pdb文件以及项目源码，可以方便使用的时候进行调试。

# build.ps1

1. `-Target build`：编译项目。
2. `-Target test` ：运行单元测试。
3. `-Target pack` ：打包nuget包到nupkgs文件夹中。