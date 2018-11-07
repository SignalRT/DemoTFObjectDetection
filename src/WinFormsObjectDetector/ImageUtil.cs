using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TensorFlow;

namespace ObjectDetection
{
    public static class ImageUtil
    {
        // Convert the image in filename to a Tensor suitable as input to the Inception model.
        //
        public static TFTensor CreateTensorFromImageFile(string file, TFDataType destinationDataType = TFDataType.Float)
        {

            var contents = File.ReadAllBytes(file);

            // DecodeJpeg uses a scalar String-valued tensor as input.
            //
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            // Construct a graph to normalize the image
            //
            using (var graph = ConstructGraphToNormalizeImage(out input, out output, destinationDataType) )
            {
                // Execute that graph to normalize this one image
                //
                using (var session = new TFSession(graph))
                {

                    var normalized = session.Run(

                        inputs: new[] { input },

                        inputValues: new[] { tensor },

                        outputs: new[] { output });

                    return normalized[0];
                }
            }
        }

        public static TFTensor CreateTensorFromImage(Mat Image, TFDataType destinationDataType = TFDataType.Float)
        {

            var contents = Image.ToBytes();

            // DecodeJpeg uses a scalar String-valued tensor as input.
            //
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            // Construct a graph to normalize the image
            //
            using (var graph = ConstructGraphToNormalizeImage(out input, out output, destinationDataType))
            {
                // Execute that graph to normalize this one image
                //
                using (var session = new TFSession(graph))
                {

                    var normalized = session.Run(

                        inputs: new[] { input },

                        inputValues: new[] { tensor },

                        outputs: new[] { output });

                    return normalized[0];
                }
            }
        }


        // The inception model takes as input the image described by a Tensor in a very
        // specific normalized format (a particular image size, shape of the input tensor,
        // normalized pixel values etc.).
        //
        // This function constructs a graph of TensorFlow operations which takes as
        // input a JPEG-encoded string and returns a tensor suitable as input to the
        // inception model.
        //
        private static TFGraph ConstructGraphToNormalizeImage( out TFOutput input, out TFOutput output, TFDataType destinationDataType = TFDataType.Float)
        {
            var graph = new TFGraph();

            input = graph.Placeholder(TFDataType.String);

            output = graph.Cast(
                        graph.ExpandDims(
                                input: graph.Cast(graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
                                dim: graph.Const(0, "make_batch")
                       )
                       , destinationDataType
                    );

            return graph;
        }
    }
}
