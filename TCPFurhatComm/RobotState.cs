using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPFurhatComm
{
    public abstract class RobotState
    {
        public string Name;
        public string PreviousState;

        public abstract void UpdateMind();
        public abstract void UpdateBody();

        public abstract void BehaviorFinished();
        public abstract void Terminate();

    }


}
