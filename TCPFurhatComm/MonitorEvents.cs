using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    internal class MonitorEvents
    {
        internal class MonitorSpeechStart : GeneralEvent
        {

            /// <summary>
            /// The ID of the action.speech event that started the speech synthesis
            /// </summary>
            public string action { get; set; }

            /// <summary>
            /// Taken from action.speech
            /// </summary>
            public string text { get; set; }

            /// <summary>
            /// Taken from action.speech
            /// </summary>
            public int start { get; set; }

            /// <summary>
            /// The length of the utterance (in msec)
            /// </summary>
            public int length { get; set; }


            public MonitorSpeechStart(/*string action, string text, int start, string agent, int length, int prominence*/) : base(EVENTNAME.MONITOR.SPEECH.START)
            {
                //this.action = action;
                //this.text = text;
                //this.start = start;
                //this.agent = agent;
                //this.length = length;
                //this.prominence = prominence;
            }
        }

        internal class MonitorSpeechEnd : GeneralEvent
        {

            /// <summary>
            /// The ID of the action.speech event that started the speech synthesis
            /// </summary>
            public string action { get; set; }

            /// <summary>
            /// If the speech synthesis is stopped prematurely, this parameter reports the position in the utterance (in msec) where it stopped
            /// </summary>
            public int stopped { get; set; }


            public MonitorSpeechEnd(/*string action, int stopped, string agent*/) : base(EVENTNAME.MONITOR.SPEECH.END)
            {
                //this.action = action;
                //this.stopped = stopped;
                //this.agent = agent;
            }
        }

        internal class MonitorSpeechMark : GeneralEvent
        {
            /// <summary>
            /// The ID of the action.speech event that started the speech synthesis
            /// </summary>
            public string action { get; set; }

            /// <summary>
            /// The name of the mark
            /// </summary>
            public string name { get; set; }

            public MonitorSpeechMark(string action, string name) : base(EVENTNAME.MONITOR.SPEECH.MARK)
            {
                this.action = action;
                this.name = name;
            }
        }

        internal class MonitorSpeechWord : GeneralEvent
        {
            /// <summary>
            /// The ID of the action.speech event that started the speech synthesis
            /// </summary>
            public string action { get; set; }

            /// <summary>
            /// The word that is about to be spoken
            /// </summary>
            public string word { get; set; }

            /// <summary>
            /// The position of the word in the utterance (0 being the first word)
            /// </summary>
            public int position { get; set; }

            public MonitorSpeechWord(/*string action, string word, int position*/) : base(EVENTNAME.MONITOR.SPEECH.WORD)
            {
                //this.action = action;
                //this.word = word;
                //this.position = position;
            }
        }

    }
}
