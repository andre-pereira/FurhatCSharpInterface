using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    public static class GESTURES
    {
        /// <summary>
        /// identifier to be used inside the say function
        /// </summary>
        public const string Identifier = "gesture";


            public static string EYES_BLINK = "Blink";
            public static string EYES_CLOSE = "CloseEyes";
            public static string EYES_OPEN = "OpenEyes";
            public static string EYES_WINK = "OpenEyes";
            public static string EYES_LOOKAWAY = "GazeAway";

            public static string NECK_NOD = "Nod";
            public static string NECK_ROLL = "Roll";
            public static string NECK_SHAKE = "Shake";

            public static string OH = "Oh";

            public static string SMILE_BIG = "BigSmile";
            public static string SMILE = "Smile";

            public static string BROW_RAISE = "BrowRaise";
            public static string BROW_FROWN = "BrowFrown";

            public static string ANGER = "ExpressAnger";
            public static string DISGUST = "ExpressDisgust";
            public static string FEAR = "ExpressFear";
            public static string SAD = "ExpressSad";
            public static string SURPRISE = "Surprise";
            public static string THOUGHTFUL = "Thoughtful";
    }
}
