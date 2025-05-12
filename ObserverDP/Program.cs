

const int threshold = 30_000;

var emailPriceChangerNotifier = new EmailPriceChangeNotifier(threshold);
var pushPriceChangeNotifier = new PushPriceChangeNotifier(threshold);
var goldPriceReader = new GoldPriceReader();

goldPriceReader.Attach(emailPriceChangerNotifier); 
goldPriceReader.Attach(pushPriceChangeNotifier);

for (int i = 0; i < 10; i++)
{
    goldPriceReader.ReadCurrentPrice();
}

//turning push off
Console.WriteLine("Turning push off");  
goldPriceReader.Detach(pushPriceChangeNotifier);
for (int i = 0; i < 10; i++)
{
    goldPriceReader.ReadCurrentPrice();
}

public class GoldPriceReader
{
    private int _currentGoldPrice;
    private readonly List<IObserver<decimal>> _observers = [];

    public void ReadCurrentPrice()
    {
        _currentGoldPrice = new Random().Next(20000, 50000);
        NotifyObservers();
    }

    public void Attach(IObserver<decimal> observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver<decimal> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_currentGoldPrice);
        }
    }
}

public class EmailPriceChangeNotifier(decimal notificationThreshold) : IObserver<decimal>
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
    
    public class PushPriceChangeNotifier(decimal notificationThreshold) : IObserver<decimal>
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

    public interface IObserver<TData>
    {
        void Update(TData data);
    }

    public interface IObservable<TData>
    {
        void Attach(IObserver<TData> observer);
        void Detach(IObserver<TData> observer);
        void NotifyObservers();
    }