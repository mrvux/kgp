using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Speech command processor, runs grammar element when semantic is recognized
    /// </summary>
    public class SpeechCommandProcessor
    {
        private readonly KinectSensor sensor;
        private readonly KinectAudioStream kinectAudioStream;
        private SpeechRecognitionEngine speechEngine = null;

        private readonly IEnumerable<ISpeechGrammarElement> grammarElements;

        public double ConfidenceThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// Utility to construct a processor from params array
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="grammarElements">Grammar elements</param>
        /// <returns>Speech Command processor</returns>
        public static SpeechCommandProcessor FromArray(KinectSensor sensor, params ISpeechGrammarElement[] grammarElements)
        {
            return new SpeechCommandProcessor(sensor, grammarElements);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="grammarElements">Grammar elements</param>
        public SpeechCommandProcessor(KinectSensor sensor, IEnumerable<ISpeechGrammarElement> grammarElements)
        {
            if (sensor == null)
                throw new ArgumentNullException("sensor");
            if (grammarElements == null)
                throw new ArgumentNullException("grammarElements");

            this.ConfidenceThreshold = 0.9f;
            this.sensor = sensor;
            this.grammarElements = grammarElements;

            IReadOnlyList<AudioBeam> audioBeamList = this.sensor.AudioSource.AudioBeams;
            System.IO.Stream audioStream = audioBeamList[0].OpenInputStream();

            // create the convert stream
            this.kinectAudioStream = new KinectAudioStream(audioStream);

            RecognizerInfo ri = TryGetKinectRecognizer();

            if (null != ri)
            {
                this.speechEngine = new SpeechRecognitionEngine(ri.Id);

                var commandList = new Choices();
                foreach (ISpeechGrammarElement sp in this.grammarElements)
                {
                    foreach (string word in sp.WordList)
                    {
                        commandList.Add(new SemanticResultValue(word, sp.Semantic));
                    }
                }

                var gb = new GrammarBuilder { Culture = ri.Culture };
                gb.Append(commandList);

                var g = new Grammar(gb);

                this.speechEngine.LoadGrammar(g);

                this.speechEngine.SpeechRecognized += this.SpeechRecognized;
                this.kinectAudioStream.SpeechActive = true;
                this.speechEngine.SetInputToAudioStream(
                this.kinectAudioStream, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                var gElement = this.grammarElements.Where(ig => ig.Semantic == e.Result.Semantics.Value.ToString()).FirstOrDefault();
                if (gElement != null)
                {
                    gElement.Recognized(e.Result.Confidence);
                }
            }
        }

        /// <summary>
        /// Gets the metadata for the speech recognizer (acoustic model) most suitable to
        /// process audio from Kinect device.
        /// </summary>
        /// <returns>
        /// RecognizerInfo if found, <code>null</code> otherwise.
        /// </returns>
        private static RecognizerInfo TryGetKinectRecognizer()
        {
            IEnumerable<RecognizerInfo> recognizers;
            try
            {
                recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            }
            catch (COMException)
            {
                return null;
            }

            foreach (RecognizerInfo recognizer in recognizers)
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }

    }
}
