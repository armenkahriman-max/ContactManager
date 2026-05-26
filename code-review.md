# Code Review

## Class1.cs
- line 28-30:
```csharp
    public class ContactService
    
   public void AddContact(string name)
```
Work in progress i guess ? compiling version:
```csharp
public class ContactService
{
    public void AddContact(string name) { }
} 
```
but make sure this is outside of `InMemoryContactRepository`.
- remove unused namespaces
- one class per file, or one file per class

-fixed the mistakes