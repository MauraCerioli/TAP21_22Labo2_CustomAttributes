using System;
using System.Reflection;
using MyAttribute;

namespace Executor{
    class Program{
        static void Main(string[] args){
            var a = Assembly.LoadFrom(@"..\..\..\..\MyLibrary\bin\Debug\net5.0\MyLibrary.dll");
            foreach (var type in a.GetTypes())
                if (type.IsClass){
                    Console.WriteLine(type.FullName);
                    object? receiver;
                    try{
                        receiver = Activator.CreateInstance(type);
                    }
                    catch (MissingMethodException e){
                        Console.WriteLine(e);
                        continue;
                    }
                    foreach (var m in type.GetMethods()){
                        foreach (var att in m.GetCustomAttributes<ExecuteMeAttribute>()){
                            try{
                                VerifyParameters(att.Actual, m.GetParameters());
                            }
                            catch (ArgumentException e){
                                Console.WriteLine(e.Message);
                                continue;
                            }
                            m.Invoke(receiver, att.Actual);
                        }
                    }
                }

            void VerifyParameters(object[] attActual, ParameterInfo[] parameterInfos){
                if (attActual.Length != parameterInfos.Length)
                    throw new ArgumentException(
                        $"Unexpected parameter number ({parameterInfos.Length} expected, it were {attActual.Length})");

                for (int i = 0; i < attActual.Length; i++){
                    if (!attActual[i].GetType().IsAssignableTo(parameterInfos[i].ParameterType)){
                        throw new ArgumentException($"Unexpected parameter type in position {i} ({parameterInfos[i].ParameterType.Name} expected, {attActual[i].GetType().Name} received)");
                    }
                }
            }
        }
    }
}