namespace WebApp2._2.Models
{
    public class CalculatorViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Action { get; set; }
        public string Calc() 
        {
            return Action switch
            {
                "+" => $"{X} + {Y} = {X + Y}",
                "-" => $"{X} - {Y} = {X - Y}",
                "*" => $"{X} * {Y} = {X * Y}",
                "/" when Y != 0 => $"{X} / {Y} = {X / Y}",
                "/" when Y == 0 => "Error: division by zero",
                _ => "Invalid operation"
            };
        }
    }
}