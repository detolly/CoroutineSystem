using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using Cudafy;
using Cudafy.Translator;
using Cudafy.Host;

namespace CoroutineSystem
{
    public partial class Form1 : Form
    {
        #region Declarations
        public bool hasClosed = false;
        private Random random = new Random();
        #endregion

        #region Init
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        
        public IEnumerator ChangeColor()
        {
            int[] rgbColour = new int[] { 255,0,0 };

            for (int decColour = 0; decColour < 3; decColour += 1)
            {
                int incColour = decColour == 2 ? 0 : decColour + 1;

                // cross-fade the two colours.
                for (int i = 0; i < 255; i += 1)
                {
                    rgbColour[decColour] -= 1;
                    rgbColour[incColour] += 1;
                        
                    BackColor = Color.FromArgb(255, rgbColour[0], rgbColour[1], rgbColour[2]);
                    yield return new WaitForMilliseconds(5);
                }
            }

            BackColor = SystemColors.Control;
        }

        public void Random()
        {

        }

        [Cudafy]
        public static void add(float a, float b, float[] c)
        {
            c[0] = a + b;
        }

        [Cudafy]
        public static void sin(float a, float[] b)
        {
            b[0] = (float)Math.Sin(a);
        }

        public IEnumerator SpawnText()
        {
            //CudafyModule km = CudafyTranslator.Cudafy();
            //GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target);
            //gpu.LoadModule(km);
            using (Label label = new Label())
            {
                label.Text = "Greetings!";
                label.Location = new Point(random.Next(Width), 0);
                Controls.Add(label);

                float comparison = (float)Math.PI / 2f;

                for (float angle = 0; angle < comparison; angle += 0.02f)
                {
                    float output = (float)Math.Sin(angle);
                    //float[] dev_output = gpu.Allocate<float>();
                    //gpu.Launch().sin(angle, dev_output);
                    //gpu.CopyFromDevice(dev_output, out output);
                    label.Location = new Point(label.Location.X, (int)(Height * output));
                    yield return new WaitForMilliseconds(10);
                }
                for (float angle = (float)Math.PI / 2f; angle >= 0; angle -= 0.02f)
                {
                    float output = (float)Math.Sin(angle);
                    //float[] dev_output = gpu.Allocate<float>();
                    //gpu.Launch().sin(angle, dev_output);
                    //gpu.CopyFromDevice(dev_output, out output);
                    label.Location = new Point(label.Location.X, (int)(Height * output));
                    yield return new WaitForMilliseconds(10);
                }
            }
        }

        #region Events
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            hasClosed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //color
            CoroutineManager.StartCoroutine(ChangeColor());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //stop
            CoroutineManager.StopAllCoroutines();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //random
            Random();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //text
            CoroutineManager.StartCoroutine(SpawnText());
        }
        #endregion
    }
}
