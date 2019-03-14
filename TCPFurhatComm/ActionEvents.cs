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
            public bool interimResults { get; set; }

        /// <param name="endSilTimeout"> The silence timeout (in msec) to detect end-of-speech </param>
        /// <param name="noSpeechTimeout"> The silence timout (in msec) if no speech is detected </param>
        /// <param name="nbest"> The maximum number of hypotheses to generate </param>
        public StartListening(int endSilTimeout, int noSpeechTimeout, int nbest, int maxSpeechTimeout, bool interimResults) :base(EVENTNAME.ACTION.LISTEN)
            {
                //this.context = "default";
                this.endSilTimeout = endSilTimeout;
                this.noSpeechTimeout = noSpeechTimeout;
                this.maxSpeechTimeout = maxSpeechTimeout;
                this.nbest = nbest;
                this.interimResults = interimResults;
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
            public int priority { get; set; }

            /// <param name="name"> The name of the gesture </param>
            public PerformGesture(string name, int priority):base(EVENTNAME.ACTION.GESTURE)
            {
                this.name = name;
                this.priority = priority;
            }
        }


        /// <summary>
        /// Makes the agent perform a specific gesture.
        /// </summary>
        public class PerformCustomGesture : GeneralEvent
        {
            public Gesture gesture { get; set; }
            public int priority { get; set; }

            /// <param name="name"> The name of the gesture </param>
            public PerformCustomGesture(Gesture gesture, int priority) : base(EVENTNAME.ACTION.GESTURE)
            {
                this.gesture = gesture;
                this.priority = priority;
            }
        }

        public class SkillConnect : GeneralEvent
        {
            public string name { get; set; }

            public SkillConnect(string name) : base(EVENTNAME.ACTION.SKILL_CONNECT)
            {
                this.name = name;
            }
        }

        public class Attend : GeneralEvent
        {
            public string target { get; set; }
            public int mode { get; set; }
            public int speed { get; set; }

            public Attend(string target, int mode = 0, int speed = 2) : base(EVENTNAME.ACTION.ATTEND)
            {
                this.target = target;
                this.mode = mode;
                this.speed = speed;
            }
        }

        public class AttendWithRoll : GeneralEvent
        {
            public string target { get; set; }
            public int mode { get; set; }
            public int speed { get; set; }
            public double roll { get; set; }

            public AttendWithRoll(string target, int mode = 0, int speed = 2, double roll = 0) : base(EVENTNAME.ACTION.ATTEND)
            {
                this.target = target;
                this.mode = mode;
                this.speed = speed;
                this.roll = roll;
            }
        }

        /// <summary>
        /// Makes the agent shift gaze to a certain location in 3D space
        /// </summary>
        public class PerformGaze : GeneralEvent
        {
            public Location location { get; set; }
            public int mode { get; set; }
            public int speed { get; set; }
            //public bool calculateSpline { get; set; }

            /// <param name="location"> The 3D location where the agent should gaze </param>
            /// <param name="mode"> Can be "default", "eyes" or "headpose" </param>
            public PerformGaze(Location location, int mode, int speed/*, bool calculateSpline*/) : base(EVENTNAME.ACTION.GAZE)
            {
                this.location = location;
                this.mode = mode;
                this.speed = speed;
                //this.calculateSpline = calculateSpline;
            }
        }


        /// <summary>
        /// Makes the agent shift gaze to a certain location in 3D space
        /// </summary>
        public class PerformGazeWithRoll : GeneralEvent
        {
            public Location location { get; set; }
            public int mode { get; set; }
            public int speed { get; set; }
            public float roll { get; set; }
            //public bool calculateSpline { get; set; }

            /// <param name="location"> The 3D location where the agent should gaze </param>
            /// <param name="mode"> Can be "default", "eyes" or "headpose" </param>
            public PerformGazeWithRoll(Location location, int mode, int speed, float roll/*, bool calculateSpline*/) : base(EVENTNAME.ACTION.GAZE)
            {
                this.location = location;
                this.mode = mode;
                this.speed = speed;
                this.roll = roll;
                //this.calculateSpline = calculateSpline;
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

        /// <summary>
        /// Changes the led color of the robot.
        /// </summary>
        public class ChangeLedSolidColor : GeneralEvent
        {
            public int red { get; set; }
            public int green { get; set; }
            public int blue { get; set; }

            /// <param name="texture"> The name of the texture </param>
            public ChangeLedSolidColor(int red, int green, int blue) : base(EVENTNAME.ACTION.LED_SOLID)
            {
                this.red = red;
                this.green = green;
                this.blue = blue;
            }
        }


    }

}
