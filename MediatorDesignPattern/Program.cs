using System;
using System.Collections.Generic;

// Create instances of Mediator.
IFacebookGroupMediator facebookMediator = new ConcreteFacebookGroupMediator();

// Create instances of users.
User manun = new ConcreteUser("Manun");
User biplob = new ConcreteUser("Biplob");
User imrul = new ConcreteUser("Imrul");
User saddam = new ConcreteUser("Saddam");
User kabir = new ConcreteUser("Kabir");
User taijul = new ConcreteUser("Taijul");

// Register the users with the mediator.
facebookMediator.RegisterUser(biplob);
facebookMediator.RegisterUser(manun);
facebookMediator.RegisterUser(imrul);
facebookMediator.RegisterUser(saddam);
facebookMediator.RegisterUser(kabir);
facebookMediator.RegisterUser(taijul);

// Users sending messages in the Facebook Group.
biplob.Send("Hi everyone how are you?");
Console.WriteLine();
kabir.Send("Do you know What is Design Patterns? explain");


// Mediator Interface
public interface IFacebookGroupMediator
{
    void RegisterUser(User user);
    void SendMessage(string msg, User user);
}

// Concrete Mediator
public class ConcreteFacebookGroupMediator : IFacebookGroupMediator
{
    private List<User> usersList = new List<User>();

    public void RegisterUser(User user)
    {
        usersList.Add(user);
        user.Mediator = this;
    }

    public void SendMessage(string message, User user)
    {
        foreach (User u in usersList)
        {
            if (u != user)
            {
                u.Receive(message);
            }
        }
    }
}

// Colleague
public abstract class User
{
    protected string Name;
    public IFacebookGroupMediator Mediator { get; set; }

    public User(string name)
    {
        this.Name = name;
    }

    public abstract void Send(string message);
    public abstract void Receive(string message);
}

// Concrete Colleague
public class ConcreteUser : User
{
    public ConcreteUser(string name) : base(name)
    {

    }
    public override void Receive(string message)
    {
        Console.WriteLine(this.Name + ": Received Message: " + message);
    }

    public override void Send(string message)
    {
        Console.WriteLine(this.Name + ": Sending Message = " + message + "\n");
        Mediator.SendMessage(message, this);
    }
}
