using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TensorFlow;

namespace ObjectDetection
{
    public class ObjectDetector
    {
        private IEnumerable<CatalogItem> _Catalog;
        private TFGraph _Graph;
        private TFSession _Session; 

        public delegate void ImageFechHandler();

        //public event ImageFechHandler OnFechImage;

        public void Initilize( string catalogPath, string modelPath )
        {
            _Catalog = CatalogUtil.ReadCatalogItems(catalogPath);
            _Graph = new TFGraph();
            var model = File.ReadAllBytes(modelPath);
            _Graph.Import(new TFBuffer(model));
            _Session = new TFSession(_Graph);
        }

        public void Analyze( string inputFile, string outputFile )
        {
            Stopwatch sw = new Stopwatch();

            var img = Cv2.ImRead(inputFile);

            var fileTuples = new List<(string input, string output)> { (inputFile, outputFile) };

            foreach (var tuple in fileTuples)
            {
                var tensor = ImageUtil.CreateTensorFromImageFile(tuple.input, TFDataType.UInt8);
                var runner = _Session.GetRunner();
                runner
                        .AddInput(_Graph["image_tensor"][0], tensor)
                        .Fetch(
                            _Graph["detection_boxes"][0],
                            _Graph["detection_scores"][0],
                            _Graph["detection_classes"][0],
                            _Graph["num_detections"][0]);

                var output = runner.Run();
                var boxes = (float[,,])output[0].GetValue(jagged: false);
                var scores = (float[,])output[1].GetValue(jagged: false);
                var classes = (float[,])output[2].GetValue(jagged: false);
                var num = (float[])output[3].GetValue(jagged: false);

                #region show image
                for (int i = 0; i < boxes.GetLength(1); i++)
                {
                    float score = scores[0, i];

                    if (score > 0.50)
                    {
                        Scalar color = Scalar.Red;
                        if (score > 0.75) color = Scalar.Green;
                        else if (score > 0.50) color = Scalar.Orange;

                        var idx = Convert.ToInt32(classes[0, i]);
                        var x1 = (int)(boxes[0, i, 1] * img.Width);
                        var y1 = (int)(boxes[0, i, 0] * img.Height);
                        var x2 = (int)(boxes[0, i, 3] * img.Width);
                        var y2 = (int)(boxes[0, i, 2] * img.Height);
                        var catalog = _Catalog.First(x => x.Id == idx);
                        string label = $"{(catalog == null ? idx.ToString() : catalog.DisplayName)}: {scores[0, i] * 100:0.00}%";
                        Console.WriteLine($"{label} {x1} {y1} {x2} {y2}");
                        Cv2.Rectangle(img, new Rect(x1, y1, x2 - x1, y2 - y1), color, 2);
                        int baseline;
                        var textSize = Cv2.GetTextSize(label, HersheyFonts.HersheyTriplex, 0.5, 1, out baseline);
                        textSize.Height = textSize.Height + baseline / 2;
                        var y = y1 - textSize.Height < 0 ? y1 + textSize.Height : y1;
                        Cv2.Rectangle(img, new Rect(x1, y - textSize.Height, textSize.Width, textSize.Height + baseline / 2), color, Cv2.FILLED);
                        Cv2.PutText(img, label, new Point(x1, y), HersheyFonts.HersheyTriplex, 0.5, Scalar.Black);
                    }
                }
                #endregion
            }

            using (new Window("image", img))
            {
                Cv2.WaitKey();
            }
        }

        public void Analyze( Mat inputImage )
        {
            var img = inputImage;

            var tensor = ImageUtil.CreateTensorFromImage(inputImage, TFDataType.UInt8);
            var runner = _Session.GetRunner();
                runner
                    .AddInput(_Graph["image_tensor"][0], tensor)
                        .Fetch(
                            _Graph["detection_boxes"][0],
                            _Graph["detection_scores"][0],
                            _Graph["detection_classes"][0],
                            _Graph["num_detections"][0]);

            var output = runner.Run();
            var boxes = (float[,,])output[0].GetValue(jagged: false);
            var scores = (float[,])output[1].GetValue(jagged: false);
            var classes = (float[,])output[2].GetValue(jagged: false);
            var num = (float[])output[3].GetValue(jagged: false);

            #region show image
            for (int i = 0; i < boxes.GetLength(1); i++)
            {
                float score = scores[0, i];

                if (score > 0.50)
                {
                    Scalar color = Scalar.Red;
                    if (score > 0.75) color = Scalar.Green;
                    else if (score > 0.50) color = Scalar.Orange;

                    var idx = Convert.ToInt32(classes[0, i]);
                    var x1 = (int)(boxes[0, i, 1] * img.Width);
                    var y1 = (int)(boxes[0, i, 0] * img.Height);
                    var x2 = (int)(boxes[0, i, 3] * img.Width);
                    var y2 = (int)(boxes[0, i, 2] * img.Height);
                    var catalog = _Catalog.First(x => x.Id == idx);
                    string label = $"{(catalog == null ? idx.ToString() : catalog.DisplayName)}: {scores[0, i] * 100:0.00}%";
                    Console.WriteLine($"{label} {x1} {y1} {x2} {y2}");
                    Cv2.Rectangle(img, new Rect(x1, y1, x2 - x1, y2 - y1), color, 2);
                    int baseline;
                    var textSize = Cv2.GetTextSize(label, HersheyFonts.HersheyTriplex, 0.5, 1, out baseline);
                    textSize.Height = textSize.Height + baseline / 2;
                    var y = y1 - textSize.Height < 0 ? y1 + textSize.Height : y1;
                    Cv2.Rectangle(img, new Rect(x1, y - textSize.Height, textSize.Width, textSize.Height + baseline / 2), color, Cv2.FILLED);
                    Cv2.PutText(img, label, new Point(x1, y), HersheyFonts.HersheyTriplex, 0.5, Scalar.Black);
                }
            }
            #endregion
        }
    }
}
