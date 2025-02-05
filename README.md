# Factory Design Pattern

The Factory Pattern is a creational design pattern that provides a single method to create objects without exposing the instantiation logic. Instead of using new or Instantiate(), we call a factory method that returns the required object.

# Abstract Factory Pattern

The Abstract Factory Pattern extends the Factory Pattern by grouping multiple related factories under a single interface. Instead of creating individual objects, it provides a way to create families of related objects without specifying their concrete classes.

![Alt Text](https://github.com/nikunjkareliya/FactoryDesignPattern/blob/master/Screenshots/Factory.PNG?raw=true)


## Example

Define types of flyable objects that game has

```csharp
public enum FlyableType
{        
    Balloon,        
    Bubble, 
    HotAirBalloon,
    Heart,
    Cloud,
    AirPlane
}

```

Config scriptable object holds static flyable data like sprite, spawn position, prefab reference, etc..

```csharp
[CreateAssetMenu(fileName = "FlyableConfig", menuName = "SO/FlyableConfig")]
public class FlyableConfig : ScriptableObject
{
    public FlyableType flyableType;
    public GameObject flyablePrefab;
    public ParticleSystem popEffect;
    public Sprite sprite;
    public Vector3 spawnPos;        
    public float minSpeed = 1.2f;
    public float maxSpeed = 2.2f;
}

```

## Define the Factory Interface
The base factory class is an abstract ScriptableObject, ensuring that all concrete factories implement the CreateFlyable method.

```csharp
public abstract class FlyableFactory : ScriptableObject
{                
    public abstract IFlyable CreateFlyable();
}
```

## Implement Concrete Factories
Each concrete factory extends FlyableFactory and is responsible for instantiating a specific flyable type.


```csharp
[CreateAssetMenu(fileName = "BalloonFactory", menuName = "SO/Factories/BalloonFactory")]
public class BalloonFactory : FlyableFactory
{
    public FlyableConfig config;
    public override IFlyable CreateFlyable()
    {
        var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
        return obj.GetComponent<IFlyable>();
    }
}

[CreateAssetMenu(fileName = "BubbleFactory", menuName = "SO/Factories/BubbleFactory")]
public class BubbleFactory : FlyableFactory
{
    public FlyableConfig config;
    public override IFlyable CreateFlyable()
    {
        var obj = Instantiate(config.flyablePrefab, config.spawnPos, Quaternion.identity);
        return obj.GetComponent<IFlyable>();
    }
}
```

## Define the Flyable Interface
The IFlyable interface ensures that all flyable objects share common behaviors.

```csharp
public interface IFlyable
{        
    void Init(LetterEntry entry);
    void Fly();
    void Pop();        
}
```

## Implement the Flyable Objects
Each flyable object implements IFlyable. Below is an example for the Balloon class:

```csharp
public class Balloon : MonoBehaviour, IFlyable
{        
    [SerializeField] private FlyableConfig _config;        
    [SerializeField] private TextMeshProUGUI _textLetter;
    [SerializeField] private SpriteRenderer _spriteRenderer;        

    private float _speed = 2.2f;        
    private bool _isFlying = false;
    private Vector3 _targetDirection;
    private LetterEntry _entry;

    public void Init(LetterEntry entry)
    {
        _entry = entry;            
        _textLetter.SetText(entry.value);
        _spriteRenderer.sprite = _config.sprite;            
    }

    public void Fly()
    {
        _isFlying = true;
        _targetDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1f, 0f);
        _speed = Random.Range(_config.minSpeed, _config.maxSpeed);
    }

    private void Update()
    {
        if (_isFlying)
        {                
            transform.Translate(_targetDirection * _speed * Time.deltaTime);
            if (transform.position.y > Camera.main.orthographicSize * 2)
            {
                _isFlying = false;
                Destroy(gameObject);
            }
        }
    }

    public void Pop()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject, 1f));
        if (_config.popEffect != null)
        {
            Instantiate(_config.popEffect, transform.position, Quaternion.identity);
        }
    }
}
```





## Define a Factory Registry
A registry is used to manage multiple factory instances efficiently.

```csharp
[System.Serializable]
public struct FlyableFactoryEntry
{
    public FlyableType flyableType;
    public FlyableFactory factory;
}

[CreateAssetMenu(fileName = "FlyableFactoryRegistry", menuName = "SO/Factories/FlyableFactoryRegistry")]
public class FlyableFactoryRegistry : ScriptableObject
{
    [SerializeField] private List<FlyableFactoryEntry> _factories = new();         
    private Dictionary<FlyableType, FlyableFactory> _factoryLookup = new Dictionary<FlyableType, FlyableFactory>();

    public void Init()
    {
        _factoryLookup.Clear();

        foreach (var entry in _factories)
        {
            if (!_factoryLookup.ContainsKey(entry.flyableType))
            {
                _factoryLookup.Add(entry.flyableType, entry.factory);
            }
        }
    }

    public IFlyable CreateFlyable(FlyableType flyableType)
    {
        if (_factoryLookup.TryGetValue(flyableType, out FlyableFactory factory))
        {                
            return factory.CreateFlyable();
        }

        Debug.LogError($"No factory registered for FlyableType: {flyableType}");
        return null;
    }

    public IFlyable CreateRandomFlyable()
    {
        int randomIndex = Random.Range(0, _factories.Count);
        var randomFactory = _factories[randomIndex].factory;

        return randomFactory.CreateFlyable();
    }

}
```

## Implement the Spawn Controller
The SpawnController uses the factory registry to create random flyable objects dynamically.

```csharp
[System.Serializable]
public class LetterEntry
{
    public string ID;
    public string value;    
}

[CreateAssetMenu(fileName = "LettersConfig", menuName = "SO/LettersConfig")]
public class LettersConfig : ScriptableObject
{
    public string locale = "en";
    public List<LetterEntry> lettersMap = new List<LetterEntry>();
}

public class SpawnController : MonoBehaviour
{
    [SerializeField] private LettersConfig _lettersConfig;
    [SerializeField] private FlyableFactoryRegistry _factoryRegistry;        
    [SerializeField] private float _spawnFrequency = 0.5f;
    private List<LetterEntry> _allLetters = new();

    private void Awake()
    {
        _allLetters = _lettersConfig.lettersMap;
        _factoryRegistry.Init();
        StartCoroutine(SpawnFlyables());
    }
  
    private IEnumerator SpawnFlyables()
    {        
        while (true)
        {                
            var letterEntry = allLetters[Random.Range(0, _allLetters.Count)];
            IFlyable flyable = _factoryRegistry.CreateRandomFlyable();
            flyable.Init(letterEntry);
            flyable.Fly();
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }
}
```

## Conclusion
The Factory Design Pattern used in systems requiring dynamic object creation. By combining it with a data-driven approach in Unity, you empower designers to iterate on content while keeping your codebase clean and scalable. The use of ScriptableObjects and a centralized registry ensures that adding new object types becomes a configuration task rather than a coding challenge.

We could also extend this further to add Object Pooling for further memory optimization.







