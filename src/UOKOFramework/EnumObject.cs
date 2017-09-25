using System;
using System.Linq;
using System.Reflection;
using UOKOFramework.Extensions;

namespace UOKOFramework
{
    /// <summary>
    /// 枚举对象
    /// </summary>
    public sealed class EnumObject
    {
        private Type _type;
        private FieldInfo _field;

        private string _name;
        private bool _nameLoaded = false;

        private string _description;
        private bool _descriptionIsLoaded = false;

        private bool? _isFlag;
        private Enum[] _flagEnums;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public EnumObject(Enum value)
        {
            Value = value;
            Raw = Convert.ToInt32(value);
        }

        /// <summary>
        /// 获取枚举的类型信息
        /// </summary>
        public Type Type
        {
            get
            {
                if (this._type == null)
                {
                    this._type = Value.GetType();
                }
                return this._type;
            }
        }

        /// <summary>
        /// 获取枚举的名字
        /// </summary>
        public string Name
        {
            get
            {
                if (this._nameLoaded == true)
                {
                    return this._name;
                }

                if (this._name == null)
                {
                    this._name = Enum.GetName(this.Type, this.Value);
                    this._nameLoaded = true;
                }
                return this._name;
            }
        }

        /// <summary>
        /// 获取枚举字段
        /// </summary>
        public FieldInfo Filed
        {
            get
            {
                if (this.Name == null)
                {
                    return null;
                }

                if (this._field == null)
                {
                    this._field = this.Type.GetField(this.Name);
                }

                return this._field;
            }
        }

        /// <summary>
        /// 获取枚举的原始值
        /// </summary>
        public int Raw { get; }

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        public Enum Value { get; }

        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        public string Description
        {
            get
            {
                if (this.Filed == null)
                {
                    return null;
                }

                if (this._descriptionIsLoaded == true)
                {
                    return this._description;
                }

                if (this._description == null)
                {
                    this._description = this.Filed.GetDescription();
                    this._descriptionIsLoaded = true;
                }
                return this._description;
            }
        }

        /// <summary>
        /// 是否是Flag枚举
        /// </summary>
        public bool IsFlag
        {
            get
            {
                if (this._isFlag == null)
                {
                    var flagsAttribute = this.Type.GetCustomAttribute<FlagsAttribute>();
                    this._isFlag = (flagsAttribute != null);
                }
                return this._isFlag.Value;
            }
        }

        /// <summary>
        /// 包含的Flag枚举[包含自身]
        /// </summary>
        public Enum[] FlagEnums
        {
            get
            {
                if (this.IsFlag == false)
                {
                    return null;
                }
                if (this._flagEnums == null)
                {
                    this._flagEnums = Enum.GetValues(this.Type)
                        .Cast<Enum>()
                        .Where(item => Value.HasFlag(item))
                        .ToArray();
                }
                return this._flagEnums;
            }
        }

        /// <summary>
        /// String优先级Description > Name > Value.ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var description = this.Description;
            if (description != null)
            {
                return description;
            }
            var name = this.Name;
            if (name != null)
            {
                return name;
            }

            return this.Value.ToString();
        }

        /// <summary>
        /// Enum到EnumObject的隐式转换
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator EnumObject(Enum value)
        {
            return new EnumObject(value);
        }
    }
}
