/* 
samo da prefaceam varijable = atributi, funkcije = metode, nisam znojavac meni je to ista stvar tako da ako pisem oboje
mislim na istu stvar
i ja u vs nemam word wrap jer sam naviko tako radit tako da ako ti komentari izgledaju bespotrebno wrapani nisu,
its not a lifestyle its a choice

    -ili radit sve preko konstruktora ili sve preko propertya, najbolje ne mix and chooseat

    !!!! NE MOZES STVORIT OBJEKT BEZ KONSTRUKTORA !!!!

    -Ako se u klasi napravi parametarski ili bilo kakav konstruktor, compiler prekine radit automatski default konstruktor
    -breakpoint se radi tako da skroz lijevo kod zaslona za kod (ova mala sivkasta traka) kliknes na liniju na kojoj
     zelis napravit breakpoint, te kad kliknes play kod ce se izvrsit i stat ce tamo di je stavljen breakpoint
    
    1. ovo se zove automatski jer C# sam napravi skriveni atribut u pozadini, mozemo pick and chooseat 
       hoce mu get ili set biti privatni ili javni
    2. Ovo je samo drugaciji nacin za napisat if, tako da ako je imaginary < 0 stavi "", a ako nije onda stavi "+"
    3. this se u ovom kontekstu poziva konstruktor koji vec ima neku odredenu logiku, i u zagrade mu samo passamo 
       atribute koje zelimo da taj konstruktor ima, ne moramo ponavljati jedno te istu stvar (npr ovaj drugi konstruktor 
       umjesto da sad pisem this.real = value i bla bla, vec imam konstruktor koji ima tu logiku pa cemo iskoristit
       njega preko kljucne rijeci this, i onda cemo u zagrade stavit parametre koji taj konstruktor treba, tj. 2 inta)
    4. static atributi i metode su atributi i metode koje se odnose na klasu u cijelosti ne samo na specifican
       objekt, tako da kad pozovemo IncreaseSoldierCost(), za obje vojske se increasa jednako. Sto manje static atributa i
       metoda to bolje!!!!!!!
*/


internal class Program
{
    static void Main(string[] args)
    {
        Complex c1 = new Complex(0, -4);
        Console.WriteLine(c1.getAsString());

        Army croArmy = new Army(35);
        Army usaArmy = new Army(36);
        Console.WriteLine(croArmy.GetSoldierCost());
        Console.WriteLine(usaArmy.GetSoldierCost());
        croArmy.IncreaseSoldierCost();
        Console.WriteLine(croArmy.GetSoldierCost());
        Console.WriteLine(usaArmy.GetSoldierCost());

        Army mercenaries = Army.Hire(200);
        Army volounteers = Army.Recruit(3000); /* ovo ne mogu bas ispisat jer su soldiers private al ako stavis
                                                    * breakpoint na ovo vidit ces da mercenaries ima 18 soldiera,
                                                    * znaci radi */
    }
}

class Complex
{
    public int Real { get; private set; } // 1.
    public int Imaginary { get; private set; }
    private string _sign;

    public Complex() : this(0, 0) { } //3. 
    public Complex(int value) : this(value, value) { } //3.

    public Complex(int Real, int Imaginary)
    {
        this._sign = "";
        this.Real = Real;
        this.Imaginary = Imaginary;
    }

    public string getAsString()
    {
        _sign = Imaginary < 0 ? "" : "+"; // 2.  
        return $"{Real}{_sign}{Imaginary}i";
    }
}

class Army
{
    private static int _baseSoldierCost = 10; //4.
    public int Soldiers;
    public Army(int Soldiers)
    {
        this.Soldiers = Soldiers;
    }

    public void IncreaseSoldierCost() //ne preporuca za radit
    {
        _baseSoldierCost++;
    }

    public int GetSoldierCost()
    {
        return _baseSoldierCost;
    }

    public static Army Hire(int gold)
    {
        /* this.soldiers = ... -> ovo nece radit jer staticke metode ne mogu prizivati ne staticke atribute,
         * static na static*/
        return new Army(gold / _baseSoldierCost);
    }

    public static Army Recruit(int populationSize)
    {
        return new Army(populationSize / 2);
    }
}