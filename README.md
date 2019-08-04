# DictJson
 Dictionary-like solution for JSON files in C#  

This is a very light-weight solution to deal with JSON files that will not be used whose schema again and again. If you need to generate a string from C# object (e.g. domain objects) to use it as JSON file in the front-end of your web application, writing a cast function for each JSON schema may not be very useful unless you will use this cast function again. Using dictionary is a simpler way to generate JSON string from C# object. Because [Newtonsoft.Json](https://www.newtonsoft.com/json) can serialize `Dictionary<T, U>` type to JSON, there is no need to do extra things.  

Only thing in this approach is using `Add()` method of `Dictionary<T, U>` class to create `JsonObject` returning `_("name", value)` method. Thus you can chain your adding methods (`_("name", value)`) to create a JSON file. Additionally, a JSON document is always a collection of string-object pair (collection of `KeyValuePair<string, object>` in C#), so with this method, you don't have to specify types of key and value every time.  

You can create a JSON file like this:  
```
{
    "type": "Person",
    "name": "Aragorn",
    "birthDate": {
        "type": "date",
        "year": 1970,
        "month": 8,
        "day": 4
    },
    "job": "Sword Master",
    "friends": [
        {
            "type": "Person",
            "name": "Legolas",
            "birthDate": {
                "type": "date",
                "year": 1960,
                "month": 11,
                "day": 25
            },
            "job": "Archer"
        },
        {
            "type": "Person",
            "name": "Gimli",
            "birthDate": {
                "type": "date",
                "year": 1965,
                "month": 12,
                "day": 30
            },
            "job": "Axe Master"
        }
    ]
}
```
by using this method as below:
```
var dictJson = new JsonObject()
    ._("type", "Person")
    ._("name", person.FirstName)
    ._("birthDate", new JsonObject()
        ._("type", "date")
        ._("year", person.BirthDate.Year)
        ._("month", person.BirthDate.Month)
        ._("day", person.BirthDay.Day))
    ._("job", person.Job)
    ._("friends", person.Friends.Select(p => new JsonObject()
        ._("type", "Person")
        ._("name", p.FirstName)
        ._("birthDate", new JsonObject()
            ._("type", "date")
            ._("year", p.BirthDate.Year)
            ._("month", p.BirthDate.Month)
            ._("day", p.BirthDay.Day))
        ._("job", p.Job)));
        
var dictJsonStr = JsonConvert.SerializeObject(dictJson);
Console.WriteLine(newDictJson);
```
Note that `JsonConvert.SerializeObject(dictJson)` method is under `Newtonsoft.Json` namespace, coming with Newtonsoft.Json framework and the `person` is an instance of a custom class having `FirstName`, `BirthDate` and `Job` properties.  

By using `new JsonObject()`, you can add/create a JSON object, and by using `_("name", value)` method at instance of `JsonObject`, you can add JSON member to `JsonObject` (see [RFC 4627](https://www.ietf.org/rfc/rfc4627.txt) for JSON definitions). You can, of course, create your own `JsonMember` and `JsonObject` classes etc. instead of deriving from `Dictionary<string, object>` but this way is so simple to implement and with it, you can enjoy the benefits of using `Dictionary` type.

## Dictionary Initializer
Dictionary initializer coming with C# 6.0 is useful for this purpose. But still, you have to specify types of key and value of dictionary every time. Nice thing about using dictionary initializer is that you can format your code automatically with _Ctrl+E, D_ in Visual Studio, instead of indenting manually.
