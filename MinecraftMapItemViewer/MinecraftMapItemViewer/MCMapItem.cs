using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftMapItemViewer
{
    public class MCMapItem
    {
        private Dictionary<int, Color> _colorTable;
        
        private byte _scale;
        private byte _dimension;
        private short _height;
        private short _width;
        private int _xCenter;
        private int _zCenter;
        private byte[] _colors;

        public MCMapItem(byte[] wholeArray)
        {
            BuildColorTable();
            _scale = wholeArray.GetByte("scale");
            _dimension = wholeArray.GetByte("dimension");
            _height = wholeArray.GetShort("height");
            _width = wholeArray.GetShort("width");
            _xCenter = wholeArray.GetInt("xCenter");
            _zCenter = wholeArray.GetInt("zCenter");
            _colors = wholeArray.GetByteArray("colors");
        }

        private void BuildColorTable()
        {
            _colorTable = new Dictionary<int,Color>();
            _colorTable.Add(0, Color.FromArgb(0, 0, 0, 0));
            _colorTable.Add(1, Color.FromArgb(0, 0, 0, 0));
            _colorTable.Add(2, Color.FromArgb(0, 0, 0, 0));
            _colorTable.Add(3, Color.FromArgb(0, 0, 0, 0));
            _colorTable.Add(4, Color.FromArgb(255, 89, 125, 39));
            _colorTable.Add(5, Color.FromArgb(255, 109, 153, 48));
            _colorTable.Add(6, Color.FromArgb(255, 127, 178, 56));
            _colorTable.Add(7, Color.FromArgb(255, 109, 153, 48));
            _colorTable.Add(8, Color.FromArgb(255, 174, 164, 115));
            _colorTable.Add(9, Color.FromArgb(255, 213, 201, 140));
            _colorTable.Add(10, Color.FromArgb(255, 247, 233, 163));
            _colorTable.Add(11, Color.FromArgb(255, 213, 201, 140));
            _colorTable.Add(12, Color.FromArgb(255, 117, 117, 117));
            _colorTable.Add(13, Color.FromArgb(255, 144, 144, 144));
            _colorTable.Add(14, Color.FromArgb(255, 167, 167, 167));
            _colorTable.Add(15, Color.FromArgb(255, 144, 144, 144));
            _colorTable.Add(16, Color.FromArgb(255, 180, 0, 0));
            _colorTable.Add(17, Color.FromArgb(255, 220, 0, 0));
            _colorTable.Add(18, Color.FromArgb(255, 255, 0, 0));
            _colorTable.Add(19, Color.FromArgb(255, 220, 0, 0));
            _colorTable.Add(20, Color.FromArgb(255, 112, 112, 180));
            _colorTable.Add(21, Color.FromArgb(255, 138, 138, 220));
            _colorTable.Add(22, Color.FromArgb(255, 160, 160, 255));
            _colorTable.Add(23, Color.FromArgb(255, 138, 138, 220));
            _colorTable.Add(24, Color.FromArgb(255, 117, 117, 117));
            _colorTable.Add(25, Color.FromArgb(255, 144, 144, 144));
            _colorTable.Add(26, Color.FromArgb(255, 167, 167, 167));
            _colorTable.Add(27, Color.FromArgb(255, 144, 144, 144));
            _colorTable.Add(28, Color.FromArgb(255, 0, 87, 0));
            _colorTable.Add(29, Color.FromArgb(255, 0, 106, 0));
            _colorTable.Add(30, Color.FromArgb(255, 0, 124, 0));
            _colorTable.Add(31, Color.FromArgb(255, 0, 106, 0));
            _colorTable.Add(32, Color.FromArgb(255, 180, 180, 180));
            _colorTable.Add(33, Color.FromArgb(255, 220, 220, 220));
            _colorTable.Add(34, Color.FromArgb(255, 255, 255, 255));
            _colorTable.Add(35, Color.FromArgb(255, 220, 220, 220));
            _colorTable.Add(36, Color.FromArgb(255, 115, 118, 129));
            _colorTable.Add(37, Color.FromArgb(255, 141, 144, 158));
            _colorTable.Add(38, Color.FromArgb(255, 164, 168, 184));
            _colorTable.Add(39, Color.FromArgb(255, 141, 144, 158));
            _colorTable.Add(40, Color.FromArgb(255, 129, 74, 33));
            _colorTable.Add(41, Color.FromArgb(255, 157, 91, 40));
            _colorTable.Add(42, Color.FromArgb(255, 183, 106, 47));
            _colorTable.Add(43, Color.FromArgb(255, 157, 91, 40));
            _colorTable.Add(44, Color.FromArgb(255, 79, 79, 79));
            _colorTable.Add(45, Color.FromArgb(255, 96, 96, 96));
            _colorTable.Add(46, Color.FromArgb(255, 112, 112, 112));
            _colorTable.Add(47, Color.FromArgb(255, 96, 96, 96));
            _colorTable.Add(48, Color.FromArgb(255, 45, 45, 180));
            _colorTable.Add(49, Color.FromArgb(255, 55, 55, 220));
            _colorTable.Add(50, Color.FromArgb(255, 64, 64, 255));
            _colorTable.Add(51, Color.FromArgb(255, 55, 55, 220));
            _colorTable.Add(52, Color.FromArgb(255, 73, 58, 35));
            _colorTable.Add(53, Color.FromArgb(255, 89, 71, 43));
            _colorTable.Add(54, Color.FromArgb(255, 104, 83, 50));
            _colorTable.Add(55, Color.FromArgb(255, 89, 71, 43));

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

        public Color[,] Colors
        {
            get
            {
                Color[,] returnArray = new Color[_width, _height];
                int i = 0;
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        returnArray[x, y] = _colorTable[_colors[i]];
                        i++;
                    }
                }
                return returnArray;
            }
        }

        public Bitmap Image
        {
            get
            {
                Bitmap bmp = new Bitmap(_width, _height);
                int i = 0;
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        bmp.SetPixel(x, y, _colorTable[_colors[i]]);
                        i++;
                    }
                }
                return bmp;
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

        public static byte[] GetByteArray(this byte[] wholeArray, string strTagName)
        {
            int ArraySize;
            byte[] searchArray = { Tags.NBT_Byte_Array }; // One tag byte
            Extensions.Append(ref searchArray, new byte[] { // Two bytes for length of tag name
                (byte)(Encoding.UTF8.GetByteCount(strTagName) >> 8), 
                (byte)Encoding.UTF8.GetByteCount(strTagName) });
            Extensions.Append(ref searchArray, Encoding.UTF8.GetBytes(strTagName)); // Tag name
            int locationOfInt = wholeArray.IndexOf(searchArray) + searchArray.Length;
            byte[] quadArray = wholeArray.SubArray(locationOfInt, 4); // Get the four bytes that make up an int,
            Array.Reverse(quadArray); // and reverse them
            ArraySize = BitConverter.ToInt32(quadArray, 0); // gives us the size of our array.
            return wholeArray.SubArray((locationOfInt + 4), ArraySize);
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
