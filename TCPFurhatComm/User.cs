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
        public float x;
        public float y;
        public float z;

        public Vector3Simple(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }
}