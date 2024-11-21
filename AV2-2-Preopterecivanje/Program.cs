/* 
    -Cijela fora preopterecivanja operatora je da mozemo stavit custom definicije za te operatore, te time mozemo
     npr. zbrajati kompleksne brojeve. Ako bismo probali zbrajati kompleksne brojeve bez preopterecenja, krenio bi
     plakati jer on ne zna kako zbrajati kompleksne brojeve. Zato u samu klasu napravimo staticku funkciju koja
     se bavi zbrajanjem kompleksnih brojeva

    -Vazne dostupne klase proci sami kuci ili na labosu al je lakse stoput da prodjemo kuci, sve je otvoreno na
     merlinu i ima dokumentacija
*/

internal class Program
{
    static void Main(string[] args)
    {
        Complex c1 = new Complex(1, 7);
        Complex c2 = new Complex(2, -6);

        Complex c3 = c1 + c2;


        Random generator = new Random();
        int random_int = generator.Next();
        double random_double = generator.NextDouble(); //Vraca broj izmedju 0 i 1
        Complex random_complex = generator.NextComplex();
        Complex no_sugar = RandomExtensions.NextComplex(generator); //isti zapis al ovo nije sugar, tj nije ~pretty~
    }
}

class Complex
{
    public int Real { get; private set; }
    public int Imaginary { get; private set; }
    private string _sign;

    public Complex() : this(0, 0) { }
    public Complex(int value) : this(value, value) { }

    public Complex(int Real, int Imaginary)
    {
        this._sign = "";
        this.Real = Real;
        this.Imaginary = Imaginary;
    }

    public string getAsString()
    {
        _sign = Imaginary < 0 ? "" : "+";
        return $"{Real}{_sign}{Imaginary}i";
    }

    public static Complex operator +(Complex left, Complex right)
    {
        return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
    }

    public double CalculateModulus() /*ne slati objekt iz vana inace ce nas rusit automatski. Linija je realno 
                                          * retardirana jer ce ti izgledat kao c_dumb.CalculateModulus(c_dumb); 
                                          * salje samog sebe*/
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary); /*ovdje imas onaj Math.Pow al je on sporiji jer je namjenjen
                                                                * za svaku vrstu potenciranja a mi imamo jednostavno 
                                                                * kvadriranje*/
    }
    public static bool operator >(Complex left, Complex right)//Ako prigovara to je jer mu fali definicija za suprotno
    {
        return left.CalculateModulus() > right.CalculateModulus();
    }
    public static bool operator <(Complex left, Complex right)
    {
        return right > left;
    }
    public static bool operator <=(Complex left, Complex right)
    {
        return !(left > right);
    }
    public static bool operator >=(Complex left, Complex right)
    {
        return !(right > left);
    }
}

static class RandomExtensions
{
    public static Complex NextComplex(this Random generator)
    {
        int re = generator.Next();
        int im = generator.Next();
        return new Complex(re, im); /*Moze se samo upisat generator.Next kao argumenti al je bolje koristit atribute
                                         * ali tako nema neke logike koji ce se random prvo pozvat 
                                         * (? objasnjenje ali kao praksa bolje ne koristit magic numbers)*/
    }
}

/*class StudentError

{
    int value = 10;

    public int Value { get; set; } -> OVO NE RADIT JER OVAJ PROPERTY NEMA VEZE SA ONOM VARIJABLOM GORE, 
                                      GRESKA ZNA RUSIT ZA OVO
    -> Automatsko svojstvo 'Value' u ovom slucaju nije na nikakav nacin povezana sa atributom 'value',
       kao da su odvojene varijable

}

main:
StudentError error = new STudentError();
error.value = 20;

*/