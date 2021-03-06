﻿using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Snow.OCR.Aliyun
{
    /// <summary>
    /// 阿里OCR身份证识别Response
    /// </summary>
    internal class AliyunOCRResponse
    {
        /// <summary>
        ///
        /// </summary>
        public List<Output> Outputs { get; set; }

        /// <summary>
        ///
        /// </summary>
        public class Output
        {
            /// <summary>
            ///
            /// </summary>
            public string OutputLabel { get; set; }

            /// <summary>
            ///
            /// </summary>
            public OutputValue OutputValue { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        public class OutputValue
        {
            /// <summary>
            ///
            /// </summary>
            public int DataType { get; set; }

            /// <summary>
            ///
            /// </summary>
            public DataValue DataValue { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        public class DataValue
        {
            /// <summary>
            ///
            /// </summary>
            public Config_str Config_str { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Nationality { get; set; }

            /// <summary>
            /// num
            /// </summary>
            public string Num { get; set; }

            /// <summary>
            ///
            /// </summary>

            public string Sex { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Birth { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Start_date { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string End_date { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Issue { get; set; }

            /// <summary>
            ///
            /// </summary>
            public bool Success { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        public class Config_str
        {
            /// <summary>
            ///
            /// </summary>
            public string Side { get; set; }
        }
    }
}