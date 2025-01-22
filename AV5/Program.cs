using AV5.Demo; // Iako je child namespace ne moze mu pristupiti, bez ovoga bi trebalo demostration ovako izgledat: AV5.Demo.Demonstration d;
using Faculty;
using Faculty.Staff; // Ako stavimo ovako sve sta je pod namespaceom "Students" je sad vidljivo u ovom fileu
namespace AV5 // prostor imena :O, po defaultu se imenuje isto kao ime filea
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student(); //da nema gore using ovo ti nece dat jer su drugaciji namespace
            Teacher teacher = new Teacher();
            Demonstration d = new Demonstration();
            Console.WriteLine(d);
        }
    }
}
