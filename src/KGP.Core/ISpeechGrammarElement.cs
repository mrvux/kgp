using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Interface to provide speech grammar to speech recognizer
    /// </summary>
    public interface ISpeechGrammarElement
    {
        /// <summary>
        /// Semantic name
        /// </summary>
        string Semantic { get; }

        /// <summary>
        /// Word list
        /// </summary>
        IEnumerable<string> WordList { get; }

        /// <summary>
        /// Run callback when one word is recognized by the speech engine
        /// </summary>
        /// <param name="confidence">Confidence level</param>
        void Recognized(double confidence);
    }
}
