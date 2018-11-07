using ObjectDetection;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SignalRT.Metrics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsObjectDetector
{
    public partial class MainForm : Form
    {

        private static string _catalogFile = "label_map.pbtxt";
        private static string _modelFile = "frozen_inference_graph.pb";
        private static int _camera = 0;
        private static string _videoFile = string.Empty;
        private static string _outputVideoFile = string.Empty;

        PerformanceCounter aiMetrics = new PerformanceCounter("AI detection");
        string AIMetrics = string.Empty;             

        VideoCapture capture;
        VideoWriter videoWriter;
        Mat frame;
        Bitmap image;
        ObjectDetector detector = new ObjectDetector();

        private Thread camera;

        bool isCameraRunning = false;

        public MainForm()
        {
            InitializeComponent();

            // Load frozen model names deployed with the project
            //
            cbxModel.Items.Add("faster_rcnn_inception_resnet_v2_atrous_coco_11_06_2017");
            cbxModel.Items.Add("faster_rcnn_resnet101_coco_11_06_2017");
            cbxModel.Items.Add("mask_rcnn_resnet50_atrous_coco_2018_01_28");
            cbxModel.Items.Add("rfcn_resnet101_coco_11_06_2017");
            cbxModel.Items.Add("ssd_inception_v2_coco_11_06_2017");
            cbxModel.Items.Add("ssd_mobilenet_v1_coco_11_06_2017");
            cbxModel.Items.Add("ssd_mobilenet_v1_coco_2017_11_17");
            cbxModel.Items.Add("hands_detection");
            // Select the last one (one of the fastest)
            cbxModel.SelectedIndex = 6;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Select the camera TODO: Combo with the cameras detected in the system
            if (rdbBack.Checked) _camera = 1;
            else _camera = 0;

            // Check if we need to open a video
            //
            if (chbFromFile.Checked)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _videoFile = openFileDialog.FileName;
                    _outputVideoFile = Path.Combine(Path.GetDirectoryName(_videoFile), "output.mp4");
                }
            }
            else _videoFile = string.Empty;

            CaptureCamera();
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            isCameraRunning = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            isCameraRunning = false;
            lblTime.Text = String.Format( "Avg msecons: {0}", AIMetrics );
            Thread.Sleep(5);
            capture.Release();
        }

        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));

            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            
            // Base directory ONE levels UP
            //
            string baseDirectory = Path.GetDirectoryName(currentDirectory);

            string catalog = Path.Combine(baseDirectory, "data", cbxModel.Text, _catalogFile);
            string model = Path.Combine(baseDirectory, "data", cbxModel.Text, _modelFile);

            detector.Initilize(catalog, model);
            camera.Start();
        }

        private void CaptureCameraCallback()
        {
            frame = new Mat();
            if (string.IsNullOrEmpty(_videoFile))
            {
                capture = new VideoCapture();
                capture.Open(_camera);
            }
            else
            {
                capture = new VideoCapture(_videoFile);
            }
            videoWriter = new VideoWriter(_outputVideoFile, FourCC.XVID, 5 /*capture.Fps*/, new OpenCvSharp.Size(capture.FrameWidth, capture.FrameHeight));

            while (isCameraRunning == true)
            {
                bool result = capture.Read(frame);
                if( result )
                { 
                    aiMetrics.Start();
                    detector.Analyze(frame);
                    aiMetrics.Finish();
                    AIMetrics = aiMetrics.LastDurationMS.ToString();
                    videoWriter.Write(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    videoImage.Image = image;
                    image = null;
                }
            }
            videoWriter.Release();
            capture.Release();
            AIMetrics = aiMetrics.AverageDurationMS.ToString();
        }
    }
}
