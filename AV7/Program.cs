namespace AV7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* - Ako nesto vamo fali fizicki nisam stigo ide 100 na sat
               - Potrebna jedna ili dvije dvolisnice za ispit
               - Ako ne planiramo rjesavat neki zadatak krizat ga na papiru da jadan prof ne mora trazit
               - Ako implementiramo overrideani operator > ne moramo suprotan
               - Sve se mora nekako inicijalizirati, ako nije to je onda greska, objekt mora bit doveden u stanje sigurno za rad (prvi av!!!!!!)
               - Rok je objavljen na merlinu valjda ali ne sa rijesenjom*/
        }
    }

    public class Company
    {
        string name;
        decimal yearlyEarning;
        decimal stockValue;
        public Company(string name, decimal yearlyEarning, decimal stockValue)
        {
            this.name = name;
            this.yearlyEarning = yearlyEarning;
            this.stockValue = stockValue;
        }

        public string Name { get { return this.name; } } // NE MOZEMO PRAVITI AUTOMATSKO SVOJSTVO!!!!!!
        public decimal YearlyEarning { get { return this.yearlyEarning; } }
        public decimal StockValue { get { return this.stockValue; } }

        public static bool operator >(Company left, Company right) //Ne moras na ispitu pisat definiciju za < iako vs sere da treba
        {
            if (left.yearlyEarning == right.yearlyEarning)
                return left.stockValue > right.stockValue;
            return left.yearlyEarning > right.yearlyEarning; //Ne mora bit optimiziran kod samo da radi!!!!!!
        }

        public string CreateAcronym() //Ne vamo pisat string name jer string name vec imamo saveano u klasi i time rusimo enkapsulaciju i time zoric rusi nas :D
        {
            string acronym = "";

            string[] words = name.Split(" "); //Metoda koja string dijeli po "substringovima" i stavlja ih u zasebno polje
            if (words.Length >= 3)
            {
                for (int i = 0; i < words.Length; i++)
                {
                    acronym += words[i][0]; //String je opet samo array charactera

                    /*
                     string word = words[i];
                    acronym += word[0];
                     */

                }
            }
            else 
            {
                for (int i = 0; i < name.Length; i++)
                {
                    //ovo se moze radit da napravis string result = string.Join("", words); i onda izbjegavas ovaj prvi if
                    if (name[i] != ' ')
                        acronym += name[i];
                    if (acronym.Length == 3)
                        break;
                }
            }

            return acronym.ToUpper();
        }

    }

    public class DrasticYears
    {
        private int bestYear;
        private int worstYear;

        public DrasticYears(int bestYear, int worstYear)
        {
            this.bestYear = bestYear;
            this.worstYear = worstYear;
        }

        public int BestYear { get; private set; }
        public int WorstYear { get; private set; }

    }

    public static class StockAnalyst
    {
        public static DrasticYears FindDrasticYears(Dictionary<int, Company> reports)
        {
            int bestYear = reports.First().Key;
            int worstYear = bestYear;

            foreach (var report in reports) //nisam znao da mos samo sa foreach proc kroz dictionary... oops
            {
                if (report.Value.StockValue < reports[worstYear].StockValue)
                {
                    worstYear = report.Key;
                }
                if (report.Value.StockValue > reports[bestYear].StockValue)
                {
                    bestYear = report.Key;
                }
            }

            return new DrasticYears(bestYear, worstYear);
        }
    }

    //Ovo je odvojeno strelicama pa nije vezan za ono gore !!!!

    public abstract class IotSensor
    {
        protected int samplingRateHz;
        protected double batteryPrecent;
        protected double samplePowerDraw;

        protected IotSensor(int samplingRateHz, double batteryPrecent, double samplePowerDraw)
        {
            this.samplingRateHz = samplingRateHz > 0 ? samplingRateHz : 1;
            this.batteryPrecent = Math.Clamp(batteryPrecent, 0, 100);
            this.samplePowerDraw = samplePowerDraw;
        }

        public int CalculateRemainingTime()
        {
            int sample =(int) (batteryPrecent / samplePowerDraw);
            int time = sample / samplingRateHz;
            return time; //sretno u ucenju oet opet
        }

        public int CountMaxSamples(int seconds)
        {
            int time = Math.Min(CalculateRemainingTime(), seconds);
            int samplesCount = time * samplingRateHz;
            return samplesCount;

        }

        public abstract List<Measurement> Measure(DateTime start, DateTime stop); //Moze timespan a moze i int seconds samo da je modelirano vrijeme
    }

    public abstract class Measurement
    { 
        public abstract DateTime MeasuredAt { get; }
        public abstract string CreateReport();
    }

    class SensorArray
    {
        IotSensor[][] sensors;

        public SensorArray(int rows, int columns)
        {
            sensors = new IotSensor[rows][];
            for (int i = 0; i < sensors.Length; i++)
            {
                sensors[i] = new IotSensor[columns];
            }
        }

        public void Insert(IotSensor sensor, int row, int column)
        {
            sensors[row][column] = sensor;
        }

        public int DetermineRemainingTime()
        {
            int time = 0;

            foreach (var sensorRow in sensors)
            {
                foreach (var sensor in sensorRow)
                {
                    if (sensor != null)
                    {
                        if (sensor.CalculateRemainingTime() == 0)
                            throw new SensorFailException();

                        //int currentTime = sensor.CalculateRemainingTime(); ovo mozes napraviti i onda samo umetnes current time tamo di je sensor.CalculateRemainingTime()
                        if (time == 0 || sensor.CalculateRemainingTime() < time)
                        {
                            time = sensor.CalculateRemainingTime();
                        }
                    }
                }
            }

            return time;
        }
    }

    class SensorFailException : Exception
    {
        public SensorFailException()
        {
            
        }
        public SensorFailException(string message) : base (message) //Ova dva konstruktora MORAJU BIT UVIJEK KADA RADIMO CUSTOM EXCEPTION
        {
            
        }
    }

    //Strelice opet

    public class Circle
    {
        public Circle(int x, int y, double radius)
        {
            this.X = x;
            this.Y = y;
            this.Radius = radius;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public double Radius { get; private set; }
        public double Area { get { return Radius * Radius * Math.PI; } }
    }

    public interface ICirclePicker
    {
        Circle PickMostSimilar(Circle circle, List<Circle> circles);
        List<Circle> PickSimilar(Circle circle, List<Circle> circles, double differencePrecent);
    }

    class AreaPicker : ICirclePicker
    {
        public Circle PickMostSimilar(Circle circle, List<Circle> circles)
        {
            int mostSimilatIndex = 0;

            for (int i = 0; i < circles.Count; i++)
            {
                if (
                    Math.Abs(circle.Area - circles[i].Area) <
                    Math.Abs(circle.Area - circles[mostSimilatIndex].Area)) // ovo opet mos izvuc van pa optimizirat kod al ne moras, nece se gledat
                {
                    mostSimilatIndex = i;
                }
            }

            return circles[mostSimilatIndex];
        }

        public List<Circle> PickSimilar(Circle circle, List<Circle> circles, double differencePrecent)
        {
            List<Circle> similarCircles = new List<Circle>();

            double minArea = circle.Area * (1.0 - differencePrecent);
            double maxArea = circle.Area * (1.0 + differencePrecent);

            foreach (Circle c in circles)
            {
                if (c.Area >= minArea && c.Area <= maxArea) //moze i samo > i <, nije eksplicitno zadano da mora bit jednako pa je svejedno
                {
                    similarCircles.Add(c);
                }
            }

            return similarCircles;
        }
    }

    class Drawing
    {
        Circle selectedCircle;
        List<Circle> circles;
        ICirclePicker circlePicker;

        public Drawing(Circle selectedCircle, List<Circle> circles, ICirclePicker circlePicker)
        {
            this.selectedCircle = selectedCircle;
            this.circles = circles;
            this.circlePicker = circlePicker;
        }

        public void ChangePicker(ICirclePicker circlePicker)
        {
            this.circlePicker = circlePicker;
        }

        public double CalculateTotalArea(double differencePercent)
        {
            double totalArea = 0.0;
            List<Circle> similarCircles = circlePicker.PickSimilar(selectedCircle, circles, differencePercent);

            foreach (Circle c in similarCircles)
            {
                totalArea += c.Area;
            }
            return totalArea;
        }

    }

    public static class RandomExtensions
    {
        public static Circle NextCircle(this Random generator, double maxRadius)
        {
            double radius = 1.0 + generator.NextDouble() * (maxRadius - 1.0);
            return new Circle(generator.Next(), generator.Next(), radius);

            //testirat za zadacu
            //GetRange metoda za trazenje svih slicnih prvom!!!!!!!!!
        }
    }
}
