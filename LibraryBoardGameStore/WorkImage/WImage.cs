using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace LibraryBoardGameStore.WorkImage
{
    public static class WImage
    {
        public static byte[] CreateCopy(string file)
        {
            Image img = Image.FromFile(file);
            int maxWid = 300;
            int maxHeid = 300;
            double ratioX = (double)maxWid / img.Width;
            double ratioY = (double)maxHeid / img.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newW = (int)(img.Width * ratio);
            int newH = (int)(img.Height * ratio);
            Image mi = new Bitmap(newW, newH);
            Graphics g = Graphics.FromImage(mi);
            g.DrawImage(img, 0, 0, newW, newH);
            MemoryStream ms = new MemoryStream();
            mi.Save(ms, ImageFormat.Jpeg);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            return br.ReadBytes((int)ms.Length);
        }
        public static Image GetImage(byte[] vs)
        {
            MemoryStream ms = new MemoryStream(vs);
            return Image.FromStream(ms);
        }
    }
}
