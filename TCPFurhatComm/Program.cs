using System;
using System.Collections.Generic;
using System.Threading;

namespace TCPFurhatComm
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            //CONNECT TO FURHAT USING THE ROBOT'S IP PORT
            FurhatInterface furhat = new FurhatInterface("130.237.67.215", nameForSkill: "CSharp Example", filePathDialogActs: "../DialogActs.tsv");

            //CREATE A KEYVALUEPAIR DICTIONARY TO BE ABLE TO DO VARIABLE REPLACEMENT IN SAY COMMANDS
            Dictionary<string, string> vars = new Dictionary<string, string>() {
                { "userName", "human" }, { "robotName", "Furhat" } };

            //CHANGE TO DEFAULT SLEEP SETTINGS
            furhat.EnableMicroexpressions(false);
            furhat.FaceTexture(FACETEXTURES.DEFAULT);
            furhat.ChangeVoice(VOICES.EN_US_AMAZON_JOEY);
            furhat.ChangeLed(0, 0, 0);
            furhat.Gesture(GESTURES.EYES_CLOSE);


            Console.WriteLine("[Press Enter To Start Demonstration]");
            Console.ReadLine();
            Console.Clear();

            //SAY DEMONSTRATION
            furhat.Gesture(GESTURES.EYES_OPEN);
            furhat.CustomEvent = new Action<string>((s) => Console.WriteLine(s));
            Console.WriteLine("Say Demonstration\n[Press Enter To Continue]");
            furhat.Say("Good that you woke me up.");
            furhat.Say("I was feeling pretty bored.", GESTURES.MOOD_BORED);
            Console.ReadLine();
            Console.Clear();

            //VARIABLE REPLACEMENT
            Console.WriteLine("Variable Replacement Demonstration\n[Press Enter To Continue]");
            furhat.Say("Hello |var(userName)|, my name is |var(robotName)| and I am here to interact with you ", keyValuePairs: vars);
            Console.ReadLine();
            Console.Clear();

            //GESTURES and LED DEMONSTRATION
            Console.WriteLine("Gestures and LED Demonstration.\n[Press Enter To Continue]");
            furhat.ChangeLed(100, 100, 100);
            furhat.Gesture(GESTURES.SMILE_BIG);
            furhat.Say($"Here you can use LED's and gestures {InText.Gesture(GESTURES.ANGER)} {InText.Led(255, 0, 0)} using many different alternatives |gesture(Surprise)| |led(0,0,0)|");
            Console.ReadLine();
            Console.Clear();

            //GAZE DEMONSTRATION
            Console.WriteLine("Gaze Demonstration.\n[Press Enter To Continue]");
            furhat.Gaze(0.2f, -1, 2);
            furhat.ChangeLed(0, 0, 0);
            furhat.Say($"I can also gaze outside and in the middle {InText.Gaze(-0.2f, -1f, 2)} of the text |gaze(0,0,2)|");
            Console.ReadLine();
            Console.Clear();

            //FACE TEXTURE DEMONSTRATION
            Console.WriteLine("Face Texture Demonstration.\n[Press Enter To Continue]");
            furhat.FaceTexture(FACETEXTURES.IROBOT);
            furhat.Say($"I can look less like a person, |facetexture(avatar)| like a creature  or more like a {InText.FaceTexture(FACETEXTURES.MALE)} human ");
            Console.ReadLine();
            Console.Clear();

            //VOICE CHANGE DEMONSTRATION
            Console.WriteLine("Voice Change Demonstration.\n[Press Enter To Continue]");
            furhat.ChangeVoice(VOICES.SV_SE_AMAZON_ASTRID);
            Thread.Sleep(750);
            furhat.Say("Jag kan pratar Svenska.");
            furhat.ChangeVoice(VOICES.EN_GB_CEREPROC_WILLIAM);
            furhat.Say(" Or I can return to my native language.");
            Console.ReadLine();
            Console.Clear();

            //SAPI TAGS DEMONSTRATION
            Console.WriteLine("SAPI Demonstration.\n[Press Enter To Continue]");
            furhat.Say(InText.SAPIRate("Hey there I speak very very very quickly!", SAPI.RateValues.xfast));
            furhat.Say(InText.SAPIRate("Or I can speak very slowly!", SAPI.RateValues.xslow));
            furhat.Say(InText.SAPIVolume("With a soft voice!", SAPI.VolumeValues.xsoft));
            furhat.Say(InText.SAPIVolume("Or a loud voice!", SAPI.VolumeValues.xloud));
            furhat.Say(InText.SAPIPitch("With a low pitch!", SAPI.PitchValues.xlow));
            furhat.Say(InText.SAPIPitch("With a high pitch!", SAPI.PitchValues.xhigh));
            furhat.Say("Finally, I can " + InText.SAPIEmphasis("emphasize", SAPI.EmphasisValues.strong) + " words!");
            furhat.Say("And pause " + InText.SAPIBreak(SAPI.BreakValues.xstrong) + " when I am speaking");
            Console.ReadLine();
            Console.Clear();

            //SPEECH RECOGNITION DEMONSTRATION
            Console.Write("Speech Recognition Demonstration.\n[Press Enter To Continue]");
            furhat.SayBlock("I can recognize and reapeat what you say"); //Say block must be used so that listen is executed synchrounously
            furhat.StartListening(withPartialResults: true);
            furhat.RecognizedPartialSpeechAction = new Action<string>((s) => Console.WriteLine("Recognized Partial String: " + s));
            furhat.RecognizedSpeechAction = new Action<string>((s) => furhat.Say(s));
            Console.ReadLine();
            furhat.StopListening();
            Console.Clear();

            //USER DETECTION DEMONSTRATION
            Console.Write("User Detection Demonstration. Move in front of the camera or use virtual users!\n[Press Enter To Finish Example]");
            furhat.SensedUsersAction = new Action<List<User>>((users) => printUsers(users));
            Console.ReadLine();
            furhat.SensedUsersAction = null;
            Console.Clear();

            //Dialog Acts From Files
            Console.WriteLine("Dialog Act Demonstration\n[Press Enter To Continue]");
            furhat.CustomEvent = new Action<string>((s) => HandleCustomEvents(furhat,s));
            furhat.Say("let me tell you 3 jokes.");
            for (int i = 0; i < 3; i++)
            {
                furhat.sayDialogAct("Jokes", vars);
            }

            Console.ReadLine();

            Environment.Exit(0);
        }

        private static void HandleCustomEvents(FurhatInterface furhat, string str)
        {
            switch (str)
            {
                case "lookLeft":
                    furhat.Gaze(-1, 0, 1);
                    break;
                case "lookCenter":
                    furhat.Gaze(0, 0, 1);
                    break;
                case "lookAtUser":
                    if (furhat.users != null)
                        furhat.Gaze(furhat.users[0].location);
                    break;
                default:
                    break;
            }
            Console.WriteLine("Custom event triggered: " + str);
            
        }

        private static void printUsers(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("User detection Demonstration. Move in front of the camera or use virtual users!");
            foreach (var user in users)
            {
                Console.WriteLine("User: " + user.id);
                Console.WriteLine("Location: x:" + user.location.x + " y:" + user.location.y + " z:" + user.location.z);
                Console.WriteLine("Rotation: x:" + user.rotation.x + " y:" + user.rotation.y + " z:" + user.rotation.z);
                Console.WriteLine("------------------------------------------");
            }
            Console.WriteLine("[Press Enter To Finish Example]");
        }
    }

}