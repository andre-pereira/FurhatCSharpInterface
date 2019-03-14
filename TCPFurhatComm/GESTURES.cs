using Newtonsoft.Json;
using System.Collections.Generic;

namespace TCPFurhatComm
{
    public enum PARAMS
    {
        EXPR_ANGER, EXPR_DISGUST, EXPR_FEAR, EXPR_SAD,
        SMILE_CLOSED, SMILE_OPEN,
        SURPRISE,
        BLINK_LEFT, BLINK_RIGHT,
        BROW_DOWN_LEFT, BROW_DOWN_RIGHT, BROW_IN_LEFT, BROW_IN_RIGHT, BROW_UP_LEFT, BROW_UP_RIGHT,
        EARS_OUT,
        EPICANTHIC_FOLD,
        EYE_SQUINT_LEFT, EYE_SQUINT_RIGHT, LOOK_DOWN, LOOK_LEFT, LOOK_RIGHT, LOOK_UP,
        PHONE_AAH, PHONE_B_M_P, PHONE_BIGAAH, PHONE_CH_J_SH, PHONE_D_S_T, PHONE_EE, PHONE_EH, PHONE_F_V, PHONE_I, PHONE_K, PHONE_N, PHONE_OH, PHONE_OOH_Q, PHONE_R, PHONE_TH, PHONE_W,
        LOOK_DOWN_LEFT, LOOK_DOWN_RIGHT, LOOK_LEFT_LEFT, LOOK_LEFT_RIGHT, LOOK_RIGHT_LEFT, LOOK_RIGHT_RIGHT, LOOK_UP_LEFT, LOOK_UP_RIGHT,
        NECK_TILT, NECK_PAN, NECK_ROLL,
        GAZE_PAN, GAZE_TILT
    }

    internal class Gesture
    {
        public List<Frame> frames { get; set; }
        public string name { get; set; }

        public Gesture(string name, List<Frame> frames)
        {
            this.frames = frames;
            this.name = name;
        }
    }

    public class KeyFramedGesture
    {
        public List<float> keyFrameTimes;
        public List<keyFramedPARAM> frames { get; set; }
        public string name { get; set; }

        public KeyFramedGesture(string name, List<float> keyFrameTimes, List<keyFramedPARAM> keyFrameParams)
        {
            this.keyFrameTimes = keyFrameTimes;
            this.frames = keyFrameParams;
            this.name = name;
        }
    }

    internal class Frame
    {
        public bool persist { get; set; }
        public List<float> time { get; set; }

        [JsonProperty(PropertyName = "params")]
        public Dictionary<string, dynamic> parameters { get; set; }

        public Frame(bool persist, List<float> time, Dictionary<string, dynamic> parameters)
        {
            this.persist = persist;
            this.time = time;
            this.parameters = parameters;
        }
    }

    public class keyFramedPARAM
    {
        public PARAMS param;
        public List<float> keyFrameValues;

        public keyFramedPARAM(PARAMS param, List<float> keyframeValues)
        {
            this.param = param;
            this.keyFrameValues = keyframeValues;
        }
    }

    public static class GESTURES
    {
        /// <summary>
        /// identifier to be used inside the say function
        /// </summary>
        public const string Identifier = "gesture";

        public static string EYES_BLINK = "Blink";
        public static string EYES_CLOSE = "CloseEyes";
        public static string EYES_OPEN = "OpenEyes";
        public static string EYES_WINK = "Wink";
        public static string EYES_LOOKAWAY = "GazeAway";

        public static string NECK_NOD = "Nod";
        public static string NECK_ROLL = "Roll";
        public static string NECK_SHAKE = "Shake";

        public static string OH = "Oh";

        public static string SMILE_BIG = "BigSmile";
        public static string SMILE = "Smile";

        public static string BROW_RAISE = "BrowRaise";
        public static string BROW_FROWN = "BrowFrown";

        public static string ANGER = "Anger";
        public static string DISGUST = "Disgust";
        public static string FEAR = "Fear";
        public static string SAD = "Sad";
        public static string SURPRISE = "Surprise";
        public static string THOUGHTFUL = "Thoughtful";


