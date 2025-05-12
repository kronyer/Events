const int threshold = 30_000;

var emailPriceChangerNotifier = new EmailPriceChangeNotifier(threshold);
var pushPriceChangeNotifier = new PushPriceChangeNotifier(threshold);
var goldPriceReader = new GoldPriceReader();


goldPriceReader.PriceRead += emailPriceChangerNotifier.Update;
goldPriceReader.PriceRead += pushPriceChangeNotifier.Update;

for (int i = 0; i < 10; i++)
{
    goldPriceReader.ReadCurrentPrice();
}


public delegate void PriceRead(decimal price);

public class GoldPriceReader
{
    public event PriceRead? PriceRead; //events are always a delegate
    
    public void ReadCurrentPrice()
    {
        var  currentGoldPrice = new Random().Next(20000, 50000);
        OnPriceRead(currentGoldPrice);
    }
    
    private void OnPriceRead(decimal price)
    {
        PriceRead?.Invoke(price); //null propagating 
    }
    
}

public class EmailPriceChangeNotifier(decimal notificationThreshold) 
    {
        public void Update(decimal currentPrice)
        {
            if (currentPrice > notificationThreshold)
            {
                // send email
                Console.WriteLine($"Email sent, because price is {currentPrice}, and it's above {notificationThreshold}");
            }
        }
    }
    
    public class PushPriceChangeNotifier(decimal notificationThreshold) 
    {
        public void Update(decimal currentPrice)
        {
            if (currentPrice > notificationThreshold)
            {
                // send email
                Console.WriteLine($"SMS sent, because price is {currentPrice}, and it's above {notificationThreshold}");
            }
        }
    
    }

