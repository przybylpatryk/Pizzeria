using System;
namespace Pizzeria.pizza
{
    public enum size
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }
    public enum toppings
    {
        salami,
        chicken,
        ham,
        pepperoni,
        onions,
        pinaple,
        mushroom,
        olives,
        oregano
    }
    public abstract class Pizza
    {

        public string Name { get; set; }
        public size Size { get; set; }
        public bool Cheese { get; set; }
        public bool TomatoSauce { get; set; }
        public List<toppings> Toppings { get; set; }
        public decimal Price { get; set; }

        public Pizza(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null)
        {
            Toppings = new List<toppings>();
            this.Size = Size;
            this.Cheese = Cheese;
            this.TomatoSauce = TomatoSauce;
            this.Toppings = Toppings;
        }
    }
    public sealed class Margherita : Pizza
    {
        public string Name { get; set; }
        public size Size { get; set; }
        public Margherita(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null) : base(Size, Cheese, TomatoSauce, Toppings)
        {
            Toppings = new List<toppings>();
            Name = "Margharita";
        }
        public bool Cheese { get; set; }
        public bool TomatoSauce { get; set; }
        public decimal Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {
                    case size.Small:
                        Price = 20.0M;
                        break;
                    case size.Medium:
                        Price = 25.0M;
                        break;
                    case size.Large:
                        Price = 35.0M;
                        break;
                    case size.ExtraLarge:
                        Price = 40.0M;
                        break;
                }
            }
        }
    }

    public sealed class Pepperoni : Pizza
    {
        public string Name { get; set; }
        public size Size { get; set; }
        public Pepperoni(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null) : base(Size, Cheese, TomatoSauce, Toppings)
        {
            Toppings = new List<toppings>();
            Name = "Pepperoni";
            Toppings.Add(toppings.pepperoni);
        }
        public bool Cheese { get; set; } = true;
        public bool TomatoSauce { get; set; } = true;
        public List<toppings> Toppings{ get; set; }
        public decimal Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {
                    case size.Small:
                        Price = 22.5M;
                        break;
                    case size.Medium:
                        Price = 27.5M;
                        break;
                    case size.Large:
                        Price = 37.5M;
                        break;
                    case size.ExtraLarge:
                        Price = 42.5M;
                        break;
                }
            }
        }
    }

    public sealed class Hawaiian : Pizza
    {
        public string Name { get; set; }
        public size Size { get; set; }
        public Hawaiian(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null) : base(Size, Cheese, TomatoSauce, Toppings)
        {
            Toppings = new List<toppings>();
            Name = "Hawajska";
            Toppings.Add(toppings.ham);
            Toppings.Add(toppings.pinaple);
        }
        public bool Cheese { get; set; } = true;
        public bool TomatoSauce { get; set; } = true;
        public List<toppings> Toppings { get; set; }
        public decimal Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {
                    case size.Small:
                        Price = 24.5M;
                        break;
                    case size.Medium:
                        Price = 29.5M;
                        break;
                    case size.Large:
                        Price = 39.5M;
                        break;
                    case size.ExtraLarge:
                        Price = 44.5M;
                        break;
                }
            }
        }
    }
    public sealed class Capricciosa : Pizza
    {
        public string Name { get; set; }
        public size Size { get; set; }
        public Capricciosa(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null) : base(Size, Cheese, TomatoSauce, Toppings)
        {
            Toppings = new List<toppings>();
            Name = "Capricciosa";
            Toppings.Add(toppings.ham);
            Toppings.Add(toppings.mushroom);
            Toppings.Add(toppings.olives);
        }
        public bool Cheese { get; set; } = true;
        public bool TomatoSauce { get; set; } = true;
        public List<toppings> Toppings { get; set; }
        public decimal Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {
                    case size.Small:
                        Price = 25M;
                        break;
                    case size.Medium:
                        Price = 30M;
                        break;
                    case size.Large:
                        Price = 40M;
                        break;
                    case size.ExtraLarge:
                        Price = 45M;
                        break;
                }
            }
        }
    }
    public sealed class JohnnysSpecial : Pizza
    {
        public string Name { get; set; }
        public size Size { get; set; }
        public JohnnysSpecial(size Size, bool Cheese = true, bool TomatoSauce = true, List<toppings> Toppings = null) : base(Size, Cheese, TomatoSauce, Toppings)
        {
            Toppings = new List<toppings>();
            Name = "JohnnysSpecial";
            Toppings.Add(toppings.chicken);
            Toppings.Add(toppings.ham);
            Toppings.Add(toppings.salami);
        }
        public bool Cheese { get; set; } = true;
        public bool TomatoSauce { get; set; } = true;
        public List<toppings> Toppings { get; set; }
        public decimal Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {
                    case size.Small:
                        Price = 25M;
                        break;
                    case size.Medium:
                        Price = 30M;
                        break;
                    case size.Large:
                        Price = 40M;
                        break;
                    case size.ExtraLarge:
                        Price = 45M;
                        break;
                }
            }
        }
    }
}