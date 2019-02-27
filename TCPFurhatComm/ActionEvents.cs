using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    internal class ActionEvents
    {

        /// <summary>
        /// An action that can be sent to add an utterance to the speech queue
        /// </summary>
        public class Speak : GeneralEvent
        {
            public string text { get; set; }
            public bool monitorWords { get; set; }
            /// <param name="text"> The text to speak </param>
            /// <param name="monitorWords"> Set as true if you want to receive monitor.speech.word events </param>
            public Speak(string text, bool monitorWords) : base(EVENTNAME.ACTION.SPEECH)
            {
                this.text = text;
                this.monitorWords = monitorWords;
            }
        }


        /// <summary>
        /// An action that can be sent to a synthesizer to stop speaking (and clearing the speech queue)
        /// </summary>
        public class StopSpeech : GeneralEvent
        {
            public StopSpeech() : base(EVENTNAME.ACTION.SPEECH_STOP)
            {
            }
        }

        /// <summary>
        /// An action that can be sent to a synthesizer to change the voice by name.
        /// </summary>
        public class ChangeVoiceByName : GeneralEvent
        {
            public bool notifyInterface { get; set; }
            public bool enabled { get; set; }
            public string name { get; set; }
            /// <param name="name"> The name of the voice </param>
            public ChangeVoiceByName(string name) : base(EVENTNAME.ACTION.VOICE)
            {
                this.notifyInterface = false;
                this.enabled = true;
                this.name = name;
            }
        }

        /// <summary>
        /// An action that can be sent to a recognizer to make it start listening.
        /// </summary>
        public class StartListening : GeneralEvent
        {
            public int endSilTimeout { get; set; }
            public int noSpeechTimeout { get; set; }
            public int maxSpeechTimeout { get; set; }
            public int nbest { get; set; }
            //public string context { get; set; }
            public bool interimResults { get; set; }

        /// <param name="endSilTimeout"> The silence timeout (in msec) to detect end-of-speech </param>
        /// <param name="noSpeechTimeout"> The silence timout (in msec) if no speech is detected </param>
        /// <param name="nbest"> The maximum number of hypotheses to generate </param>
        public StartListening(int endSilTimeout, int noSpeechTimeout, int nbest, int maxSpeechTimeout):base(EVENTNAME.ACTION.LISTEN)
            {
                //this.context = "default";
                this.endSilTimeout = endSilTimeout;
                this.noSpeechTimeout = noSpeechTimeout;
                this.maxSpeechTimeout = maxSpeechTimeout;
                this.nbest = nbest;
                this.interimResults = false;
            }
        }

        /// <summary>
        /// An action that can be sent to a recognizer to make it stop listening.
        /// </summary>
        public class StopListening : GeneralEvent
        {
            public StopListening() : base(EVENTNAME.ACTION.LISTEN_STOP)
            {

            }
        }

        /// <summary>
        /// Makes the agent perform a specific gesture.
        /// </summary>
        public class PerformGesture : GeneralEvent
        {
            public string name { get; set; }

            /// <param name="name"> The name of the gesture </param>
            public PerformGesture(string name):base(EVENTNAME.ACTION.GESTURE)
            {
                this.name = name;
            }
        }

        /// <summary>
        /// Makes the agent shift gaze to a certain location in 3D space
        /// </summary>
        public class PerformGaze : GeneralEvent
        {
            public Location location { get; set; }
            public string mode { get; set; }

            /// <param name="location"> The 3D location where the agent should gaze </param>
            /// <param name="mode"> Can be "default", "eyes" or "headpose" </param>
            public PerformGaze(Location location, string mode) : base(EVENTNAME.ACTION.GAZE)
            {
                this.location = location;
                this.mode = mode;
            }
        }

        /// <summary>
        /// Makes the agent change the texture of the face.
        /// </summary>
        public class ChangeFaceTexture : GeneralEvent
        {
            public string texture { get; set; }

            /// <param name="texture"> The name of the texture </param>
            public ChangeFaceTexture(string texture) : base(EVENTNAME.ACTION.FACE_TEXTURE)
            {
                this.texture = texture;
            }
        }


    }
}
