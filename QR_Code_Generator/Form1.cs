using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using ZXing;

namespace QR_Code_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var result = QCwriter.Write(textBox1.Text);
            string path ="C:/Users/Administrator/Desktop/QRimage.jpg";
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path,
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            MessageBox.Show("QR code is generated..");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                var QCreader = new BarcodeReader();
                string QCfilename = dlg.FileName;
                  // ("~/images"), "MyQRImage.jpg");
                var QCresult = QCreader.Decode(new Bitmap(QCfilename));
                if (QCresult != null)
                {
                    MessageBox.Show("QR Code Text : " + QCresult.Text);
                }
            }
        }
    }
}
