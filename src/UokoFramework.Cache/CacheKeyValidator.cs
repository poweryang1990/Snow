using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UOKOFramework.Extensions;

namespace UOKOFramework.Cache
{
    /// <summary>
    /// CacheKey验证器
    /// </summary>
    public class CacheKeyValidator
    {

        /// <summary>
        /// 验证CacheKey是否符合规范。
        /// </summary>
        /// <returns></returns>
        public bool Validate(List<Type> cacheKeyTypes)
        {
            var cacheKeyList = cacheKeyTypes
                .Select(type => (CacheKey)Activator.CreateInstance(type, true))
                .ToList();

            IsDirectInheritCacheKeyClass(cacheKeyTypes);
            IsSealedClass(cacheKeyTypes);
            PublicMethodsReturnTypeMustSelfClassType(cacheKeyTypes);
            PublicPropertiesTypeMustCacheKeyType(cacheKeyTypes);
            NoPublicConstructors(cacheKeyTypes);
            NoPublicStaticMembers(cacheKeyTypes);
            NoDuplicatedScopes(cacheKeyList);
            NoDuplicatedNames(cacheKeyList);

            return true;
        }

        /// <summary>
        /// 检查是否有同名的Scope
        /// </summary>
        /// <param name="cacheKeyList"></param>
        private void NoDuplicatedScopes(IEnumerable<CacheKey> cacheKeyList)
        {
            var duplicatedScopes = cacheKeyList
                           .GroupBy(scope => scope.GetFiledValue<string>("_scope"))
                           .Where(scopeGroup => scopeGroup.Count() > 1)
                           .ToList();

            if (duplicatedScopes.Count != 0)
            {
                var errorMessage = new StringBuilder();
                foreach (var duplicatedScope in duplicatedScopes)
                {
                    var duplicatedScopeTypes = duplicatedScope.Select(ds => ds.GetType());
                    errorMessage.AppendLine($"在类型[{GetTypeNames(duplicatedScopeTypes)}]上出现了相同的Scope[{duplicatedScope.Key}].");
                }
                throw new Exception(errorMessage.ToString());
            }
        }

        /// <summary>
        /// 在同一Scope中检查是否有重复的Names
        /// </summary>
        /// <param name="cacheKeyList"></param>
        private void NoDuplicatedNames(IEnumerable<CacheKey> cacheKeyList)
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
        private void NoDuplicatedNames(CacheKey cacheKey)
        {
            var cacheKeyType = cacheKey.GetType();

            var propertyList = cacheKeyType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(type => type == typeof(CacheKey))
                .Select(type => (CacheKey)type.GetValue(cacheKey))
                .ToList();

            var duplicatedNames = propertyList
                           .GroupBy(scope => scope.GetFiledValue<string>("_name"))
                           .Where(keyGroup => keyGroup.Count() > 1)
                           .Select(keyGroup => keyGroup.Key)
                           .ToList();

            if (duplicatedNames.Count != 0)
            {
                throw new Exception($"在类型[{cacheKeyType.FullName}]上出现了相同的Name[{duplicatedNames.JoinString(",")}].");
            }
        }

        /// <summary>
        /// 要求没有Pubilc、static的成员
        /// </summary>
        /// <param name="types"></param>
        private void NoPublicStaticMembers(IEnumerable<Type> types)
        {
            var publicStaticMembers = types
                .SelectMany(type => type.GetMembers(BindingFlags.Public | BindingFlags.Static))
                .ToList();

            if (publicStaticMembers.Count != 0)
            {
                var publicStaticMembersTypes = publicStaticMembers.Select(memeber => memeber.DeclaringType);
                throw new Exception($"这些类型[{GetTypeNames(publicStaticMembersTypes)}]不允许有pubilc、static的成员。");
            }
        }

        /// <summary>
        /// 要求方法的返回类型是自身类型，避免写错。
        /// </summary>
        /// <param name="types"></param>
        private void PublicMethodsReturnTypeMustSelfClassType(IEnumerable<Type> types)
        {
            var errorMethods = types
                .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                .Where(method => method.ReturnType != method.DeclaringType
                                    && method.Name.StartsWith("get") == false
                                    && method.Name.StartsWith("set") == false)
                .ToList();

            if (errorMethods.Count != 0)
            {
                var errorMessage = new StringBuilder();
                foreach (var errorMethod in errorMethods)
                {
                    errorMessage.AppendLine($"类型[{errorMethod.DeclaringType?.FullName}]的[{errorMethod}]方法的返回类型应该是[{errorMethod.DeclaringType?.Name}].");
                }
                throw new Exception(errorMessage.ToString());
            }
        }

        /// <summary>
        /// 要求属性为CacheKey类型，不能是子类，避免多次链式调用引起歧义。
        /// </summary>
        /// <param name="types"></param>
        private void PublicPropertiesTypeMustCacheKeyType(IEnumerable<Type> types)
        {
            var errorProperties = types
                .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                .Where(property => property.PropertyType != typeof(CacheKey))
                .ToList();

            if (errorProperties.Count != 0)
            {
                var errorMessage = new StringBuilder();
                foreach (var errorPropertie in errorProperties)
                {
                    errorMessage.AppendLine($"在类型[{errorPropertie.PropertyType.FullName}]上出现了非CachaKey类型的Public属性[{errorPropertie.Name}].");
                }
                throw new Exception(errorMessage.ToString());
            }
        }

        /// <summary>
        /// 要求没有Pubilc的构造函数，统一外部使用的入口为CacheKeys静态类，所以避免外部直接通过new来构造。
        /// </summary>
        /// <param name="types"></param>
        private void NoPublicConstructors(IEnumerable<Type> types)
        {
            var publicConstructors = types
                .SelectMany(type => type.GetConstructors())
                .Where(c => c.IsPublic)
                .ToList();

            if (publicConstructors.Count != 0)
            {
                var publicConstructorTypes = publicConstructors.Select(constructor => constructor.DeclaringType);
                throw new Exception($"这些类型[{GetTypeNames(publicConstructorTypes)}]不允许有Public的构造器。");
            }
        }

        /// <summary>
        /// 要求是本身是封闭类，避免多层继承导致Scope，Params，Names被多个子类共用带来的混乱。
        /// </summary>
        /// <param name="types"></param>
        private void IsSealedClass(IEnumerable<Type> types)
        {
            var notSealedTypes = types.Where(type => type.IsSealed == false).ToList();
            if (notSealedTypes.Count != 0)
            {
                throw new Exception($"这些类型[{GetTypeNames(notSealedTypes)}]不是封闭的类型。");
            }
        }

        /// <summary>
        /// 要求是直接继承自CacheKey类的子类，避免多层继承导致Scope，Params，Names被多个子类共用带来的混乱。
        /// </summary>
        /// <param name="types"></param>
        private void IsDirectInheritCacheKeyClass(IEnumerable<Type> types)
        {
            var notDirectInheritCacheKeyTypes = types
                .Where(type => type.BaseType != typeof(CacheKey))
                .ToList();

            if (notDirectInheritCacheKeyTypes.Count != 0)
            {
                throw new Exception($"这些类型[{GetTypeNames(notDirectInheritCacheKeyTypes)}]没有直接继承自{typeof(CacheKey).FullName}类。");
            }
        }

        private static string GetTypeNames(IEnumerable<Type> types)
        {
            return types.Select(type => type.FullName).JoinString(",");
        }
    }
}