        public static KeyFramedGesture STARSTRUCK = new KeyFramedGesture(
        name: "Starstruck",
        keyFrameTimes: new List<float> { 0.16f, 0.32f, 0.48f, 0.64f, 0.70f, 0.96f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> { 1,0,1,0,1,0}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> { 1,0,1,0,1,0}),
                    new keyFramedPARAM(PARAMS.PHONE_W, new List<float> { 0.6f,0.6f,0.45f,0.3f,0.15f,0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0.6f, 0.6f,0.6f, 0.6f, 0,0}),
                    new keyFramedPARAM(PARAMS.NECK_TILT, new List<float> { -3,-3,0,0,0,0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> { 1,1,1,1,0,0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> { 1,1,1,1,0,0})
        });

        public static KeyFramedGesture SHY = new KeyFramedGesture(
        name: "Shy",
        keyFrameTimes: new List<float> { 0.32f, 0.64f, 0.84f, 0.96f, 1.16f, 1.28f},
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.NECK_TILT, new List<float> { 20,10,10,5,5,0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0f,0f,0f,0.4f,0.4f,0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> { 1,1,1f,0,0,0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> { 1,1,1f,0,0,0}),
                    new keyFramedPARAM(PARAMS.LOOK_DOWN, new List<float> { 0f,0f,0.7f,0.7f, 0.7f, 0}),
                    new keyFramedPARAM(PARAMS.LOOK_LEFT, new List<float> { 0.8f,0.8f,0.8f,0.8f,0,0})
        });


        public static KeyFramedGesture BIGWINK = new KeyFramedGesture(
        name: "BigWink",
        keyFrameTimes: new List<float> { 0.32f, 0.64f},
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EPICANTHIC_FOLD, new List<float> { 0.3f,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> { 3,0}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> { 0.6f,0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> { 1,0})
        });

        public static KeyFramedGesture FLIRTY = new KeyFramedGesture(
        name: "Flirty",
        keyFrameTimes: new List<float> { 0.32f, 0.64f},
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.NECK_TILT, new List<float> { -3f,0}),
                    new keyFramedPARAM(PARAMS.PHONE_W, new List<float> { 0.4f,0}),
                    new keyFramedPARAM(PARAMS.EPICANTHIC_FOLD, new List<float> { 0.3f,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> { 3,0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> { 1,0}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> { 0.3f,0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0.4f,0})
        });

        public static KeyFramedGesture SURPRISEHAPPY = new KeyFramedGesture(
        name: "SurpriseHappy",
        keyFrameTimes: new List<float> { 0.32f, 0.72f, 0.92f, 1.72f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0,0.9f,0.9f,0}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> { 0,0.3f,0.3f,0}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> { 0.8f,0.4f,0.3f,0})
        });

