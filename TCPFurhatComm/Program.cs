﻿using System;
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
            furhat.ConnectSkill("DotNet Example Skill");

            //CHANGE TO DEFAULT TEXTURE AND VOICE
            furhat.FaceTexture(FACETEXTURES.DEFAULT);
            furhat.ChangeVoice(VOICES.EN_US_AMAZON_JOEY);
            furhat.ChangeLed(0, 0, 0);
            furhat.EnableMicroexpressions(false);

            //SAY DEMONSTRATION
            Console.WriteLine("Say Demonstration\n[Press Enter To Continue]");
            furhat.Say("Welcome to the Furhat C Sharp interface example. Press enter to continue.");
            Console.ReadLine();
            Console.Clear();

            //GESTURES and LED DEMONSTRATION
            Console.WriteLine("Gestures and LED Demonstration.\n[Press Enter To Continue]");
            furhat.ChangeLed(100, 100, 100);
            furhat.Gesture(GESTURES.SMILE_BIG);
            furhat.Say($"Here you can use LED's and perform gestures in the middle {InText.Gesture(GESTURES.ANGER)} {InText.Led(255, 0, 0)} of the text using three different alternatives |gesture(Surprise)| |led(0,0,0)|");
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
            furhat.Say($"I can look less like a person, |facetexture(avatar)| like a creature  or more like a {InText.FaceTexture(FACETEXTURES.MALE)} person ");
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
            furhat.StartListening();
            furhat.RecognizedSpeechAction = new Action<string>((s) => furhat.Say(s));
            Console.ReadLine();
            furhat.StopListening();

            Console.Clear();
            Console.Write("User Detection Demonstration. Move in front of the camera or use virtual users!\n[Press Enter To Finish Example]");
            furhat.SensedUsersAction = new Action<List<User>>((users) => printUsers(users));
            Console.ReadLine();

            Environment.Exit(0);
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