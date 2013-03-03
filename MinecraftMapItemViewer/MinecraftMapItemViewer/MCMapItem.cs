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

        #endregion


    }

    public static class NBTExtensions
    {
        public static byte GetByte(this byte[] wholeArray, string strTagName)
        {
            byte[] searchArray = {0x01}; //TODO: Make constants like NBT_Byte = 0x01, etc
            Extensions.Append(ref searchArray, new byte[] { (byte)(strTagName.Length >> 8), (byte)strTagName.Length });
            Extensions.Append(ref searchArray, Encoding.UTF8.GetBytes(strTagName));
            int locationOfByte = wholeArray.IndexOf(searchArray) + searchArray.Length;
            return wholeArray[locationOfByte];
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

        //TODO: Turn this into an extension. Screw the ref.
        public static void Append(ref byte[] firstArray, byte[] secondArray)
        {
            int initialFirstLength = firstArray.Length;
            Array.Resize(ref firstArray, initialFirstLength + secondArray.Length);
            Array.Copy(secondArray, 0, firstArray, initialFirstLength, secondArray.Length);
        }
    }
}