        public static KeyFramedGesture EMBARRASSED = new KeyFramedGesture(
        name: "Embarrassed",
        keyFrameTimes: new List<float> { 0.32f, 1.22f, 1.54f},
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0.55f,0.55f,0}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> { 0.45f,0.45f,0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> { 0.45f,0.45f,0f})
        });

        public static KeyFramedGesture CONFUSED = new KeyFramedGesture(
        name: "Confused",
        keyFrameTimes: new List<float> { 0.32f, 1.02f, 1.42f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> { 0.2f,0.2f,0}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> { 0.3f,0.3f,0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> { 1,1,0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> { 1,1,0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> { 1,1,0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> { 1,1,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> { 0.6f,0.6f,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> { 0.6f,0.6f,0}),
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> { 0.1f,0.1f,0}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> { 0,0,0})
        });

        public static KeyFramedGesture SKEPTICAL = new KeyFramedGesture(
        name: "Skeptical",
        keyFrameTimes: new List<float> { 0.32f, 0.96f, 1.28f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> { 1,1,0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> { 0.5f,0.5f,0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> { 0.8f,0.8f,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> { 0.2f,0.2f,0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> { 0.4f,0.4f,0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> { 0.4f,0.4f,0})
        });

        public static KeyFramedGesture EXCITED = new KeyFramedGesture(
        name: "Excited",
        keyFrameTimes: new List<float> { 0.32f, 0.4f, 0.6f, 0.8f, 0.96f, 1.2f, 1.36f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0.4f,0.5f,0.5f,0.5f,0.5f,0,0}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0.5f,0.6f,0.6f,0.6f,0.6f,0,0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {1,1,1,1,1,0f,0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {1,1,1,1,1,0f,0}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0.7f,0.7f,0.7f,0.7f,0f,0f,0})
        });


        public static KeyFramedGesture MOOD_NEUTRAL = new KeyFramedGesture(
        name: "MoodNeutral",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0})
        });

        public static KeyFramedGesture MOOD_CONFUSED = new KeyFramedGesture(
        name: "MoodConfused",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0.1f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0.3f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0.2f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {1}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {1}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {1}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {1}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0.6f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0.6f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0})
        });

        public static KeyFramedGesture MOOD_HAPPY = new KeyFramedGesture(
        name: "MoodHappy",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0.5f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0.5f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0.5f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0.7f})
        });

        public static KeyFramedGesture MOOD_FEAR = new KeyFramedGesture(
        name: "MoodFear",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_SAD = new KeyFramedGesture(
        name: "MoodSad",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_BORED = new KeyFramedGesture(
        name: "MoodBored",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0.45f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0.45f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_EXCITED = new KeyFramedGesture(
        name: "MoodExcited",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0.7f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0.5f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0.6f})
        });

        public static KeyFramedGesture MOOD_CONCERNED = new KeyFramedGesture(
        name: "MoodConcerned",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0.3f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0.3f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_SKEPTICAL = new KeyFramedGesture(
        name: "MoodSkeptical",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0.8f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0.5f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0.4f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0.4f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0.2f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_SMUG = new KeyFramedGesture(
        name: "MoodSmug",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0.75f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0.4f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0.4f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        public static KeyFramedGesture MOOD_FROWN = new KeyFramedGesture(
        name: "MoodFrown",
        keyFrameTimes: new List<float> { 0.32f },
        keyFrameParams: new List<keyFramedPARAM> {
                    new keyFramedPARAM(PARAMS.EXPR_ANGER, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_DISGUST, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_FEAR, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EXPR_SAD, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_DOWN_LEFT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_UP_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_RIGHT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BROW_IN_LEFT, new List<float> {1f}),
                    new keyFramedPARAM(PARAMS.BLINK_LEFT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.BLINK_RIGHT, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SURPRISE, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.SMILE_CLOSED, new List<float> {0f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_LEFT, new List<float> {0.6f}),
                    new keyFramedPARAM(PARAMS.EYE_SQUINT_RIGHT, new List<float> {0.6f}),
                    new keyFramedPARAM(PARAMS.SMILE_OPEN, new List<float> {0f})
        });

        internal static Dictionary<string, KeyFramedGesture> customGestures = new Dictionary<string, KeyFramedGesture>() {
            { STARSTRUCK.name, STARSTRUCK },
            { SHY.name, SHY },
            { BIGWINK.name, BIGWINK},
            { FLIRTY.name, FLIRTY},
            { SURPRISEHAPPY.name, SURPRISEHAPPY},
            { EMBARRASSED.name, EMBARRASSED},
            { CONFUSED.name, CONFUSED },
            { SKEPTICAL.name, SKEPTICAL },
            { EXCITED.name, EXCITED },
            { MOOD_NEUTRAL.name, MOOD_NEUTRAL },
            { MOOD_HAPPY.name, MOOD_HAPPY },
            { MOOD_EXCITED.name, MOOD_EXCITED },
            { MOOD_BORED.name, MOOD_BORED },
            { MOOD_CONCERNED.name, MOOD_CONCERNED },
            { MOOD_SKEPTICAL.name, MOOD_SKEPTICAL },
            { MOOD_CONFUSED.name, MOOD_CONFUSED },
            { MOOD_FEAR.name, MOOD_FEAR },
            { MOOD_SAD.name, MOOD_SAD },
            { MOOD_FROWN.name, MOOD_FROWN },
            { MOOD_SMUG.name, MOOD_SMUG }};
    }
}
