using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{ 
    public class constants
    {
        public const float fEpsilon = 0.00000001F;
        public const float fDefTS_Ad125 = 0.0033F;    //AD125
        public const float fDefTS_Ad135 = 0.0005F;    //AD135
        //public const float fDefTS = 0.0125F;    //Supo2
        public const float fDefTS_Supo = 0.0025F;    //Supo
        public const float fDefTS_GQ = 2e-7F;    //GODIVAQ
        public const float fDefTS_AD135p = 0.001F;    //GODIVAQ

        public static float DelT(string sModName)
        {
            switch(sModName)
            {
                case "AD125":
                    return fDefTS_Ad125;
                case "AD135":
                    return fDefTS_Ad135;
                case "SupoModel":
                    return fDefTS_Supo;
                case "GODIVAQ":
                    return fDefTS_GQ;
                case "MRML2HE135R":
                    return fDefTS_Ad135;
            }
            return 0.001F;    //overall default
        }
    }
}
