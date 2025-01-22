namespace AV5_2_Nastavak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try // Ako funkcija baca igdje mora ju se stavljat u try-catch-throw
            {
                HealthCalculator calculator = new HealthCalculator();
                Console.WriteLine("In Try");
                Console.Write("Enter height [m]: ");// Ovo ce radit ali ako njemu dodje neki format koji nije brojka (aka. One hundred and sixty) kod breaka
                double height = double.Parse(Console.ReadLine());
                Console.Write("Enter weight [kg]: ");
                double weight = double.Parse(Console.ReadLine());


                Console.WriteLine(calculator.CalculateBmi(height, weight));
            }
            catch (ArgumentException e) // Ne moras ga identificirat, i to oznacava ignoriraj ga i ne mora ga nista pisat
                                        /* Catchevi se redaju po nekoj hijerarhiji, znaci od ajmo rec najmanjeg do nadleznog (aka samo exception), 
                                         * ima neka hijerarhija, mozemo ju radit sami ili idemo u dokumentaciju (za hijerarhiju, npr idemo po polju i radimo Console.ReadLine, 
                                         * prvo bi islo IndexOutOfBound exception pa onda ArgumentException jer se prvo lista kroz red pa onda se gledaju argumenti) */
            {
                Console.WriteLine("Argument Exception:");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e) // Ovaj exception hvata sve errore, mozemo napisat za nesto specificno
                                // Ako dodje do neke iznimke di moze puknit kod, ovo omogucava da samo kaze hej ne radi i ne breaka program nego samo ode dalje, hvala ti Husakovic sta to ne znas objasnit :thumbs_up:
            {
                Console.WriteLine(e.Message);
                //return; //Ovako se izlazi iz catcha i breaka program
            }
            /*finally // nije obavezan blok
            {
                
            }*/
            Console.WriteLine("Still Running.");

        }
    }

    class HealthCalculator
    {
        public double CalculateBmi(double heightMeters, double weightKilograms)
        {
            
            if(heightMeters <= 0)
            {
                //throw new Exception(""); // Ovo se ne radi nikad jer ne najopcenitiji tip i ne zna se gdje je nastao problem, task failed successfully
                throw new ArgumentException($"Parameter {nameof(heightMeters)} was {heightMeters}. Illegal value.");
            }
            return weightKilograms / (heightMeters * heightMeters);
        }
    }
}
