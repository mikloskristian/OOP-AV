/* Nasljedivanje je jako fucking korisno i koristit ce se ja pretpostavljam u svakom zadatku od sada pa nadalje.
 * Fora kod nasljedivanja je da imamo jednu klasu koja je "korijenska" klasa, aka njezine funkcije i varijable
   mogu dijeliti bilo koje funkcije na koje ju spojimo.
 * Primjer, na farmi imamo puno zivotinja, i svaka zivotinja ima nesto posebno za njih (krava daje mlijeko, prase daje
   slaninu, konj vuce kociju itd itd) ali imaju jako puno stvari koje im je zajedno (age, movement, drink, eat, breathe)
 * I sada, umjesto da mi za svaku klasu posebno pravimo varijable age, isThirsty, isHungry, funkcije Move(), Eat() itd,
   mi cemo napraviti jednu zajednicku klasu, recimo Animal, koja ce imati sve te atribute inicijalizirane.
 * Korisno je zbog toga sto se te 
 */

internal class Program
{
    static void Main(string[] args)
    {
        Character ashenOne = new Character("Alfonzo", 1);
        Console.WriteLine(ashenOne.GetAsString());
        Console.WriteLine(ashenOne.Attack());

        Orc orc1 = new Orc("Danilo", 20, "Forest", true);
        Console.WriteLine(orc1.GetAsString());
        Console.WriteLine(orc1.Attack());

        Character orc2 = new Orc("Rughh", 300, "Rock", true); // Polimorfizam (everybody cheered), ima pristup samo 
        Console.WriteLine(orc2.Attack());                     // Mogu prizvat samo funkcije i varijable koje su u
                                                              // character, ali se koriste funkcije i varijable iz
                                                              // orc klase
        Elf legolas = new Elf("Leglosa", 100);

        Character[] characters = new Character[]
        {
            ashenOne, legolas, orc2
        };

        foreach (Character character in characters)
        {
            Console.WriteLine(character.Attack()); // Ovdje ce attack biti specijalizirat ovisno o izvedenom tipu,
                                                   // Ovo je moc polimorfizma i profesor to *obozava* (same lol) bruhić
        }

        foreach (Character character in characters)
        {
            Console.WriteLine(character); //ovako se priziva funkcija ToString() koja je skrivena funkcija svakog objekta
        }

        int a = 10;
        double d = a;
        Console.WriteLine(d); // Ovo ce dat jer pretvaras jednostavniji tip podatka u slozeni (cijeli u decimalni)

        double pi = 3.14;
        int FeritPi = (int)pi; // Ovo nece dat jer slozenije u jednostavno

        Elf elf = new Elf("", 10);
        Character c = elf; //Upcastanje, castas ju na osnovni tip, isto kao int u double
        Elf PointerToElf = (Elf)c; //Downcastanje, mora se implicirati tip
    }
}

class Character //Necu sad implementirati jer bi bilo jos vise komentiranja i ne da mi se, ali ubiti ako bi se stavilo
                //abstract class onda se character klasa NE BI MOGLA implementirati, samo izvoditi
{
    private string name;
    private int hp;

    /*public Character()
    {
        Console.WriteLine("Def. ctor Character");
        this.name = "Unknown hero";
        this.hp = 100;              //defaultne vrijednosti zadane za sve charactere u nekoj igrici
    }*/

    public Character(string name, int hp)
    {
        //Console.WriteLine("Par. ctor Character");
        this.name = name;
        this.hp = hp;
    }

    public virtual string GetAsString() // Virtualne metode omogucuju da izvedene klase overrideaju tu metodu
    {
        return $"{name} {hp}";
    }

    //Posto je ova klasa naslijedila skrivenu klasu "object" ovo gore mos i ovako
    public override string ToString()
    {
        return $"{name} {hp}";
    } //Onda mozes napravit Console.WriteLine(characters) gore kod foreach i ispisat ce ovako string umjesto default
      //sta je u ToString() funkciji, tj imeSolutiona.imeObjekta

    protected string GetNickName() //ovome mogu pristupiti SAMO ova klasa i svaka klasa koja ju nasljeduje
    {
        return name.ToUpper();
    }

    public int Hp
    {
        get { return hp; }
        protected set { this.hp = Math.Max(0, value); } // Varijabli mogu pristupit svi, ali ju mijenjat samo klase
    }                                                   // koje naslijeduju character

    public virtual int Attack()
    {
        return hp;
    }

    /*public abstract void Heal()    //Moze se implementirati samo abstraktnoj klasi, sta ja nemam :c ...skill issue
                                     //Ovo se mora implementirati u svakoj klasi koja ju nasljeduje da se moze raditi
                                     //dalje, maltene napravis funkciju mandatory, kao svaka zivotinja mora hodati, jesti
                                     //i piti
    */


}

class Orc : Character //Orc ce ovdje nasljediti ime i hp od charactera zadan gore
{
    private string tribe;
    private bool isEnraged;

    public Orc(string name, int hp, string tribe, bool isEnraged)
        : base(name, hp)
    {
        //Console.WriteLine("Par. ctor Orc");
        this.tribe = tribe;
        this.isEnraged = isEnraged;
    }   //Parametarski konstruktor izvedene klase napisati sto sire i sa varijablama parent klase,
        //pa onda napisat base() koji pristupa konstruktoru parent klase, i prenesemo mu name i hp
        //moze ic i neke default vrijednosti al sta ako imam 1000 orcova, jel ja moram mijenjat
        //svakom ime i hp posebno? Moram al ne zelim




    public override string GetAsString() // Sakrivena funkcija iz parent klase, prioritizira se ova, mos stavit mis na ime 
    {                                    // pa vidis da je istina, nije dobra praksa tako da bolje radit override, vise o tome dolje
        return this.GetNickName() + base.GetAsString();
    }

    public override int Attack() //Override *dozvoljava* funkciji da mijenja implementaciju od parent klase
    {
        if (isEnraged)
        {
            return base.Attack() * 2;
        }
        else
        {
            return base.Attack();
        }
    }

    public void Heal() // Da je character abstract vamo bi trebalo bit override
    {
        this.Hp += 100;
    }
}

class Elf : Character
{

    public Elf(string name, int hp) : base(name, hp) //MORA imat takav konstruktor jer jedini ctor koji ima u parent
                                                     //je parametarski (pogledaj gore klasu Character)
                                                     //moze bit prazan ctor jer nema nesto posebno da se treba
                                                     //implementirati
    {

    }

    public override int Attack()
    {
        return base.Attack() / 2;
    }

    /*
    public override void Heal()
    {
        Hp += 1000;
    }
     
     */
}