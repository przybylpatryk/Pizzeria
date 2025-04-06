using System;
public enum size
{
    Small,
    Medium,
    Large,
    ExtraLarge
}
public enum extras
{
	salami,
	chicken,
	ham,
	onions,
	pinaple,
	mushroom


}
public class Pizza
{
	
	public string Name {  get; set; }
	public size Size { get; set; }
	public bool Cheese = true;
    public bool TomatoSauce = true;
    public List<extras> Extras { get; set; }


	public Pizza()
	{
	}
}
