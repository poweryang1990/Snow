using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Snow.Extensions;

namespace Snow.Cache
{
    public partial class CacheKey
    {
        /// <summary>
        /// CacheKey验证器。
        /// </summary>
        public static class Validator
        {
            /// <summary>
            /// 验证CacheKey是否符合规范。
            /// </summary>
            /// <returns></returns>
            public static bool Verify(List<Type> cacheKeyTypes)
            {
                var cacheKeyList = cacheKeyTypes
                    .Select(type => (CacheKey)Activator.CreateInstance(type, true))
                    .ToList();

                //类型必须直接继承CacheKey
                ClassMustBeDirectInheritCacheKey(cacheKeyTypes);
                //类型必须是封闭类型
                ClassMustBeSealed(cacheKeyTypes);
                //类型的构造器必须是私有的
                ConstructorsMustBePrivate(cacheKeyTypes);

                //没有重复的Scope
                NoDuplicatedScopes(cacheKeyList);
                //没有重复的名字
                NoDuplicatedNames(cacheKeyList);

                //公开的属性必须是CacheKey类型的
                PublicPropertiesTypeMustBeCacheKey(cacheKeyTypes);

                return true;
            }

            /// <summary>
            /// 类型必须是直接继承自CacheKey类的子类，避免多层继承导致Scope，Params，Names被多个子类共用带来的混乱。
            /// </summary>
            /// <param name="types"></param>
            public static void ClassMustBeDirectInheritCacheKey(IEnumerable<Type> types)
            {
                var notDirectInheritCacheKeyTypes = types
                    .Where(type => type.BaseType != typeof(CacheKey))
                    .ToList();

                if (notDirectInheritCacheKeyTypes.Count != 0)
                {
                    throw new Exception(
                        $"这些[{GetTypeNames(notDirectInheritCacheKeyTypes)}]类型没有直接继承自{typeof(CacheKey).FullName}类。");
                }
            }

            /// <summary>
            /// 类型必须是封闭类，避免多层继承导致Scope，Params，Name被多个子类共用带来的混乱。
            /// </summary>
            /// <param name="types"></param>
            public static void ClassMustBeSealed(IEnumerable<Type> types)
            {
                var notSealedTypes = types
                    .Where(type => type.IsSealed == false)
                    .ToList();

                if (notSealedTypes.Count != 0)
                {
                    throw new Exception($"这些[{GetTypeNames(notSealedTypes)}]类型不是封闭的类型。");
                }
            }

            /// <summary>
            /// 构造函数必须是私有的，避免外部直接通过new来构造，只能通过静态的工厂方法来构造。
            /// </summary>
            /// <param name="types"></param>
            public static void ConstructorsMustBePrivate(IEnumerable<Type> types)
            {
                var publicConstructors = types
                    .SelectMany(type => type.GetConstructors())
                    .Where(c => c.IsPrivate == false)
                    .ToList();

                if (publicConstructors.Count != 0)
                {
                    var publicConstructorTypes = publicConstructors.Select(constructor => constructor.DeclaringType);
                    throw new Exception($"这些类型[{GetTypeNames(publicConstructorTypes)}]的构造器不是private的。");
                }
            }

            /// <summary>
            /// 检查是否有同名的Scope
            /// </summary>
            /// <param name="cacheKeyList"></param>
            public static void NoDuplicatedScopes(IEnumerable<CacheKey> cacheKeyList)
            {
                var duplicatedScopes = cacheKeyList
                    .GroupBy(cacheKey => cacheKey._scope)
                    .Where(scopeGroup => scopeGroup.Count() > 1)
                    .ToList();

                if (duplicatedScopes.Count != 0)
                {
                    var errorMessage = new StringBuilder();
                    foreach (var duplicatedScope in duplicatedScopes)
                    {
                        var duplicatedScopeTypes = duplicatedScope.Select(ds => ds.GetType());
                        errorMessage.AppendLine(
                            $"在类型[{GetTypeNames(duplicatedScopeTypes)}]上出现了相同的Scope[{duplicatedScope.Key}].");
                    }
                    throw new Exception(errorMessage.ToString());
                }
            }

            /// <summary>
            /// 在同一Scope中检查是否有重复的Names
            /// </summary>
            /// <param name="cacheKeyList"></param>
            public static void NoDuplicatedNames(IEnumerable<CacheKey> cacheKeyList)
            {
                foreach (var cacheKey in cacheKeyList)
                {
                    NoDuplicatedNames(cacheKey);
                }
            }

            /// <summary>
            /// 在同一Scope中检查是否有重复的Names
            /// </summary>
            /// <param name="cacheKey"></param>
            private static void NoDuplicatedNames(CacheKey cacheKey)
            {
                var cacheKeyType = cacheKey.GetType();

                var propertyList = cacheKeyType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(type => type.PropertyType == typeof(CacheKey))
                    .Select(type => (CacheKey)type.GetValue(cacheKey))
                    .ToList();

                var duplicatedNames = propertyList
                    .GroupBy(property => property._name)
                    .Where(keyGroup => keyGroup.Count() > 1)
                    .Select(keyGroup => keyGroup.Key)
                    .ToList();

                if (duplicatedNames.Count != 0)
                {
                    throw new Exception($"在类型[{cacheKeyType.FullName}]上出现了相同的Name[{duplicatedNames.JoinString(",")}].");
                }
            }

            
            /// <summary>
            /// 要求属性为CacheKey类型，不能是子类，避免多次链式调用引起歧义。
            /// </summary>
            /// <param name="types"></param>
            public static void PublicPropertiesTypeMustBeCacheKey(IEnumerable<Type> types)
            {
                var errorProperties = types
                    .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                    .Where(property => property.PropertyType != typeof(CacheKey))
                    .ToList();

                if (errorProperties.Count != 0)
                {
                    var errorMessage = new StringBuilder();
                    foreach (var errorPropertie in errorProperties)
                    {
                        errorMessage.AppendLine(
                            $"[{errorPropertie.ReflectedType.FullName}]类型上出现了非CachaKey类型的Public属性[{errorPropertie.Name}].");
                    }
                    throw new Exception(errorMessage.ToString());
                }
            }

            private static string GetTypeNames(IEnumerable<Type> types)
            {
                return types
                    .Select(type => type.FullName)
                    .JoinString(",");
            }
        }
    }
}