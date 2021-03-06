﻿using System;
using System.Reflection;
using Snow.Extensions;

// ReSharper disable InconsistentNaming

namespace Snow.IPDB._17ipdb
{
    /// <summary>
    /// 17ipdb帮助类
    /// </summary>
    internal class _17IPv4DBHelper
    {
        private static readonly byte[] _dataBuffer;
        private static readonly byte[] _indexBuffer;
        private static readonly uint[] _index = new uint[256];

        static _17IPv4DBHelper()
        {
            _dataBuffer = Assembly.GetExecutingAssembly()
                                  .GetResourceBytes(_ => _.EndsWith("17ipdb.dat"));

            var indexBufferLength = BytesToUint(_dataBuffer[0],
                                                _dataBuffer[1],
                                                _dataBuffer[2],
                                                _dataBuffer[3]);

            _indexBuffer = new byte[indexBufferLength];

            Array.Copy(_dataBuffer, 4, _indexBuffer, 0, _indexBuffer.LongLength);

            for (var loop = 0; loop < 256; loop++)
            {
                _index[loop] = BytesToUint(_indexBuffer[loop * 4 + 3],
                                           _indexBuffer[loop * 4 + 2],
                                           _indexBuffer[loop * 4 + 1],
                                           _indexBuffer[loop * 4]);
            }
        }

        internal static string[] Find(IPv4 ipv4)
        {
            var ip = BytesToUint(ipv4.Byte4, ipv4.Byte3, ipv4.Byte2, ipv4.Byte1);
            var start = _index[ipv4.Byte1];
            var maxLength = _indexBuffer.Length - 1028;
            long indexOffset = -1;
            var indexLength = -1;
            for (start = start * 8 + 1024; start < maxLength; start += 8)
            {
                var bytesToUint = BytesToUint(_indexBuffer[start + 0],
                                              _indexBuffer[start + 1],
                                              _indexBuffer[start + 2],
                                              _indexBuffer[start + 3]);

                if (bytesToUint >= ip)
                {
                    indexOffset = BytesToUint(0,
                                               _indexBuffer[start + 6],
                                               _indexBuffer[start + 5],
                                               _indexBuffer[start + 4]);

                    indexLength = 0xFF & _indexBuffer[start + 7];
                    break;
                }
            }

            var areaBytes = new byte[indexLength];

            Array.Copy(_dataBuffer,
                       _indexBuffer.Length + (int)indexOffset - 1024,
                       areaBytes,
                       0,
                       indexLength);

            return areaBytes.GetString().Split('\t');
        }

        private static uint BytesToUint(byte a, byte b, byte c, byte d)
        {
            return (uint)((a << 24) | (b << 16) | (c << 8) | d);
        }
    }
}
