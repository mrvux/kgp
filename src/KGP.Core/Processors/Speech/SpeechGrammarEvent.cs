using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Event version of speech grammar element, raises appropriate event when recognized
    /// </summary>
    public class SpeechGrammarEvent : ISpeechGrammarElement
    {
        private string semantic;
        private IEnumerable<string> words;

        /// <summary>
        /// Raised when grammar is recognised
        /// </summary>
        public event EventHandler OnGrammarRecognized;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="semantic">Semantic name</param>
        /// <param name="words">Word list</param>
        public SpeechGrammarEvent(string semantic, IEnumerable<string> words)
        {
            if (semantic == null)
                throw new ArgumentNullException("semantic");
            if (words == null)
                throw new ArgumentNullException("words");

            this.semantic = semantic;
            this.words = words;
        }

        /// <summary>
        /// Single word constructor version
        /// </summary>
        /// <param name="semantic">Semantic name</param>
        /// <param name="word">Word to recognize</param>
        public SpeechGrammarEvent(string semantic, string word)
            : this(semantic, new string[] { word })
        {
        }

        ///<see cref="KGP.ISpeechGrammarElement.Semantic"/>
        public string Semantic
        {
            get { return this.semantic; }
        }

        ///<see cref="KGP.ISpeechGrammarElement.WordList"/>
        public IEnumerable<string> WordList
        {
            get { return this.words; }
        }

        ///<see cref="KGP.ISpeechGrammarElement.Recognized"/>
        public void Recognized(double confidence)
        {
            if (this.OnGrammarRecognized != null)
            {
                this.OnGrammarRecognized(this, new EventArgs());
            }
        }
    }
}
