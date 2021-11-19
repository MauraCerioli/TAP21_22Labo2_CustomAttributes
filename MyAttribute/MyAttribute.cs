using System;

namespace MyAttribute {
    [AttributeUsage(validOn:AttributeTargets.Method,AllowMultiple = true)]
    public class ExecuteMeAttribute:Attribute {
        public object[] Actual{ get; }

        public ExecuteMeAttribute(params object[] actual){
            Actual = actual;
        }
    }
}
