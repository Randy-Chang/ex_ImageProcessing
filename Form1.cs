using System.Drawing.Imaging;

namespace ex_ImageProcessing
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            btnLoadImageFilePath.Click += btnLoadImageFilePath_Click;
        }


        private void btnLoadImageFilePath_Click(object sender, EventArgs e)
        {
            LoadImage();
        }

        void LoadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbImageFilePath.Text = ofd.FileName;
                ShowPicture(tbImageFilePath.Text);
            }
        }

        void ShowPicture(string Path)
        {
            Image bmp;
            pbFig1.Image = null;
            pbFig2.Image = null;
            pbFig3.Image = null;

            bmp = Image.FromFile(Path);

            ZoomImage(pbFig1, bmp);
            RGBtoGray(pbFig2, bmp);
            Binarization(pbFig3, bmp);
            DifferentialBoundary(pbFig4, pbFig5, bmp);

            ConvolutionBoundary(pbFig6, bmp);

            bmp.Dispose();
        }

        void ConvolutionBoundary(PictureBox pb, Image bmp)
        {
            Image bmpClone;

            bmpClone = (Image)bmp.Clone(); //複製一個重複的影像

            // 定義存圖的框架多大的點陣
            Bitmap bmpZoom = new Bitmap(pb.Width, pb.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // 將bmpZoom建立為新圖形
            Graphics g = Graphics.FromImage(bmpZoom);

            // 定義存圖的 位置 與 長寬大小
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);

            g.DrawImage(bmpClone, rect);

            // Convolution
            //double[,] Gx = new double[3, 3] {   { -1, 0, 1 },
            //                                    { -1, 0, 1 },
            //                                    { -1, 0, 1 } };

            //double[,] Gy = new double[3, 3] {   { -1, -1, -1 },
            //                                    {  0,  0,  0 },
            //                                    {  1,  1,  1 } };

            double[,] Gx = new double[3, 3] {   { -1, 0, 1 },
                                                { -2, 0, 2 },
                                                { -1, 0, 1 } };

            double[,] Gy = new double[3, 3] {   { -1, -2, -1 },
                                                {  0,  0,  0 },
                                                {  1,  2,  1 } };

            double[,] Cx = new double[bmpZoom.Width - 2, bmpZoom.Height - 2];
            double[,] Cy = new double[bmpZoom.Width - 2, bmpZoom.Height - 2];

            double[,] Ary_Gray = new double[bmpZoom.Width, bmpZoom.Height];

            double Sum_Cx = 0; double Sum_Cy = 0;
            double Avg_Cx, Avg_Cy, Avg_C;

            RGBtoGray_1(bmpZoom);

            for (int y = 0; y < bmpZoom.Height; y++)
            {
                for (int x = 0; x < bmpZoom.Width; x++)
                {
                    Ary_Gray[x, y] = bmpZoom.GetPixel(x, y).R;
                }
            }

            for (int y = 1; y < bmpZoom.Height - 1; y++)
            {
                for (int x = 1; x < bmpZoom.Width - 1; x++)
                {
                    Convolution_Ary33(x, y, Gx, Ary_Gray, Cx);
                    Convolution_Ary33(x, y, Gy, Ary_Gray, Cy);
                    Sum_Cx = Sum_Cx + Cx[x - 1, y - 1];
                    Sum_Cy = Sum_Cy + Cy[x - 1, y - 1];
                }
            }

            Avg_Cx = Sum_Cx / (double)Cx.Length * 1;
            Avg_Cy = Sum_Cy / (double)Cy.Length * 1;
            Avg_C = (Sum_Cx + Sum_Cy) / (double)Cy.Length * 1;

            for (int y = 0; y < bmpZoom.Height - 2; y++)
            {
                for (int x = 0; x < bmpZoom.Width - 2; x++)
                {
                    if (Cx[x, y] + Cy[x, y] > Avg_C)//if (Cx[x, y] > Avg_Cx & Cy[x, y] > Avg_Cy)
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }

            pb.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);

            bmpZoom.Dispose();
            bmpClone.Dispose();
        }

        void Convolution_Ary33(int x, int y, double[,] A1, double[,] A2, double[,] B)
        {
            B[x - 1, y - 1] = A1[0, 0] * A2[x - 1, y - 1] + A1[1, 0] * A2[x, y - 1] + A1[2, 0] * A2[x + 1, y - 1] +
                          A1[0, 1] * A2[x - 1, y] + A1[1, 1] * A2[x, y] + A1[2, 1] * A2[x + 1, y] +
                          A1[0, 2] * A2[x - 1, y + 1] + A1[1, 2] * A2[x, y + 1] + A1[2, 2] * A2[x + 1, y + 1];
        }


        void DifferentialBoundary(PictureBox pb, PictureBox pb2, Image bmp)
        {
            Image bmpClone;

            bmpClone = (Image)bmp.Clone(); //複製一個重複的影像

            // 定義存圖的框架多大的點陣
            Bitmap bmpZoom = new Bitmap(pb.Width, pb.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // 將bmpZoom建立為新圖形
            Graphics g = Graphics.FromImage(bmpZoom);

            // 定義存圖的 位置 與 長寬大小
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);

            g.DrawImage(bmpClone, rect);

            RGBtoGray_1(bmpZoom);

            int[,] Ary_dR = new int[bmpZoom.Width - 1, bmpZoom.Height - 1];
            int[,] Ary_dG = new int[bmpZoom.Width, bmpZoom.Height];
            int[,] Ary_dB = new int[bmpZoom.Width, bmpZoom.Height];
            int Sum_dR = 0; int Sum_dG = 0; int Sum_dB = 0;
            double Avg_dR, Avg_dG, Avg_dB;

            // 微分
            for (int y = 0; y < bmpZoom.Height - 1; y++)
            {
                for (int x = 0; x < bmpZoom.Width - 1; x++)
                {
                    int R = bmpZoom.GetPixel(x, y).R;
                    int G = bmpZoom.GetPixel(x, y).G;
                    int B = bmpZoom.GetPixel(x, y).B;

                    Ary_dR[x, y] = bmpZoom.GetPixel(x + 1, y + 1).R - bmpZoom.GetPixel(x, y).R;
                    Ary_dG[x, y] = bmpZoom.GetPixel(x + 1, y + 1).G - bmpZoom.GetPixel(x, y).G;
                    Ary_dB[x, y] = bmpZoom.GetPixel(x + 1, y + 1).B - bmpZoom.GetPixel(x, y).B;

                    Sum_dR = Sum_dR + Ary_dR[x, y];
                    Sum_dG = Sum_dG + Ary_dG[x, y];
                    Sum_dB = Sum_dB + Ary_dB[x, y];
                }
            }
            double Weight = 5;
            Avg_dR = (double)Sum_dR / (double)(Ary_dR.Length) + Weight;
            Avg_dG = (double)Sum_dG / (double)(Ary_dG.Length) + Weight;
            Avg_dB = (double)Sum_dB / (double)(Ary_dB.Length) + Weight;

            for (int y = 0; y < bmpZoom.Height - 1; y++)
            {
                for (int x = 0; x < bmpZoom.Width - 1; x++)
                {
                    try
                    {
                        var rr = Ary_dR[x, y];
                        if (Ary_dR[x, y] > Avg_dR || Ary_dG[x, y] > Avg_dG || Ary_dB[x, y] > Avg_dB)
                        {
                            bmpZoom.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                    }
                    catch (Exception ex)
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }

            pb.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);

            for (int y = 0; y < bmpZoom.Height - 1; y++)
            {
                for (int x = 0; x < bmpZoom.Width - 1; x++)
                {
                    try
                    {
                        if (Ary_dR[x, y] > Avg_dR & Ary_dG[x, y] > Avg_dG & Ary_dB[x, y] > Avg_dB)
                        {
                            bmpZoom.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                    }
                    catch
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            pb2.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);

            bmpZoom.Dispose();
            bmpClone.Dispose();
        }

        // ---------- Zoom Image --------------------------
        void ZoomImage(PictureBox pb, Image bmp)
        {
            Image bmpClone;

            bmpClone = (Image)bmp.Clone(); //複製一個重複的影像

            // 定義存圖的框架多大的點陣
            Bitmap bmpZoom = new Bitmap(pb.Width, pb.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // 將bmpZoom建立為新圖形
            Graphics g = Graphics.FromImage(bmpZoom);

            // 定義存圖的 位置 與 長寬大小
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);

            g.DrawImage(bmpClone, rect);
            pb.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);
            bmpZoom.Dispose();
            bmpClone.Dispose();
        }

        // ---------- RGB to Gray --------------------------
        void RGBtoGray(PictureBox pb, Image bmp)
        {
            Image bmpClone;

            bmpClone = (Image)bmp.Clone(); //複製一個重複的影像

            // 定義存圖的框架多大的點陣
            Bitmap bmpZoom = new Bitmap(pb.Width, pb.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // 將bmpZoom建立為新圖形
            Graphics g = Graphics.FromImage(bmpZoom);

            // 定義存圖的 位置 與 長寬大小
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);

            g.DrawImage(bmpClone, rect);

            // ---------- RGBtoGray ----------
            RGBtoGray_1(bmpZoom);

            // ---------- RGBtoGray ----------
            RGBtoGray_2(bmpZoom, g, rect);


            pb.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);
            bmpZoom.Dispose();
            bmpClone.Dispose();
        }
        void RGBtoGray_1(Bitmap bmpZoom)
        {
            for (int y = 0; y < bmpZoom.Height; y++)
            {
                for (int x = 0; x < bmpZoom.Width; x++)
                {
                    int R = bmpZoom.GetPixel(x, y).R;
                    int G = bmpZoom.GetPixel(x, y).G;
                    int B = bmpZoom.GetPixel(x, y).B;
                    int gray = (bmpZoom.GetPixel(x, y).R +
                                bmpZoom.GetPixel(x, y).G +
                                bmpZoom.GetPixel(x, y).B) / 3;

                    Color color = Color.FromArgb(gray, gray, gray);
                    bmpZoom.SetPixel(x, y, color);

                }
            }
        }

        void RGBtoGray_2(Bitmap bmpZoom, Graphics g, Rectangle rect)
        {
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
                new float[] { 0.30f, 0.30f, 0.30f, 0.00f, 0.00f } ,
                new float[] { 0.59f, 0.59f, 0.59f, 0.00f, 0.00f } ,
                new float[] { 0.11f, 0.11f, 0.11f, 0.00f, 0.00f } ,
                new float[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f } ,
                new float[] { 0.00f, 0.00f, 0.00f, 0.00f, 1.00f }
                });
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            Bitmap bitmap = new Bitmap(bmpZoom.Width, bmpZoom.Height, bmpZoom.PixelFormat);
            Graphics gg = Graphics.FromImage(bitmap);
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            g.DrawImage(bmpZoom, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, ia);
        }

        // ---------- RGB to Gray End--------------------------


        void Binarization(PictureBox pb, Image bmp)
        {
            Image bmpClone;

            bmpClone = (Image)bmp.Clone(); //複製一個重複的影像

            // 定義存圖的框架多大的點陣
            Bitmap bmpZoom = new Bitmap(pb.Width, pb.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // 將bmpZoom建立為新圖形
            Graphics g = Graphics.FromImage(bmpZoom);

            // 定義存圖的 位置 與 長寬大小
            Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);

            g.DrawImage(bmpClone, rect);

            // Binarization 二值化
            for (int y = 0; y < bmpZoom.Height; y++)
            {
                for (int x = 0; x < bmpZoom.Width; x++)
                {
                    int R = bmpZoom.GetPixel(x, y).R;
                    int G = bmpZoom.GetPixel(x, y).G;
                    int B = bmpZoom.GetPixel(x, y).B;

                    if ((R + G + B) / 3 >= 127)
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmpZoom.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            pb.Image = bmpZoom.Clone(new Rectangle(0, 0, bmpZoom.Width, bmpZoom.Height), bmpZoom.PixelFormat);
            bmpZoom.Dispose();
            bmpClone.Dispose();
        }

    }
}