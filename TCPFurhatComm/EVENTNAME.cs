using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    internal static class EVENTNAME
    {
        public static class ACTION
        {
            /// <summary>
            /// An action that can be sent to a synthesizer to add an utterance to the speech queue
            /// <para> [text] (string) - The text to speak </para>
            /// <para> [start] (int) - The number of milliseconds into the utterance that the synthesis should start </para> 
            /// <para> [audio] (string) - An audio file to play instead of synthesizing  </para>
            /// <para> [display] (string) - A nicely formatted text representation for display purposes  </para>
            /// <para> [agent] (string) - The ID of the agent that is associated with this synthesis. If this is omitted, all synthesizers should start  </para>
            /// <para> [ifsilent] (bool) - Only add utterance if the system is silent. (Default is false)  </para>
            /// <para> [abort] (bool) - Whether to abort the current speech queue. (Default is false, i.e., append the utterance).  </para>
            /// <para> [monitorWords] (bool) - Whether to send monitor.speech.word events before each word (Default is false) </para>
            /// </summary>
            public static string SPEECH = "furhatos.event.actions.ActionSpeech";

            /// <summary>
            /// An action that can be sent to a synthesizer to stop speaking (and clearing the speech queue)
            /// <para> [action] (string) - The ID of the action.speech event that started the speech synthesis. If this is omitted, all synthesizers should stop </para>
            /// </summary>
            public static string SPEECH_STOP = "furhatos.event.actions.ActionSpeechStop";

            /// <summary>
            /// An action that can be sent to a synthesizer to stop speaking (and clearing the speech queue)
            /// <para> [name] (string) - The name of the voice </para>
            /// <para> [lang] (string) - The language code for the voice (such as "en-US") </para>
            /// <para> [gender] (string) - The gender of the voice ("male" or "female") </para>
            /// <para> [agent] (string) - The ID of the agent that is associated with this synthesis </para>
            /// </summary>
            public static string VOICE = "furhatos.event.actions.ActionConfigVoice";

            /// <summary>
            /// An action that can be sent to a recognizer to make it start listening.
            /// <para> [context] (int) - A name filter with the contexts to use. If omitted, the default context is used (as set with action.context.default). </para>
            /// <para> [endSilTimeout] (int) - The silence timeout (in msec) to detect end-of-speech </para>
            /// <para> [noSpeechTimeout] (int) - The silence timout (in msec) if no speech is detected </para>
            /// <para> [maxSpeechTimeout] (int) - The maximum length of the speech (in msec) </para>
            /// <para> [nbest] (int) - The maximum number of hypotheses to generate </para>
            /// </summary>
            public static string LISTEN = "furhatos.event.actions.ActionListen";

            /// <summary>
            /// An action that can be sent to a recognizer to make it start listening.
            /// <para> [action] (int) - The ID of the action.listen event that started the speech recognizer. If this is omitted, all recognizers should stop </para>
            /// </summary>
            public static string LISTEN_STOP = "furhatos.event.actions.ListenStop";


            /// <summary>
            /// Makes the agent perform a specific gesture.
            /// <para> [name] (string) - The name of the gesture </para>
            /// </summary>
            public static string GESTURE = "furhatos.event.actions.ActionGesture";

            /// <summary>
            /// Makes the agent shift gaze to a certain location in 3D space
            /// <para> [location] (Location) - The 3D location where the agent should gaze </para>
            /// <para> [mode] (string) - Can be "default", "eyes" or "headpose" </para>
            /// <para> [agent] (string) - The ID of the agent </para>
            /// <para> [speed] (string) - How fast the head should move </para>
            /// </summary>
            public static string GAZE = "furhatos.event.actions.ActionGaze";

            /// <summary>
            /// Makes the agent change the texture of the face.
            /// <para> [texture] (string) - The name of the texture </para>
            /// </summary>
            public static string FACE_TEXTURE = "furhatos.event.actions.ActionConfigFace";


        }

        public static class MONITOR
        {
            public static class SPEECH
            {
                /// <summary>
                /// Monitors that a speech synthesizer has started an utterance
                /// </summary>
                public static string START = "furhatos.event.monitors.MonitorSpeechStart";

                /// <summary>
                /// Monitors that a speech synthesizer has started an utterance
                /// </summary>
                public static string END = "furhatos.event.monitors.MonitorSpeechEnd";


                /// <summary>
                /// Reported by the synthesizer just before a mark is being reached in the utterance (part of the SSML specification).
                /// </summary>
                public static string MARK = "furhatos.event.monitors.MonitorSpeechMark";

                /// <summary>
                /// Reported by the synthesizer just before each word is being spoken (if monitorWords is set to true in the action.speech event).
                /// </summary>
                public static string WORD = "furhatos.event.monitors.MonitorSpeechWord";
            }

        }

        public static class SENSE
        {
            public static class SPEECH
            {
                /// <summary>
                /// Once the recognition is done, one event that matches sense.speech.rec** is reported. See the different variants below. If speech recognition is successful, sense.speech.rec will be generated(without suffix). Note that this includes cases where no grammar matched the recogniton, in which case the "text" parameter will be set to NOMATCH".
                /// </summary>
                public static string REC = "furhatos.event.senses.SenseSpeech";

            }
        }
    }
}
