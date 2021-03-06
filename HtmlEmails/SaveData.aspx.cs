﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlEmails
{
    public partial class SaveData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(0, 10000);

            // Save Image
            if (Request.Files[0].ContentLength > 0)
            {
                Request.Files[0].SaveAs(Server.MapPath("PreviewImage.jpg"));
            }
                
            // Capture form text data
            string userName = Request.Form["userName"].ToString();
            string newFileBody = PopulateBody(userName, "", "", "", rndNumber);
            using (StreamWriter writer =
                new StreamWriter(Server.MapPath("EmailOutput.htm"), false))
            {
                writer.Write(newFileBody);
            }
            Context.Response.ContentType = "text/HTML";
            Context.Response.Write(newFileBody);

            //Context.Response.ContentType = "text/plain";
            //Context.Response.Write("PreviewImage.jpg");

            //string json = "{\"avatar_url\":\"Penguins.jpg\"}";
            //Response.ContentType = "application/json; charset=utf-8";
            //Response.Write(json);
            //Response.End();
        }


        private string PopulateBody(string userName, string title, string url,
            string description, int rndNumber)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{dummyImgNumber}", rndNumber.ToString());
            //body = body.Replace("{Title}", title);
            //body = body.Replace("{Url}", url);
            //body = body.Replace("{Description}", description);
            return body;
        }


        /// <summary>
        /// Resizes and rotates an image, keeping the original aspect ratio. Does not dispose the original
        /// Image instance.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="width">desired width</param>
        /// <param name="height">desired height</param>
        /// <param name="rotateFlipType">desired RotateFlipType</param>
        /// <returns>new resized/rotated Image instance</returns>
        public static System.Drawing.Image Resize(System.Drawing.Image image, int width,
            int height, RotateFlipType rotateFlipType)
        {
            // clone the Image instance, since we don't want to resize the original Image instance
            var rotatedImage = image.Clone() as System.Drawing.Image;
            //rotatedImage.RotateFlip(rotateFlipType);
            var newSize = CalculateResizedDimensions(rotatedImage, width, height);

            var resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
            resizedImage.SetResolution(72, 72);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                // set parameters to create a high-quality thumbnail
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // use an image attribute in order to remove the black/gray border around image after resize
                // (most obvious on white images), see this post for more information:
                // http://www.codeproject.com/KB/GDI-plus/imgresizoutperfgdiplus.aspx
                using (var attribute = new ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);

                    // draws the resized image to the bitmap
                    graphics.DrawImage(rotatedImage, new Rectangle(new Point(0, 0), newSize), 0, 0, rotatedImage.Width, rotatedImage.Height, GraphicsUnit.Pixel, attribute);
                }
            }

            return resizedImage;
        }

        /// <summary>
        /// Calculates resized dimensions for an image, preserving the aspect ratio.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="desiredWidth">desired width</param>
        /// <param name="desiredHeight">desired height</param>
        /// <returns>Size instance with the resized dimensions</returns>
        private static Size CalculateResizedDimensions(System.Drawing.Image image, int desiredWidth, int desiredHeight)
        {
            var widthScale = (double)desiredWidth / image.Width;
            var heightScale = (double)desiredHeight / image.Height;

            // scale to whichever ratio is smaller, this works for both scaling up and scaling down
            var scale = widthScale < heightScale ? widthScale : heightScale;

            return new Size
            {
                Width = (int)(scale * image.Width),
                Height = (int)(scale * image.Height)
            };
        }



    }
}