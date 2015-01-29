# NTH.Framework

A small framework for solving common problems. Data Storage, Encryption and Stuff.

Not to confuse with [nikeee/nth](https://github.com/nikeee/nth), wich is not a framework for common problems, instead it only provides small extensions.


## Features

### Data Storage
```C#
using (var storage = new FileDataStore<Human>(
    new JsonSerializer<Human>(), /* serializer (builtin: JSON and Binary) */
    "Humans", /* directory */
    ".human.gz", /* file extension */
    new GZipDataStorageProxy() /* compression (builtin: gzip) */))
{
    var human1 = new Human("Gill Bates", 21);
    await storage.StoreAsync("gill", human1); // TAP for non-blocking storage
    // ...
    var gill = await storage.RetrieveAsync("gill");
}

[Serializable]
class Human
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Human(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

### Global Hotkey API
```C#
var hook = new KeyboardHook();
var someHotkey = new Hotkey(ModifierKeys.Win, Keys.F4);
someHotkey.KeyPressed += (s, e) => {
    MessageBox.Show("someHotkey pressed! Was: " + someHotkey);
};

hook.RegisterHotkey(someHotkey);
//... later:
hook.UnregisterHotkey(someHotkey);
```


### Windows Mail API
```C#
var mail = new MapiMail();
mail.Subject = "Some Subject";
mail.MessageBody = "This is the message content";
mail.Recipients.Add("somebody@example.com");
mail.BccRecipients.Add("secretSomebody@example.com");
mail.SendPopup(); // Open default email with this preset email
```
