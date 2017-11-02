using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Snow.EF
{
    /// <summary>
    /// 动态模型加载工具
    /// </summary>
    public static class ModelBuilderHelper
    {
        /// <summary>
        /// 模型自动装配
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="modelMapAssemblyString"></param>
        public static void LoadModel(DbModelBuilder modelBuilder, string modelMapAssemblyString)
        {
            //通过反射创建出程序集下面的数据表映射实例(Map)
            var typesToRegister = Assembly.Load(modelMapAssemblyString).GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type =>type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
