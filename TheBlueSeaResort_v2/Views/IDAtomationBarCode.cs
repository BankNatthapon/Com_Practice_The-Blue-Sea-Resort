﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.IO;
using System.Drawing.Imaging;

namespace TheBlueSeaResort_v2.Views.BarCode
{
    public class IDAtomationBarCode
    {
        public static string BarcodeImageGenerator(string Code)
        {
            byte[] BarCode;
            string BarCodeImage;
            Bitmap objBitmap = new Bitmap(Code.Length * 28, 100);
            using (Graphics graphic = Graphics.FromImage(objBitmap))
            {
                Font newFont = new Font("IDAutomationHC39M Free Version", 18, FontStyle.Regular);
                PointF point = new PointF(2F,2F);
                SolidBrush balck = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                graphic.FillRectangle(white, 0, 0, objBitmap.Width, objBitmap.Height);
                graphic.DrawString("*" + Code + "*", newFont, balck, point);
            }
            using (MemoryStream Mmst = new MemoryStream())
            {
                objBitmap.Save(Mmst, ImageFormat.Png);
                BarCode = Mmst.GetBuffer();
                BarCodeImage = BarCode != null ? "data:image/jpg;base64," + Convert.ToBase64String((byte[])BarCode) : "";
                return BarCodeImage;
            }
        }
    }
}