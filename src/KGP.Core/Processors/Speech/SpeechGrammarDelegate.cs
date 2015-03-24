using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Delegate version of speech grammar, runs an anonymous action when recognized
    /// </summary>
    public class SpeechGrammarDelegate : ISpeechGrammarElement
    {
        private Action executeAction;
        private string semantic;
        private IEnumerable<string> words;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="executeAction">Action to execute when semantic is recognized</param>
        /// <param name="semantic">Semantic name</param>
        /// <param name="words">Word list</param>
        public SpeechGrammarDelegate(Action executeAction, string semantic, IEnumerable<string> words)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");
            if (semantic == null)
                throw new ArgumentNullException("semantic");
            if (words == null)
                throw new ArgumentNullException("words");

            this.executeAction = executeAction;
            this.semantic = semantic;
            this.words = words;
        }

        /// <summary>
        /// Single word constructor version
        /// </summary>
        /// <param name="executeAction">Action to execute when semantic is recognized</param>
        /// <param name="semantic">Semantic name</param>
        /// <param name="word">Word to recognize</param>
        public SpeechGrammarDelegate(Action executeAction, string semantic, string word) : this(executeAction, semantic, new string[] { word })
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
            executeAction();
        }
    }
}
