using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TCPFurhatComm
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            //CONNECT TO FURHAT USING THE ROBOT'S IP PORT
            FurhatInterface furhat = new FurhatInterface("130.237.67.172");

            //CHANGE TO DEFAULT TEXTURE AND VOICE
            furhat.FaceTexture(FACETEXTURES.DEFAULT);
            furhat.ChangeVoice(VOICES.EN_GB_CEREPROC_WILLIAM);

            ////SAY DEMONSTRATION
            furhat.Say("Welcome to the Furhat C Sharp interface example. Press enter to continue.");
            Console.WriteLine("Change face and voice to default and perform simple say Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            ////GESTURES DEMONSTRATION
            furhat.Gesture(GESTURES.SMILE_BIG);
            furhat.Say($"Here  you can perform gestures in the middle {InText.Gesture(GESTURES.ANGER)} of the text  using three different alternatives |gesture(Surprise)|");
            Console.WriteLine("Gestures Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            ////GAZE DEMONSTRATION
            furhat.Gaze(-1, 0, 2);
            furhat.Say($"I can also gaze outside and in the middle {InText.Gaze(1.5f, 0, 2)} of the text |gaze(0,0,2)|");
            Console.WriteLine("Gaze Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            ////FACE TEXTURE DEMONSTRATION
            furhat.FaceTexture(FACETEXTURES.IROBOT);
            furhat.Say($"I can look less like a person, |facetexture(avatar)| like a creature  or more like a {InText.FaceTexture(FACETEXTURES.MALE)} person ");
            Console.WriteLine("Face Texture Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            //VOICE CHANGE DEMONSTRATION
            furhat.ChangeVoice(VOICES.SV_SE_AMAZON_ASTRID);
            Thread.Sleep(750);
            furhat.Say("Jag kan pratar Svenska.");
            furhat.ChangeVoice(VOICES.EN_GB_CEREPROC_WILLIAM);
            furhat.Say(" Or I can return to my native language.");
            Console.WriteLine("Voice Change Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            //SAPI TAGS DEMONSTRATION
            furhat.Say(InText.SAPIRate("Hey there I speak very very very quickly!", SAPI.RateValues.xfast));
            furhat.Say(InText.SAPIRate("Or I can speak very slowly!", SAPI.RateValues.xslow));
            furhat.Say(InText.SAPIVolume("With a soft voice!", SAPI.VolumeValues.xsoft));
            furhat.Say(InText.SAPIVolume("Or a loud voice!", SAPI.VolumeValues.xloud));
            furhat.Say(InText.SAPIPitch("With a low pitch!", SAPI.PitchValues.xlow));
            furhat.Say(InText.SAPIPitch("With a high pitch!", SAPI.PitchValues.xhigh));
            furhat.Say("Finally, I can " + InText.SAPIEmphasis("emphasize", SAPI.EmphasisValues.strong) + " words!");
            furhat.Say("And pause " + InText.SAPIBreak(SAPI.BreakValues.xstrong) + " when I am speaking");
            Console.WriteLine("SAPI Demonstration. [Press Enter To Continue]");
            Console.ReadLine();

            //SPEECH RECOGNITION DEMONSTRATION (BETA)
            Console.Write("Speech Recognition Demonstration.");
            furhat.SayBlock("Can you tell me your name. Without saying my name is"); //Say block must be used so that listen is executed synchrounously
            Console.Write(" [Speak your Name!] ");
            furhat.StartListening();
            while (furhat.RecognizedString == null) Thread.Sleep(50);
            furhat.Say("It is very nice to meet you " + furhat.RecognizedString);
            Console.WriteLine("[Press Enter To Finish Example]");
            Console.ReadLine();

            Environment.Exit(0);
        }

    }

}