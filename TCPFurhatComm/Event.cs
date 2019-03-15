using System;

namespace TCPFurhatComm
{
    public class GeneralEvent
    {
        static int eventCount = 0;

        /// <summary>
        /// The name of the event (such as "action.speech")
        /// </summary>
        public string event_name { get; set; }
        
        /// <summary>
        /// A unique ID for the event
        /// </summary>
        public string event_id { get; set; }

        ///// <summary>
        ///// The name of the module that sent the event
        ///// </summary>
        //public string event_sender { get; set; }

        /// <summary>
        /// A timestamp when the event was created
        /// </summary>
        public string event_time { get; set; }

        public GeneralEvent(string event_name)
        {
            this.event_name = event_name;
            this.event_id = GeneralEvent.eventCount++.ToString();
            //this.event_sender = "DotNet";
            this.event_time = new DateTime().ToString("yyyy-MM-dd HH:mm:ss:fff");
            new Vector3Simple(1.0, 1.0, 1.0);
        }
    }

}
