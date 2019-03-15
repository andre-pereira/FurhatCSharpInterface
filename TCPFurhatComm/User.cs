using System.Collections.Generic;

namespace TCPFurhatComm
{
    public class User
    {
        public string id;
        public Vector3Simple rotation;
        public Vector3Simple location;

    }


    public class Vector3Simple
    {
        public double x;
        public double y;
        public double z;

        public Vector3Simple(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }
}