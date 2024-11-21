internal class Program
{
    static void Main(string[] args)
    {
        Cart cart = new Cart(new CouponDiscount(10m));
        cart.Add(new Product(100, 10));
        Console.WriteLine(cart.CalculateTotal());
        cart.ChangeDiscount(new PercentageDiscount(0.7m));
        Console.WriteLine(cart.CalculateTotal());
    }
}

class Product
{
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public Product(decimal price, int quantity)
    {
        Price = price;
        Quantity = quantity;
    }
    public void Additem() => Quantity++;
    public void RemoveItem()
    {
        if (Quantity > 0) { Quantity--; }
    }
}
interface IDiscount
{
    decimal CalculateDiscountedPrice(decimal price);
}
class PercentageDiscount : IDiscount
{
    decimal percent; //decimal se radi sa parama jer je tocnije

    public PercentageDiscount(decimal percent)
    {
        this.percent = Math.Clamp(percent, 0.0m, 1.0m); //ovo m samo kaze da je znamenka decimal tipa
    }

    public decimal CalculateDiscountedPrice(decimal price)
    {
        price -= price * percent;
        return price;
    }
}
class CouponDiscount : IDiscount
{
    private decimal amount;
    public CouponDiscount(decimal amount)
    {
        this.amount = amount;
    }


    public decimal CalculateDiscountedPrice(decimal price)
    {
        return price - amount;
    }
}

class Cart
{
    List<Product> products;
    IDiscount discount;

    //public Cart()
    //{
    //    this.products = new List<Product>();
    //    this.discount = new PercentageDiscount(0.2m);
    //}

    public Cart(IDiscount discount)
    {
        this.products = new List<Product>();
        this.discount = discount;
    }

    public void ChangeDiscount(IDiscount discount) => this.discount = discount;

    public void Add(Product product) => products.Add(product);
    public void Remove(Product product) => products.Remove(product);

    public decimal CalculateTotal() //vamo ni slucajno stavljat 'List<Product> products' jer te rusi odma!!
    {
        decimal total = 0.0m;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return discount.CalculateDiscountedPrice(total);
    }
}