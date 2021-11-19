using System;
using MyAttribute;

namespace MyLibrary {
    
    public class Foo {
        [ExecuteMe]
        [ExecuteMe(5,6)]
        public void M1() {
            Console.WriteLine("M1");
        }

        [ExecuteMe(45)]
        [ExecuteMe()]
        [ExecuteMe(3)]
        public void M2(int a) {
            Console.WriteLine("M2 a={0}", a);
        }

        public static int Puffo(){
            return 42;}

        [ExecuteMe("hello", "reflection")]
        [ExecuteMe("abc",5)]
        [ExecuteMe("ok","ok")]
        [ExecuteMe(5,"fff")]
        [ExecuteMe()]
        public void M3(string s1, string s2) {
            Console.WriteLine("M3 s1={0} s2={1}", s1, s2);
        }
        [ExecuteMe("hello", "reflection")]
        public void M4(string s1, string s2) {
            Console.WriteLine("M4 s1={0} s2={1}", s1, s2);
        }
    }

    public class NoDefaultConstructor{
        public NoDefaultConstructor(int a){}
        [ExecuteMe()]
        public void NONoNo(){Console.WriteLine("non lo dovremmo mai vedere");}
    }

    public class AnotherClass{
        [ExecuteMe()]
        public void K0(){Console.WriteLine("K");}
    }
}
