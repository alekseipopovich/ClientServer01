using System.Reflection;

Assembly asm = Assembly.LoadFrom("Part04Reflection.dll");


Console.WriteLine(asm.FullName);


Type? t = asm.GetType("Programm04");
if (t is not null)
{
    // получаем метод Square
    MethodInfo? square = t.GetMethod("Square", BindingFlags.NonPublic | BindingFlags.Static);

    // вызываем метод, передаем ему значения для параметров и получаем результат
    object? result = square?.Invoke(null, new object[] { 7 });
    Console.WriteLine(result);
}

// получаем все типы из сборки Part04Reflection.dll
Type[] types = asm.GetTypes();
foreach (Type t1 in types)
{
    Console.WriteLine(t1.Name);
}
