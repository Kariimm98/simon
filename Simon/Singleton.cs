using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon
{
    class Singleton
    {
        private readonly static Singleton _instance = new Singleton();
        public Stack<int> Historic { get; set; }

        public String Nom { set; get; }

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }


    }
}
