using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftMapItemViewer
{
    public class MCMapItem
    {
        
        private byte _scale;
        private byte _dimension;
        private short _height;
        private short _width;
        private int _xCenter;
        private int _zCenter;
        private byte[] _colors;

        public MCMapItem(byte[] wholeArray)
        {
            _scale = wholeArray.GetByte("scale");
            _dimension = wholeArray.GetByte("dimension");
            _height = wholeArray.GetShort("height");
            _width = wholeArray.GetShort("width");
            _xCenter = wholeArray.GetInt("xCenter");
            _zCenter = wholeArray.GetInt("zCenter");
        }

        #region Properties
        public byte Scale
        {
            get
            {
                return _scale;
            }
        }

        public byte Dimension
        {
            get
            {
                return _dimension;
            }
        }

        public short Height
        {
            get
            {
                return _height;
            }
        }

        public short Width
        {
            get
            {
                return _width;
            }
        }

        public int xCenter
        {
            get
            {
                return _xCenter;
            }
        }

        public int zCenter
        {
            get
            {
                return _zCenter;
            }
        }

        public byte[] Colors
        {
            get
            {
                return _colors;
            }
        }

        #endregion


    }

    public static class NBTFormat
    {
        public static class Tags
        {
            public static byte NBT_End = 0x00;
            public static byte NBT_Byte = 0x01;
            public static byte NBT_Short = 0x02;
            public static byte NBT_Int = 0x03;
            public static byte NBT_Long = 0x04;
            public static byte NBT_Float = 0x05;
            public static byte NBT_Double = 0x06;
            public static byte NBT_Byte_Array = 0x07;
            public static byte NBT_String = 0x08;
            public static byte NBT_List = 0x09;
            public static byte NBT_Compound = 0x0A;
            public static byte NBT_Int_Array = 0x0B;
        }


        public static byte GetByte(this byte[] wholeArray, string strTagName)
        {
            byte[] searchArray = {Tags.NBT_Byte}; // One tag byte
            Extensions.Append(ref searchArray, new byte[] { // Two bytes for length of tag name
                (byte)(Encoding.UTF8.GetByteCount(strTagName) >> 8), 
                (byte)Encoding.UTF8.GetByteCount(strTagName) }); 
            Extensions.Append(ref searchArray, Encoding.UTF8.GetBytes(strTagName)); // Tag name
            int locationOfByte = wholeArray.IndexOf(searchArray) + searchArray.Length;
            return wholeArray[locationOfByte];
        }

        public static short GetShort(this byte[] wholeArray, string strTagName)
        {
            byte[] searchArray = { Tags.NBT_Short }; // One tag byte
            Extensions.Append(ref searchArray, new byte[] { // Two bytes for length of tag name
                (byte)(Encoding.UTF8.GetByteCount(strTagName) >> 8), 
                (byte)Encoding.UTF8.GetByteCount(strTagName) });
            Extensions.Append(ref searchArray, Encoding.UTF8.GetBytes(strTagName)); // Tag name
            int locationOfShort = wholeArray.IndexOf(searchArray) + searchArray.Length;
            byte bFirst = wholeArray[locationOfShort];
            byte bSecond = wholeArray[locationOfShort + 1];

            return (short)((bFirst << 8) + bSecond);
        }

        public static int GetInt(this byte[] wholeArray, string strTagName)
        {
            byte[] searchArray = { Tags.NBT_Int }; // One tag byte
            Extensions.Append(ref searchArray, new byte[] { // Two bytes for length of tag name
                (byte)(Encoding.UTF8.GetByteCount(strTagName) >> 8), 
                (byte)Encoding.UTF8.GetByteCount(strTagName) });
            Extensions.Append(ref searchArray, Encoding.UTF8.GetBytes(strTagName)); // Tag name
            int locationOfInt = wholeArray.IndexOf(searchArray) + searchArray.Length;
            byte[] quadArray = wholeArray.SubArray(locationOfInt, 4); // Get the four bytes that make up an int,
            Array.Reverse(quadArray); // and reverse them
            return BitConverter.ToInt32(quadArray, 0);
        }



    }

    public static class Extensions
    {
        private static bool IsSubArrayEqual(byte[] x, byte[] y, int start)
        {
            for (int i = 0; i < y.Length; i++)
            {
                if (x[start++] != y[i]) return false;
            }
            return true;
        }
        public static int IndexOf(this byte[] wholeArray, byte[] searchArray)
        {
            int max = 1 + wholeArray.Length - searchArray.Length;
            for (int i = 0; i < max; i++)
            {
                if (IsSubArrayEqual(wholeArray, searchArray, i)) return i;
            }
            return -1;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        //TODO: Turn this into an extension. Screw the ref.
        public static void Append(ref byte[] firstArray, byte[] secondArray)
        {
            int initialFirstLength = firstArray.Length;
            Array.Resize(ref firstArray, initialFirstLength + secondArray.Length);
            Array.Copy(secondArray, 0, firstArray, initialFirstLength, secondArray.Length);
        }
    }
}
