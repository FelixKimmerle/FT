using System;
using System.Collections.Generic;

namespace FT.src.packets
{
    static public class Utils
    {
        static public byte[] GetByteArray(List<byte> bytes, int position, int length = 2, bool reversed = false)
        {
            byte[] sourceArray = bytes.ToArray();
            byte[] byteArray = new byte[length];
            Array.Copy(sourceArray, position, byteArray, 0, length);
            if (reversed)
            {
                Array.Reverse(byteArray);
            }
            return byteArray;
        }

        static public byte[] GetBytes(int value, bool reversed = false)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (reversed)
            {
                Array.Reverse(result);
            }
            return result;
        }

        static public byte[] GetBytes(uint value, bool reversed = false)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (reversed)
            {
                Array.Reverse(result);
            }
            return result;
        }

        static public byte[] GetBytes(short value, bool reversed = false)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (reversed)
            {
                Array.Reverse(result);
            }
            return result;
        }

        static public byte[] GetBytes(ushort value, bool reversed = false)
        {
            byte[] result = BitConverter.GetBytes(value);
            if (reversed)
            {
                Array.Reverse(result);
            }
            return result;
        }
    }
}