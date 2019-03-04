using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    public class FurhatInterface
    {

        #region Class Variables

        public static CultureInfo culture;

        /// <summary>
        /// TCP client to communicate with the furhat robot or dev-server
        /// </summary>
        Client client;

        public void CloseConnection()
        {
            client.Close();
            //Environment.Exit(0);
        }

        /// <summary>
        /// This Dictionary contains the actions that are triggered when an event arrives
        /// Create this actions by using SubscribeToEvent method and adding a if statement in the ProcessEvents method
        /// </summary>
        private Dictionary<string, Action<string>> SubscribedEvents;

        internal void EnableMicroexpressions(bool enable)
        {
            string hardcodedToDisable = "{\"event_id\":\"0ba36e04-912e-48c5-910e-6658933dfe33\",\"event_name\":\"furhatos.event.actions.ActionMicroexpression\",\"microexpression\":{\"repeats\":[],\"fluctuations\":[],\"name\":\"setDefaultMicroexpression\"},\"event_time\":\"2019-03-03 17:49:44.004\"}";
            //string hardcodedToDisable = "{\"event_name\":\"furhatos.event.actions.ActionMicroexpression\",\"microexpression\":{\"repeats\":[],\"fluctuations\":[],\"name\":\"setDefaultMicroexpression\"}}";
            string hardcodedToEnable = "{\"event_name\":\"furhatos.event.actions.ActionMicroexpression\",\"microexpression\":{\"repeats\":[{\"freqFrom\":200,\"adjustments\":[{\"freqFrom\":-3,\"freqTo\":3,\"paramName\":\"GAZE_PAN\"},{\"freqFrom\":-3,\"freqTo\":3,\"paramName\":\"GAZE_TILT\"}],\"freqTo\":400},{\"freqFrom\":2000,\"adjustments\":[],\"freqTo\":8000,\"gesture\":{\"frames\":[{\"time\":[0.2],\"persist\":false,\"params\":{\"BLINK_RIGHT\":1,\"BLINK_LEFT\":1}},{\"time\":[0.4],\"persist\":false,\"params\":{\"reset\":true}}],\"name\":\"Blink\"}}],\"fluctuations\":[{\"adjust\":0.12,\"freq\":0.025,\"range\":0.06,\"list\":[\"BROW_UP_LEFT\",\"BROW_UP_RIGHT\"]},{\"adjust\":0.5,\"freq\":0.025,\"range\":0.2,\"list\":[\"SMILE_CLOSED\"]},{\"adjust\":0.5,\"freq\":0.025,\"range\":0.2,\"list\":[\"EXPR_SAD\"]}],\"name\":\"setDefaultMicroexpression\"}}";
            if (enable)
                SendHardcodedEvent("furhatos.event.actions.ActionMicroexpression", hardcodedToEnable);
            else
                SendHardcodedEvent("furhatos.event.actions.ActionMicroexpression", hardcodedToDisable);
        }

        ///// <summary>
        ///// This bool is used at the end of speech event to go back to a neutral pose if the say(text,gesture) function was called
        ///// </summary>
        //private bool goBackToNeutral;

        /// <summary>
        /// Contains actions to be executed during the middle of the text if Say contained any.
        /// Item1 (int) contains the position of the word that follows the action
        /// Item2 (string) contains the name of the action that should be performed
        /// Item3 (string) contains the argument associate with that action
        /// </summary>
        private List<Tuple<int,string,string>> MidTextActions;

        /// <summary>
        /// This variable holds a queue of speech that is enqued in the speech.end event
        /// </summary>
        private Queue<Tuple<string,string>> SayQueue;

        /// <summary>
        /// Variable is true whenever furhat is in the middle of speaking
        /// </summary>
        private bool inMiddleOfSpeaking;

        /// <summary>
        /// Automatically updated when receiving word speech events from Furhat
        /// </summary>
        private int currentWord;

        private int gazeMode = 2;

        private int gazeSpeed = 2;

        private float gazeRoll = 0;

        private bool rollEnabled = false;

        #endregion

        #region Actions
        /// <summary>
        /// Action called whenever a speech synthesis event is finished
        /// </summary>
        public Action EndSpeechActionAction;

        /// <summary>
        /// Action called whenever furhat system senses users
        /// </summary>
        public Action<List<User>> SensedUsersAction;


        /// <summary>
        /// Action called whenever the Speech Recognizer returns an event
        /// </summary>
        public Action<string> RecognizedSpeechAction;


        ///// <summary>
        ///// Start with a null value and can be filled by using the LoadCategoryFileMethod
        ///// </summary>
        //private Dictionary<string, List<Behavior>> utteranceList;

        #endregion Actions

        #region Constructors and Initialization

        /// <summary>
        /// Constructor for the FurhatDotNet Interface. It will create a tcpClient and all the logic necessary to communicate with Furhat.
        /// </summary>
        /// <param name="ipAddress"> The Ip address of the furhat robot. Use "localhost" if you are using the dev-server or running this code on the robot</param>
        /// <param name="port"> The port to the tcp socket in IrisTK (default one is "1932") </param>
        public FurhatInterface(string ipAddress = "localhost", int port = 1932)
        {
            culture = new CultureInfo("en-US", false);

            currentWord = -1;

            inMiddleOfSpeaking = false;
            SayQueue = new Queue<Tuple<string, string>>();

            SubscribedEvents = new Dictionary<string, Action<string>>();
            MidTextActions = new List<Tuple<int, string, string>>();
            client = new Client(ipAddress, port);
            client.MessageReceived += new Action<string, string>(ProcessEvents);
            this.Connect("FurhatDotNet");

            SubscribeToEvent(EVENTNAME.MONITOR.SPEECH.WORD, new Action<string>(SpeechWordEvent));
            SubscribeToEvent(EVENTNAME.MONITOR.SPEECH.END, new Action<string>(SpeechEndEvent));
            SubscribeToEvent(EVENTNAME.SENSE.SPEECH, new Action<string>(SpeechSensedEvent));
            SubscribeToEvent(EVENTNAME.SENSE.USERS, new Action<string>(UsersSensedEvent));


        }



        /// <summary>
        /// Establish a TCP connection with the IrisTK Broker
        /// </summary>
        /// <param name="ticketName"> name of the ticket you want to share events with </param>
        /// <param name="moduleName"> name of your client </param>
        private void Connect(string moduleName)
        {
            client.Send("CONNECT broker " + moduleName + "\n");
        }

        #endregion

        #region Event Functionality

        /// <summary>
        /// You can assign an action that will be triggered every time a certain kind of event is triggered
        /// </summary>
        /// <param name="str"> event name that we want to subscribe to </param>
        /// <param name="action"> action that will be executed when we receive that event </param>
        private void SubscribeToEvent(string str, Action<string> action)
        {

            if (!SubscribedEvents.ContainsKey(str))
            {
                SubscribedEvents.Add(str, action);
            }

            string eventsToSubscribe = "";

            foreach (var item in SubscribedEvents)
            {
                eventsToSubscribe += " " + item.Key;
            }

            if (SubscribedEvents.Count > 0)
            {
                client.Send("SUBSCRIBE" + eventsToSubscribe + "\n");
            }
        }

        /// <summary>
        /// Goes through all existing subscribed events and if the one required exists it triggers the right action to be executed
        /// </summary>
        /// <param name="eventName"> Name of the event to be triggered </param>
        /// <param name="jsonString"> Json string to be passed to the function that handles that event </param>
        internal void ProcessEvents(string eventName, string jsonString)
        {
            foreach (var item in SubscribedEvents)
            {
                if (item.Key == eventName)
                    item.Value(jsonString);
            }
        }

        /// <summary>
        /// Internal function that actually sends the event to IrisTK via the tcp/ip client
        /// </summary>
        /// <param name="eventToSend"> Event to be sent to IrisTK. JSON.NET and the ActionEvents class helps with this task. </param>
        internal void SendEvent(GeneralEvent eventToSend)
        {
            Thread.Sleep(10);
            string json = JsonConvert.SerializeObject(eventToSend);
            client.Send("EVENT " + eventToSend.event_name + " -1 " + System.Text.ASCIIEncoding.ASCII.GetByteCount(json) + "\n");
            client.Send(json);
            //Console.WriteLine("EVENT " + eventToSend.event_name + " -1 " + System.Text.ASCIIEncoding.ASCII.GetByteCount(json) + "\n");
            //Console.WriteLine(json);
        }

        /// <summary>
        /// Internal function that actually sends the event to IrisTK via the tcp/ip client
        /// </summary>
        /// <param name="eventToSend"> Event to be sent to IrisTK. JSON.NET and the ActionEvents class helps with this task. </param>
        internal void SendHardcodedEvent(string eventName, string json)
        {
            Thread.Sleep(10);
            client.Send("EVENT " + eventName + " -1 " + System.Text.ASCIIEncoding.ASCII.GetByteCount(json) + "\n");
            client.Send(json);
            //Console.WriteLine("EVENT " + eventToSend.event_name + " -1 " + System.Text.ASCIIEncoding.ASCII.GetByteCount(json) + "\n");
            //Console.WriteLine(json);
        }

        #endregion

        #region Actions Triggered from Events

        private void UsersSensedEvent(string obj)
        {
            List<User> users = new List<User>();
            JObject o = JObject.Parse(obj);
            var usersJson = o.GetValue("users").Children();
            foreach (var user in usersJson)
            {
                User u = new User();
                var currentUser = user.First;
                JObject head = currentUser.Value<JObject>("head");
                JToken rot = head.GetValue("rotation");
                JToken loc = head.GetValue("location");
                u.id = currentUser.Value<string>("id");
                u.location = new Vector3Simple(loc.Value<float>("x"), loc.Value<float>("y"), loc.Value<float>("z"));
                u.rotation = new Vector3Simple(rot.Value<float>("x"), rot.Value<float>("y"), rot.Value<float>("z"));
                users.Add(u);
            }
            SensedUsersAction?.Invoke(users);
        }

        /// <summary>
        /// Triggered when there is a monitor.speech.rec event
        /// </summary>
        /// <param name="str"> Json string from monitor.speech.rec </param>
        private void SpeechSensedEvent(string str)
        {
            JObject o = JObject.Parse(str);
            RecognizedSpeechAction?.Invoke(o.GetValue("text").ToString());
        }

        /// <summary>
        /// Triggered when there is a monitor.speech.word event
        /// It also trigger MidTextActions that were filled in the Say method
        /// </summary>
        /// <param name="evString"> Json string from monitor.speech.word </param>
        private void SpeechWordEvent(string evString)
        {
            //Console.WriteLine(evString);
            MonitorEvents.MonitorSpeechWord speechWordEvent = JsonConvert.DeserializeObject<MonitorEvents.MonitorSpeechWord>(evString);
            currentWord = speechWordEvent.position;

            foreach (var item in MidTextActions)
            {
                if (item.Item1 == speechWordEvent.position)
                {
                    ExecuteAction(item);
                }
            }

        }

        /// <summary>
        /// Triggered when there is a monitor.speech.word event
        /// Checks if there are still actions to be triggered and updates inMiddleOfSpeaking variable
        /// </summary>
        /// <param name="ev">  </param>
        private void SpeechEndEvent(string ev)
        {
            foreach (var item in MidTextActions)
            {
                if (item.Item2 == VOICES.Identifier)
                    ChangeVoice(item.Item3);
                if (item.Item1 > currentWord)
                    ExecuteAction(item);
            }

            MidTextActions.Clear();
            MonitorEvents.MonitorSpeechEnd speechWordEvent = JsonConvert.DeserializeObject<MonitorEvents.MonitorSpeechEnd>(ev);

            inMiddleOfSpeaking = false;

            EndSpeechActionAction?.Invoke();

            currentWord = -1;

            if (SayQueue.Count > 0)
            {
                var toSayNext = SayQueue.Dequeue();
                if (toSayNext.Item2 == null)
                    Say(toSayNext.Item1);
                //else
                //    Say(toSayNext.Item1, toSayNext.Item2);
            }
        }

        /// <summary>
        /// Internally executes actions in the middle of say text
        /// </summary>
        /// <param name="item"> A tuple containing: 
        /// the position of the word of when it is supposed to be triggered(int), 
        /// name of the action to be performed (string), 
        /// and the string with the argument in that function  </param>
        private void ExecuteAction(Tuple<int, string, string> item)
        {
            switch (item.Item2)
            {
                case LED.Identifier:
                    var ledValues = item.Item3.Split(',');
                    ChangeLed(int.Parse(ledValues[0]), int.Parse(ledValues[1]), int.Parse(ledValues[2]));
                    break;
                case GESTURES.Identifier:
                    Gesture(item.Item3);
                    break;
                case FACETEXTURES.Identifier:
                    FaceTexture(item.Item3);
                    break;
                case VOICES.Identifier:
                    ChangeVoice(item.Item3);
                    break;
                case "gaze":
                    var gazeValues = item.Item3.Split(',');
                    Gaze(float.Parse(gazeValues[0], culture), float.Parse(gazeValues[1], culture), float.Parse(gazeValues[2], culture));
                    break;
            }
        }

        #endregion

        #region Speech

        /// <summary>
        /// You can create a strign list to be played in sequence 
        /// </summary>
        /// <param name="values"> List of strings that will be played in sequence </param>
        public void Say(List<string> values)
        {
            foreach (var item in values)
            {
                Say(item);
            }
        }

        /// <summary>
        /// Internal say function to make the robot actually say something (not to be accessed by DLL users)
        /// </summary>
        /// <param name="text"> Text to be said </param>
        /// <param name="wordEventTriggering"> If this is set to true it will trigger monitor.speech.word events</param>
        private void Say(string text, bool wordEventTriggering)
        {
            inMiddleOfSpeaking = true;
            SendEvent(new ActionEvents.Speak(text, wordEventTriggering));
        }

        /// <summary>
        ///Sends a text speech event to the synthesizer and adds an utterance to the speech queue
        /// </summary>
        /// <param name="text"> The text to speak </param>
        public void Say(string text)
        {
            if (inMiddleOfSpeaking)
                SayQueue.Enqueue(new Tuple<string, string>(text, null));
            else
            {
                //midtextaction are divided by | if there is such separator we should process them
                var midTextActions = text.Split('|');

                if (midTextActions.Count() > 1)
                {
                    FillMidTextActions(text);
                    text = Regex.Replace(text, @"\|.*?\|", String.Empty);
                    Say(text, true);
                }
                else
                {
                    Say(text, true);
                }
            }
        }

        /// <summary>
        ///Sends a text speech event to the synthesizer, adds an utterance to the speech queue and blocks the current thread until the speech is over
        /// </summary>
        /// <param name="text"> The text to speak </param>
        public void SayBlock(string toSay)
        {
            Say(toSay);

            while (inMiddleOfSpeaking)
            {
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Clears the internal speech queue and sends a Event to the robot to make him stop speaking
        /// </summary>
        public void SayStop()
        {
            SayQueue.Clear();
            inMiddleOfSpeaking = false;
            SendEvent(new ActionEvents.StopSpeech());
        }

        /// <summary>
        /// Parses the say text to create actions that will be executed in the middle of the text by recurring to monitor.speech.word events
        /// </summary>
        /// <param name="text"> text to be parsed </param>
        private void FillMidTextActions(string text)
        {
            string textWithNoSSML = Regex.Replace(text, "<.*?>", String.Empty);
            int wordNumber = -1;
            char previousCharacter = ' ';
            bool insideAction = false;
            bool insideArgument = false;
            StringBuilder action = new StringBuilder("");
            StringBuilder argument = new StringBuilder("");
            for (int i = 0; i < textWithNoSSML.Length; i++)
            {
                if (textWithNoSSML[i] == '|')
                {
                    if (insideAction)
                    {
                        MidTextActions.Add(new Tuple<int, string, string>(wordNumber + 1, action.ToString(), argument.ToString()));
                        action.Clear();
                        argument.Clear();
                    }
                    insideAction = !insideAction;
                }
                else
                {
                    if (insideAction)
                    {
                        if (textWithNoSSML[i] == '(')
                            insideArgument = true;
                        else if (textWithNoSSML[i] == ')')
                            insideArgument = false;
                        else if (insideArgument)
                            argument.Append(textWithNoSSML[i]);
                        else action.Append(textWithNoSSML[i]);
                    }
                    else
                    {
                        if (Char.IsLetter(textWithNoSSML[i]) && previousCharacter == ' ')
                        {
                            wordNumber++;
                        }
                        previousCharacter = textWithNoSSML[i];
                    }
                }
            }
        }


        /// <summary>
        /// An action that is sent to the synthesizer to stop speaking (and clears the speech queue)
        /// </summary>
        public void StopSpeaking()
        {
            SendEvent(new ActionEvents.StopSpeech());
        }

        #endregion

        #region Voice

        /// <summary>
        /// Change the text to speech voice using the name of the voice.
        /// </summary>
        /// <param name="name"> The name of the voice </param>
        public void ChangeVoice(string name)
        {
            SendEvent(new ActionEvents.ChangeVoiceByName(name));
        }
        #endregion

        #region Speech Recognition

        /// <summary>
        /// An action that is sent to the IRISTK recognizer to make it start listening.
        /// </summary>
        public void StartListening(int endSilTimeout = 700, int noSpeechTimeout = 8000, int nbest = 1, int maxSpeechTimeout = 15000/*, bool withIntermediateResults = false*/)
        {
            SendEvent(new ActionEvents.StartListening(endSilTimeout, noSpeechTimeout, nbest, maxSpeechTimeout/*, withIntermediateResults*/));
        }

        /// <summary>
        /// An action that can be sent to a recognizer to make it stop listening.
        /// </summary>
        public void StopListening()
        {
            SendEvent(new ActionEvents.StopListening());
        }

        #endregion

        #region Gaze

        /// <summary>
        /// Sets the global gaze roll of the robot and automatically enables roll on gaze commands. You can disable it again using the EnableRoll function.
        /// </summary>
        /// <param name="gazeRoll"> the roll value in degrees</param>
        public void SetGazeRoll(float gazeRoll)
        {
            rollEnabled = true;
            this.gazeRoll = gazeRoll;
        }

        /// <summary>
        /// Makes the system use the roll in gazeRoll by force
        /// </summary>
        /// <param name="enable"> set it to true or false </param>
        public void EnableRoll(bool enable)
        {
            this.rollEnabled = enable;
        }

        /// <summary>
        /// Sets the global gaze mode of the robot
        /// </summary>
        /// <param name="gazeMode"> 1 - eyes only; 2 - default behavior with eyes first and neck afterwards </param>
        public void SetGazeMode(int gazeMode)
        {
            this.gazeMode = gazeMode;
        }

        /// <summary>
        /// Sets the global gaze speed of the robot to the given value
        /// </summary>
        /// <param name="gazeSpeed"> 2 is default </param>
        public void SetGazeSpeed(int gazeSpeed)
        {
            this.gazeSpeed = gazeSpeed;
        }

        /// <summary>
        /// Makes the agent shift gaze to a certain location in 3D space
        /// </summary>
        /// <param name="location"> The 3D location where the agent should gaze </param>
        public void Gaze(Location location)
        {
            if (rollEnabled)
                SendEvent(new ActionEvents.PerformGazeWithRoll(location, gazeMode, gazeSpeed, gazeRoll));
            else
                SendEvent(new ActionEvents.PerformGaze(location, gazeMode, gazeSpeed));

        }


        /// <summary>
        /// Makes the agent shift gaze to a certain location in 3D space
        /// </summary>
        /// <param name="x"> x position </param>
        /// <param name="y"> y position </param>
        /// <param name="z"> z position </param>
        public void Gaze(double x, double y, double z)
        {
            Gaze(new Location(x, y, z));
        }

        public void Attend(string name, int mode = 0, int speed = 2, double roll = 0)
        {
            if(rollEnabled)
                SendEvent(new ActionEvents.AttendWithRoll(name, mode, speed, roll));
            else
                SendEvent(new ActionEvents.Attend(name, mode, speed));
        }

        public void ConnectSkill(string name)
        {
            SendEvent(new ActionEvents.SkillConnect(name));
        }

        #endregion

        #region Gesture

        /// <summary>
        /// Makes the agent perform a specific gesture.
        /// </summary>
        /// <param name="gesture"> The name of the gesture </param>
        public void Gesture(string gesture)
        {
            SendEvent(new ActionEvents.PerformGesture(gesture));
        }

        #endregion

        #region FaceTexture

        /// <summary>
        /// Makes the agent change the texture of the face.
        /// </summary>
        /// <param name="name"> The name of the texture </param>
        public void FaceTexture(string name)
        {
            SendEvent(new ActionEvents.ChangeFaceTexture(name));
        }

        #endregion

        /// <summary>
        /// Changes the led color of the robot.
        /// </summary>
        /// <param name="name"> The name of the texture </param>
        public void ChangeLed(int red, int green, int blue)
        {
            SendEvent(new ActionEvents.ChangeLedSolidColor(red,green,blue));
        }

    }




    public static class InText
    {
        /// <summary>
        /// Generates a gesture tag to be inserted inside a say string.
        /// </summary>
        /// <param name="name"> Gesture to be "played". We recommend using the GESTURES.x static string variables.</param>
        /// <returns></returns>
        public static string Gesture(string name)
        {
            return "|" + GESTURES.Identifier + "(" + name + ")|";
        }

        /// <summary>
        /// Generates a texture swap tag to be inserted inside a say string. This will change the face of the robot to the determined texture.
        /// </summary>
        /// <param name="textureName"> Face texture to be "changed". We recommend using the FACETEXTURES.x static string variables.</param>
        /// <returns></returns>
        public static string FaceTexture(string textureName)
        {
            return "|" + FACETEXTURES.Identifier + "(" + textureName + ")|";
        }

        /// <summary>
        /// Generates a gaze tag to be inserted inside a say string. This will make the robot gaze to the determined point in x,y,z.
        /// </summary>
        /// <param name="x"> Point in meters to the left or right of the robot.</param>
        /// <param name="y"> Point in meters up or down in relation to the robot.</param>
        /// <param name="z"> Point in meters in straight distance to the robot.</param>
        /// <returns></returns>
        public static string Gaze(float x, float y, float z)
        {
            return "|gaze" +"("+ x.ToString(FurhatInterface.culture) + ',' + y.ToString(FurhatInterface.culture) + ',' + z.ToString(FurhatInterface.culture) +")|";
        }

        public static string Led(int red, int green, int blue)
        {
            return "|" + LED.Identifier + "(" + red + ',' + green + ',' + blue + ")|";
        }


        public static string SAPIVolume(string str, SAPI.VolumeValues volValue)
        {
            string strVolValue = "";
            switch (volValue)
            {
                case SAPI.VolumeValues.silent:
                    strVolValue = "silent";
                    break;
                case SAPI.VolumeValues.xsoft:
                    strVolValue = "x-soft";
                    break;
                case SAPI.VolumeValues.soft:
                    strVolValue = "soft";
                    break;
                case SAPI.VolumeValues.medium:
                    strVolValue = "medium";
                    break;
                case SAPI.VolumeValues.loud:
                    strVolValue = "loud";
                    break;
                case SAPI.VolumeValues.xloud:
                    strVolValue = "x-loud";
                    break;
                case SAPI.VolumeValues.default_:
                    strVolValue = "default";
                    break;
            }

            return "<prosody volume=\"" + strVolValue + "\">" + str + "</prosody>";
        }

        public static string SAPIVolume(string str, int volume)
        {
            return "<prosody volume=\"" + volume.ToString() + "\">" + str + "</prosody>";
        }

        public static string SAPIRate(string str, SAPI.RateValues rateVal)
        {
            string rateValue = "";
            switch (rateVal)
            {
                case SAPI.RateValues.xslow:
                    rateValue = "x-slow";
                    break;
                case SAPI.RateValues.slow:
                    rateValue = "slow";
                    break;
                case SAPI.RateValues.medium:
                    rateValue = "medium";
                    break;
                case SAPI.RateValues.fast:
                    rateValue = "fast";
                    break;
                case SAPI.RateValues.xfast:
                    rateValue = "x-fast";
                    break;
                case SAPI.RateValues.default_:
                    rateValue = "default";
                    break;
            }
            return "<prosody rate=\"" + rateValue + "\">" + str + "</prosody>";
        }

        public static string SAPIPitch(string str, SAPI.PitchValues pitchVal)
        {
            string pitchValue = "";
            switch (pitchVal)
            {
                case SAPI.PitchValues.xlow:
                    pitchValue = "x-low";
                    break;
                case SAPI.PitchValues.low:
                    pitchValue = "low";
                    break;
                case SAPI.PitchValues.medium:
                    pitchValue = "medium";
                    break;
                case SAPI.PitchValues.high:
                    pitchValue = "high";
                    break;
                case SAPI.PitchValues.xhigh:
                    pitchValue = "x-high";
                    break;
                case SAPI.PitchValues.default_:
                    pitchValue = "default";
                    break;
            }
            return "<prosody pitch=\"" + pitchValue + "\">" + str + "</prosody>";
        }

        public static string SAPIEmphasis(string str, SAPI.EmphasisValues emphasisVal)
        {
            string emphasisValue = "";
            switch (emphasisVal)
            {
                case SAPI.EmphasisValues.reduced:
                    emphasisValue = "reduced";
                    break;
                case SAPI.EmphasisValues.moderate:
                    emphasisValue = "moderate";
                    break;
                case SAPI.EmphasisValues.strong:
                    emphasisValue = "strong";
                    break;
                case SAPI.EmphasisValues.none:
                    emphasisValue = "none";
                    break;
            }
            return "<emphasis level=\"" + emphasisValue + "\">" + str + "</emphasis>";
        }

        /// <summary>
        /// Inserts or reduces a pause in the middle of the text 
        /// </summary>
        /// <param name="breakType"> Use the BreakValues static class as a parameter </param>
        /// <returns></returns>
        public static string SAPIBreak(SAPI.BreakValues breakVal)
        {
            string strBreakValue = "";
            switch (breakVal)
            {
                case SAPI.BreakValues.none:
                    strBreakValue = "none";
                    break;
                case SAPI.BreakValues.xweak:
                    strBreakValue = "x-weak";
                    break;
                case SAPI.BreakValues.weak:
                    strBreakValue = "weak";
                    break;
                case SAPI.BreakValues.medium:
                    strBreakValue = "medium";
                    break;
                case SAPI.BreakValues.strong:
                    strBreakValue = "strong";
                    break;
                case SAPI.BreakValues.xstrong:
                    strBreakValue = "x-strong";
                    break;
            }
            return "<break strength=\"" + strBreakValue + "\"/>";
        }

        /// <summary>
        /// Define a break with seconds
        /// </summary>
        /// <param name="time"> Time to pause eitheir in seconds or miliseconds. See the next parameter </param>
        /// <param name="miliseconds"> if this is false the pause will be in seconds </param>
        /// <returns> Text to be included in the Say function </returns>
        public static string SAPIBreak(int time, bool miliseconds)
        {
            return "<break time=\"" + time.ToString() + (miliseconds ? "ms" : "s") + "\"/>";
        }

        /// <summary>
        /// Inserts a SSML mark in the text. This mark will be triggered by the monitor.speech.mark event
        /// </summary>
        /// <param name="str"> value that will appear in the event when it triggers </param>
        /// <returns> Text to be included in the Say function </returns>
        public static string SAPIMark(string str)
        {
            return "<mark name=\"" + str + "\"/>";
        }
    }
}
